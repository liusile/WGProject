using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Domain.Models;
using log4net;
using Service;
using Service.ServiceImp;
using CustomTask;
using OrderSortingBy50.Properties;
using OrderSortingBy50.Frm;
using Service.Model;
using System.Net.Sockets;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using Newtonsoft.Json;
using System.Reflection;
using OrderSortingBy50.Common;

namespace OrderSortingBy50
{
    public partial class Main : Form
    {
        private SlaveInfoService SlaveInfoService { get; set; }//
        public ButtonService ButtonService { get; private set; }//

        public SoundService SoundService = new SoundService();
        private SysConfig SysConfig { get; set; }//
        public WaveApiService WaveApiService { get; set; }
        public UpDownService UpDownService { get; set; }
        public CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        public static Domain.Models.UserInfo CurUser { get; set; } = new Domain.Models.UserInfo();
        Socket socketSend;

        public Main()
        {
            InitializeComponent();
            Init();
            if (CurUser.UserName == null)
            {
                Task.Run(() =>
                {
                    new Login().ShowDialog();
                    Invoke((MethodInvoker)delegate ()
                    {
                        UpdatelblCurUser();
                    });
                });   
            }
            StarMenu.Enabled = true;
            ToolSysManage.Visible = true;
        }
        #region 初始化
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        { 
            InitService();
            InitUser();
            InitTask();
            InitButton();
          //  SlaveInfoService.ClearDataAsync();
        }
        /// <summary>
        /// 初始化服务
        /// </summary>
        private void InitService()
        {
            SlaveInfoService = new SlaveInfoService();
            ButtonService = new ButtonService(SlaveInfoService);
            SysConfig = new SysConfigService().Get();
            WaveApiService = new WaveApiService();
            UpDownService = new UpDownService(SysConfig,SlaveInfoService, Main.CurUser);
            UpDownService.AddService(new PcUpDownService(SlaveInfoService, ButtonService, SynchronizationContext.Current, SysConfig,Main.CurUser));
            UpDownService.AddService(new BoardUpDownService(SlaveInfoService));
        }

