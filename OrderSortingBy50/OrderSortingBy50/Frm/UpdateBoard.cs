using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Domain.Models;
using Service.ServiceImp;
using System.Text.RegularExpressions;

namespace OrderSortingBy50.Frm
{
    public partial class UpdateBoard : Form
    {
        public Socket socketSend { get; private set; }

        private delegate void SetPos(int ipos, string vinfo);
        public UpdateBoard()
        {
            InitializeComponent();
            SysConfig = new SysConfigService().Get();
            cmbType.SelectedIndex = 0;
        }

        public SysConfig SysConfig { get; set; }

        private void SetTextMesssage(int ipos, string vinfo)
        {
            if (this.InvokeRequired)
            {
                SetPos setpos = new SetPos(SetTextMesssage);
                this.Invoke(setpos, new object[] { ipos, vinfo });
            }
            else
            {
                this.label1.Text = ipos.ToString() + "/100";
                this.progressBar1.Value = Convert.ToInt32(ipos);
                this.textBox1.AppendText(vinfo);
            }
        }
        private void Update_Click(object sender, EventArgs e)
        {
            Thread fThread = new Thread(new ThreadStart(SleepT));
            fThread.Start();
        }
        private void SleepT()
        {
            try
            {
                Invoke((MethodInvoker)delegate ()
                {
                    this.textBox1.Clear();
                    btn_Go.Enabled = false;
                });
                bool isSuccess = false;
                for (int i = 0; i < 100; i++)
                {
                    try
                    {
                        isSuccess = ConnectBoard();
                        if (isSuccess) break;
                    }
                    catch { }
                }
                if (!isSuccess) return;
                string path = "";
                Invoke((MethodInvoker)delegate ()
                {
                    if (cmbType.Text == "主机")
                    {
                        path = System.Windows.Forms.Application.StartupPath + @"\App_Data\board.bin";
                    }
                    else if (cmbType.Text == "从机")
                    {
                        path = System.Windows.Forms.Application.StartupPath + @"\App_Data\slave.bin";
                    }
                    else
                    {
                        MessageBox.Show("请选择下载类型");
                    }
                });
               
                FileTool file = new FileTool();
                var data = file.Read(path);
                SetTextMesssage(100 * 1 / 5, "开始发送联机（更新）信号,请将分拨墙重新上电" + "\r\n");
                /*****************************2.发送联机（更新）信号 校验码 4F9 0xF9, 0x4*****************************/
                byte[] write2 = null;
                Invoke((MethodInvoker)delegate ()
                {              
                    if (cmbType.Text == "主机")
                    {
                        write2=new byte [] { 0x55, 0xAA, 0x1C, 0xC1 };
                    }
                    else if (cmbType.Text == "从机")
                    {
                        write2 = new byte[] {0x55, 0xAA, 0x2D, 0xD2};
                    }
                    else
                    {
                        MessageBox.Show("请选择下载类型");
                    }
                });
                if (write2 == null) return;
                write2 = Enumerable.Concat(write2, CLCData(write2)).ToArray();
                SendMsg(write2);
                byte[] result2 = ReceiveMsg();
                if (result2[0] != 0x5F || result2[1] != 0x01 || result2[2] != 0x00 || result2[3] != 0xC1)
                {
                    throw new Exception($"下载数据失败：联机信号应答失败");
                }

                SetTextMesssage(100 * 2 / 5, "发送联机（更新）信号成功" + "\r\n");
                SetTextMesssage(100 * 2 / 5, "开始发送文件大小" + "\r\n");
                /******************************3.发送文件大小 ******************************/
                byte[] fileSize = BitConverter.GetBytes(Convert.ToInt32(data.Length));
                var update3 = new byte[] { 0x55, 0xAA, 0x2C, 0xC2 };
                update3 = Enumerable.Concat(update3, fileSize).ToArray();
                update3 = Enumerable.Concat(update3, CLCData(update3)).ToArray();
                SendMsg(update3);
                byte[] result3 = ReceiveMsg();
                if (result3[0] != 0x5F || result3[1] != 0x01 || result3[2] != 0x00 || result3[3] != 0xC2)
                {
                    throw new Exception($"下载数据失败：发送文件大小应答失败");
                }
                SetTextMesssage(100 * 3 / 5, $"发送文件大小成功" + "\r\n");
                /****************************** 4文件传输******************************/
                //调整数据为1024的倍数
                var dataRe = data.Length % 1024;
                if (dataRe != 0)
                {
                    var dataEx = new byte[1024 - data.Length % 1024];

                    data = Enumerable.Concat(data, dataEx).ToArray();
                }
                int dataLen = data.Length / 1024;

                SetTextMesssage(100 * 3 / 5, $"开始传输文件数据，共{dataLen}步" + "\r\n");
                //5发送数据
                for (int i = 1; i <= dataLen; i++)
                {
                    var writeData = new byte[] { 0x55, 0xAA, 0x3C, 0xC3 };
                    var partData = GetData(data, i);
                    writeData = Enumerable.Concat(writeData, partData).ToArray();
                    var clcData = CLCData(writeData);
                    writeData = Enumerable.Concat(writeData, clcData).ToArray();

                    SendMsg(writeData);
                    byte[] result4 = ReceiveMsg();
                    if (result4[0] != 0x5F || result4[1] != 0x01 || result4[2] != 0x00 || result4[3] != 0xC3)
                    {
                        throw new Exception($"下载数据失败：接收传输数据应答失败:第{i}步");
                    }
                    SetTextMesssage(100 * 3 / 5 + 40 / (data.Length / 1024) * i, $"第{i}步传输文件数据成功" + "\r\n");
                }
                SetTextMesssage(100 * 5 / 5, "更新成功！" + "\r\n");
                socketSend.Close();

            }
            catch (Exception ex)
            {
                Invoke((MethodInvoker)delegate ()
                {
                    btn_Go.Enabled = true;
                });
                socketSend.Close();
                MessageBox.Show(ex.ToString());
            }
        }
        private byte[] GetData(byte[] data, int start, int count = 1024)
        {
            var resultData = new byte[count];
            var index = (start - 1) * count;
            for (int i = 0; i < resultData.Length; i++)
            {
                resultData[i] = data[index + i];
            }
            return resultData;
        }
        /// <summary>
        /// 校验数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private byte[] CLCData(byte[] data)
        {
            int result = Convert.ToInt16(data[0]);
            for (int i = 1; i < data.Length; i++)
            {
                result = result ^ data[i];
            }
            return new byte[] { BitConverter.GetBytes(result)[0] };
        }
        private bool ConnectBoard()
        {
            try
            {
                if (socketSend == null || !socketSend.Connected)
                {

                    socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    //socketSend.SendTimeout = 500;
                    //socketSend.ReceiveTimeout = 500;
                    IPEndPoint point = new IPEndPoint(IPAddress.Parse(SysConfig.IP), Convert.ToInt32(SysConfig.Port));
                    socketSend.Connect(point);
                    Invoke((MethodInvoker) delegate()
                    {
                        label1.Text = "连接分拨墙成功！";
                    });
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private byte[] ReceiveMsg()
        {
            byte[] buffer = new byte[1024 * 1024 * 5];
            int r = socketSend.Receive(buffer);
            return buffer;
        }
        private void SendMsg(byte[] data)
        {
            socketSend.Send(data);
        }

        private void UpdateBoard_FormClosing(object sender, FormClosingEventArgs e)
        {
            socketSend?.Close();
        }
    }
}