        /// <summary>
        /// 初始化按钮
        /// </summary>
        private void InitButton()
        {
            var sizeChangeContext = new SizeChangeContext(Width, Height, SysConfig.LatticeLineCount, SlaveInfoService.LatticeInfoList.Count / SysConfig.LatticeLineCount);
            var btnList = ButtonService?.CreateButtonList(sizeChangeContext);
            if (btnList != null) Controls.AddRange(btnList.ToArray());
            btnList.ForEach(o => o.MouseDown += (sender, e) =>
              {
                  if (e.Button == MouseButtons.Right)
                  {
                      var btn = sender as Button;
                      Form frm = new LatticeConfig(btn.Name, UpDownService, SlaveInfoService,ButtonService);
                      frm.ShowDialog();
                  }
              }
            );
            
            ButtonService.SetButtonList(btnList);
        }
        /// <summary>
        /// 初始化任务
        /// </summary>
        private void InitTask()
        {
            new OrderDownloadTask().RunSync(SysConfig, CurUser, cancellationTokenSource.Token);
        }
        /// <summary>
        /// 初始化用户
        /// </summary>
        private void InitUser()
        {
            var userInfo = new UserService().GetLastUserInfo();
            if (userInfo != null && userInfo.IsAutoLogin)
            {
                CurUser.CopyValues(userInfo);
            }
            UpdatelblCurUser();
        }
        private void UpdatelblCurUser()
        {
           
                if (!string.IsNullOrWhiteSpace(CurUser?.UserName))
                {
                    lblCurUser.Text = CurUser?.UserName;
                    ToolLoginOut.Text = "注销";
                }
                else
                {
                    lblCurUser.Text = "未登录";
                    ToolLoginOut.Text = "登录";
                }
           
        }
        #endregion
        /// <summary>
        /// 窗体大小改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_SizeChanged(object sender, EventArgs e)
        {
            if (SysConfig != null)
            {
                var sizeChangeContext = new SizeChangeContext(Width, Height, SysConfig.LatticeLineCount, SlaveInfoService.LatticeInfoList.Count / SysConfig.LatticeLineCount);
                ButtonService.ReSizeButons(sizeChangeContext);
            }
        }
        /// <summary>
        /// 开始分拣
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StarMenu_Click(object sender, EventArgs e)
        {
            try
            {
                if (ConnectBoard())
                {
                    //检查是否有上次未分拨完的
                    var slaveInfo = SlaveInfoService.GetSlaveInfo(o=>!o.IsComplete);
                    if (slaveInfo != null)
                    {
                        var result = MessageBox.Show($"波次:{slaveInfo.WaveNo}未分拣完，是否继续？", "提示",
                            MessageBoxButtons.OKCancel);
                        if (result == DialogResult.OK)
                        {

                            UpDownService.BeginSort(slaveInfo);
                            lblMsg.Text = $"开始波次：{slaveInfo.WaveNo}";
                        }
                        else
                        {
                            SlaveInfoService.DeleteSlaveInfo(slaveInfo);
                        }
                    }
                    try
                    {
                        if (serialPort_Scan.IsOpen)
                            serialPort_Scan.Close();
                        serialPort_Scan.PortName = SysConfig.ScanPortName ?? "";
                        serialPort_Scan.Open();
                    }
                    catch (Exception ex){ LogHelper.SaveAsyn("开始分拣出错", ex); }
                    txtInput.Enabled = true;
                    txtInput.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                LogHelper.SaveAsyn("开始分拣出错",ex);
            }
        }
        /// <summary>
        /// 设备数据输入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                string data = txtInput.Text;
                if (e.KeyCode != Keys.Enter || string.IsNullOrWhiteSpace(data))
                {
                    txtInput.Select();
                    return;
                }
                txtInput.Text = "";
               // data = data.Substring(1);
                data = data.Split('-')[0];
                if (data == "admin")
                {
                    ToolSysManage.Visible = true;
                }
                else if (SlaveInfoService.IsComplete)//波次
                {
                    SortWaveBegin(data);
                }
                else
                {
                    SortProductBegin(data);
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveAsyn("设备数据输入事件", ex);
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 开始分拣产品
        /// </summary>
        /// <param name="productCode"></param>
        private void SortProductBegin(string barCode)
        {
            //0.验证
            if (SlaveInfoService.SlaveInfo.LatticeStatus == LatticeStatus.PutError)
            {
                if (SlaveInfoService.LatticeInfoList.Exists(o => o.Status == LatticeStatus.WaitPut && o.Product.Exists(p => p.BarCode == barCode)))
                {
                    var latticePutError = SlaveInfoService.LatticeInfoList.Where(o => o.Status == LatticeStatus.PutError).ToList();
                    UpDownService.RemovePutError(new UpDownMessage()
                    {
                        LatticeByUpDown =
                            latticePutError.Select(l => new LatticeByUpDown {LatticeNo = l.LatticeNo}).ToList()
                    });

                    lblMsg.Text = string.Format(Resources.RemovePutError, barCode);
                }
                else
                {
                    lblMsg.Text = string.Format(Resources.PleaseSolvePutError, barCode);
                    SoundService.PlaySound(SoundType.PutError);
                }
                return;
            }
            if (SlaveInfoService.SlaveInfo.LatticeStatus == LatticeStatus.BlockError)
            {
                lblMsg.Text = string.Format(Resources.PleaseSolveBlockError, barCode);
                SoundService.PlaySound(SoundType.BlockError);
                return;
            }
            if (SlaveInfoService.SlaveInfo.LatticeStatus == LatticeStatus.WaitPut)
            {
                lblMsg.Text = string.Format(Resources.PleaseWait, barCode);
                UpDownService.SetText("存在待投递的产品!");
                SoundService.PlaySoundAsny(SoundType.WaitPut);
                return;
            }
            
            //1.装载格口
            var waveApiList = WaveApiService.GetOrderApiList(SlaveInfoService.SlaveInfo.WaveNo, barCode);
            if (waveApiList.Count == 0)
            {
                UpDownService.ProductNotFound();
                lblMsg.Text = string.Format(Resources.ProductError, SlaveInfoService.SlaveInfo.WaveNo, barCode);
                return;
            }
            waveApiList.ForEach(o =>
            {
                if (!SlaveInfoService.LatticeInfoList.Exists(s => s.OrderNo == o.OrderNo))
                { 
                    SlaveInfoService.LoadLattice(o);
                }
            });
            if (! SlaveInfoService.SlaveInfo.LatticeInfo.SelectMany(o=>o.Product).Where(o => o.BarCode == barCode).Any(o=> !o.IsComplete))
            {
                lblMsg.Text = $"货物已配足:{barCode}";
                UpDownService.SetText($"货物已配足:{barCode}");
                SoundService.PlaySoundAsny(SoundType.ProductOver);
                return;
            }
             //2.更新格口为待投递
            string orderNo = waveApiList.First().OrderNo;
            var waitPutLattice = SlaveInfoService.LatticeInfoList.Where(o => !o.IsComplete && waveApiList.Select(s=>s.OrderNo).Contains(o.OrderNo)).ToList();
            UpDownService.WaitPut(waitPutLattice, barCode);
        }
        /// <summary>
        /// 开始波次
        /// </summary>
        /// <param name="waveNo"></param>
        private void SortWaveBegin(string waveNo)
        {
            if (!string.IsNullOrWhiteSpace(SlaveInfoService.SlaveInfo.WaveNo) && !SlaveInfoService.SlaveInfo.IsPrintOver)
            {
                SoundService.PlaySoundAsny(SoundType.WatitPrintOver);
                UpDownService.SetText("请等待打印完毕！");
                return;
            }

            var waveApi = WaveApiService.Get(waveNo);
            if (waveApi == null)
            {
                UpDownService.WaveNotfound(waveNo);
                Invoke((MethodInvoker)delegate ()
                {
                    lblMsg.Text = $"未找到波次：{waveNo}";
                });
               
                return;
            }
            if (waveApi.Status == WaveStatus.Complete)
            {
                //var result = MessageBox.Show($"波次:{waveNo}已分拣完，是否重新分拣？", "提示",
                //          MessageBoxButtons.OKCancel);
                //if (result == DialogResult.OK)
                //{
                    var slaveInfo = SlaveInfoService.GetSlaveInfo(o => o.WaveNo == waveNo);
                    SlaveInfoService.DeleteSlaveInfo(slaveInfo);
                    slaveInfo = WaveApiService.WaveApiTransformSlaveInfo(waveApi);
                    UpDownService.BeginSort(slaveInfo);
                    Invoke((MethodInvoker)delegate ()
                    {
                        lblMsg.Text = $"开始波次：{waveNo}";
                    });
                //}
                return;
            }
            if (waveApi.Status == WaveStatus.Unwork)
            {
                var slaveInfo = WaveApiService.WaveApiTransformSlaveInfo(waveApi);
                if (slaveInfo!=null)
                {
                    UpDownService.BeginSort(slaveInfo);
                    Invoke((MethodInvoker)delegate ()
                    {
                        lblMsg.Text = $"开始波次：{waveNo}";
                    });
                }
            }
        }
        /// <summary>
        /// 强制完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolWavePowerComplete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!SlaveInfoService.SlaveInfo.IsComplete)
                {
                    var result = MessageBox.Show($"确定强制完成波次:{SlaveInfoService.SlaveInfo.WaveNo}？", "提示",
                        MessageBoxButtons.OKCancel);
                    if (result == DialogResult.OK)
                    {
                        UpDownService.WavePowerComplete(new UpDownMessage
                        {
                            Message = $"强制完成波次号：{SlaveInfoService.SlaveInfo.WaveNo}"
                        });
                        lblMsg.Text = $"已强制完成波次号：{SlaveInfoService.SlaveInfo.WaveNo}";
                    }
                }
                else
                {
                    lblMsg.Text = $"未找到进行中的波次号！";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 分拨中断
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolWaveCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(SlaveInfoService.SlaveInfo.WaveNo))
                {
                    var result = MessageBox.Show($"确定中断波次:{SlaveInfoService.SlaveInfo.WaveNo}？", "提示",
                        MessageBoxButtons.OKCancel);
                    if (result == DialogResult.OK)
                    {
                        UpDownService.WaveCancel(new UpDownMessage
                        {
                            Message = "中断分拨成功"
                        });
                        lblMsg.Text = $"已中断波次号：{SlaveInfoService.SlaveInfo.WaveNo}";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool ConnectBoard()
        {
            try
            {
                if (socketSend == null || !socketSend.Connected)
                {
                   
                    socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    IPEndPoint point = new IPEndPoint(IPAddress.Parse(SysConfig.IP), Convert.ToInt32(SysConfig.Port));
                    socketSend.Connect(point);
                    UpDownService.SetSocket(socketSend); 
                    lblMsg.Text = "连接分拣架成功！";

                    Thread th = new Thread(ReceiveBoardMsg);
                    th.IsBackground = true;
                    th.Start();                   
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("连接分拣架失败！");
                return false;
            }
        }
        private void ReceiveBoardMsg()
        {
            try
            {
                while (true)
                {
                    byte[] buffer = new byte[socketSend.ReceiveBufferSize];
                    int r = socketSend.Receive(buffer);
                    if (r == 0)
                    {
                        break;
                    }
                    string strMsg = Encoding.GetEncoding("GBK").GetString(buffer, 0, r);//GB2312
                    //拆解包转为UpDownCommand对象
                    try
                    {
                        var commandArr = Regex.Matches(strMsg, "(?<=\\*)(.+?)(?=\\*)");
                        for (int i=0;i< commandArr.Count;i++)
                        {
                            var command = commandArr[i].Value;
                            var d = command.Split('#');
                            var upDownMessage = new UpDownMessage
                            {
                                LatticeByUpDown = d[2].Split(',').Select(o =>
                                    new LatticeByUpDown
                                    {
                                        Num = o.IndexOf('&') > 0 ? Convert.ToInt16(o.Split('&')[1]) : 0,
                                        LatticeNo =
                                            string.IsNullOrWhiteSpace(o.Split('&')[0])
                                                ? ""
                                                : Convert.ToInt16(o.Split('&')[0]).ToString()
                                    }
                                ).ToList(),
                                UpDownCommand = (UpDownCommand) Enum.Parse(typeof(UpDownCommand), d[1], true),
                                Type= d[0]
                            };
                            CommandHandle(upDownMessage);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    LogHelper.SaveAsyn(strMsg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CommandHandle(UpDownMessage upDownMessage)
        {
            //用反射可以一劳永益以后不用再一个一个添加，但影响效率的效率有多少？
            switch (upDownMessage.UpDownCommand)
            {
                case UpDownCommand.PutIng:
                    UpDownService.PutIng(upDownMessage);
                    break;
                case UpDownCommand.BlockError:
                    UpDownService.BlockError(upDownMessage);
                    break;
                case UpDownCommand.RemoveBlockError:
                    UpDownService.RemoveBlockError(upDownMessage);
                    break;
                case UpDownCommand.PrintSingle:
                    UpDownService.PrintSingle(upDownMessage);
                    break;
                case UpDownCommand.PrintAll:
                    UpDownService.PrintAll(upDownMessage);
                    break;
                case UpDownCommand.WaveCancel:
                    UpDownService.WaveCancel(upDownMessage);
                    break;
                case UpDownCommand.WavePowerComplete:
                    UpDownService.WavePowerComplete(upDownMessage);
                    break;
                default:
                    break;
            } 
        }

        private void ToolSortLog_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 分拣查询窗口事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolWaveApiSync_Click(object sender, EventArgs e)
        {
            Form frm = new frmWaveApiSync(SysConfig);
            frm.ShowDialog();
        }
        /// <summary>
        /// 分拣明细窗口事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolSortingDetail_Click(object sender, EventArgs e)
        {
            Form frm = new SortingDetail(SlaveInfoService);
            frm.ShowDialog();
        }

        private void ToolSortReport_Click(object sender, EventArgs e)
        {

        }

        private void ToolLocalSocketService_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Application.StartupPath + "/_SocketService/20151110_SocketServer.exe");
        }

        private void ToolSysSetting_Click(object sender, EventArgs e)
        {
            try
            {
                Form frm = new SysSetting();
                frm.ShowDialog();
                var sysConfig = new SysConfigService().Get();
                SysConfig.PrintType = sysConfig.PrintType;
                SysConfig.IP = sysConfig.IP;
                SysConfig.Port = sysConfig.Port;
                SysConfig.DomainName = sysConfig.DomainName;
                SysConfig.ScanPortName = sysConfig.ScanPortName;
                if (serialPort_Scan.IsOpen)
                    serialPort_Scan.Close();
                serialPort_Scan.PortName = SysConfig.ScanPortName ?? "";
                serialPort_Scan.Open();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ToolBoardUpdate_Click(object sender, EventArgs e)
        {
            Form frm = new UpdateBoard();
            frm.ShowDialog();
        }

        private void ToolCheckHardware_Click(object sender, EventArgs e)
        {
            Form frm = new CheckHardware();
            frm.ShowDialog();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            socketSend?.Close();
        }

        private void ToolBoardInit_Click(object sender, EventArgs e)
        {
            Form frm = new BoardInit();
            frm.ShowDialog();
        }

        private void ToolLoginOut_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(CurUser?.UserName))
            {
                new UserService().Delete(CurUser);
                CurUser.CopyValues(new Domain.Models.UserInfo());
                UpdatelblCurUser();
            }
            Login frm = new Login();
            frm.ShowDialog();
            UpdatelblCurUser();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            for(int i = 1; i <= 50; i++)
            {
                new CustomPrint().PrintSetup(new LatticeInfo
                {
                    OrderNo = "productCode" + i
                });
            }

            //var waveService = new WaveApiService();
            //var waveapi = waveService.Get("0727085908waveNo");
            //new CustomPrint().PrintSetup(new LatticeInfo
            //{
            //    OrderNo = "0727085908waveNo"
            //});
            //waveapi.OrderApi.ForEach(o =>
            //{
            //    o.ProductApi(p =>
            //    {
            //        new CustomPrint().PrintSetup(new LatticeInfo
            //        {
            //            OrderNo = o.OrderNo
            //        });
            //    })
               
            //});
            
        }

        private void serialPort_Scan_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            var data = serialPort_Scan.ReadLine();
            data = data.Split('-')[0];
            if (data == "admin")
            {
                ToolSysManage.Visible = true;
            }
            else if (SlaveInfoService.IsComplete)//波次
            {
                SortWaveBegin(data);
            }
            else
            {
                SortProductBegin(data);
            }
        }
    }
}
 