using Domain.Models;
using Service.Model;
using Service.ServiceImp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderSortingBy50.Frm
{
    public partial class CheckHardware : Form
    {
        Socket socketSend;
        Task Task1, Task2, Task3;
        private SysConfig SysConfig { get; set; }//
        public UpDownService UpDownService { get; set; }
        public CheckHardware()
        {
            InitializeComponent();
            SysConfig = new SysConfigService().Get();
            ConnectBoard();
        }

        private void tabCheck_SelectedIndexChanged(object sender, EventArgs e)
        {
           
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
        private void SendMsg(UpDownMessage upDownMessage)
        {
            string data = $"*#{upDownMessage.UpDownCommand}#";

            if (upDownMessage.LatticeByUpDown != null && upDownMessage.LatticeByUpDown.Count != 0)
            {
                upDownMessage.LatticeByUpDown.ForEach(o =>
                {
                    if (upDownMessage.UpDownCommand == UpDownCommand.PutSuccess
                        || upDownMessage.UpDownCommand == UpDownCommand.PutError
                        || upDownMessage.UpDownCommand == UpDownCommand.LatticeComplete
                        || upDownMessage.UpDownCommand == UpDownCommand.RemovePutError)
                    //底层投递成功投递失败等不能传递格口数量
                    {
                        data += $"{o.LatticeNo},";
                    }
                    else
                    {
                        data += $"{o.LatticeNo}&{o.Num},";
                    }
                });
                data = data.Substring(0, data.Length - 1);
            }
            if (string.IsNullOrWhiteSpace(upDownMessage.Message))
            {
                data += $"#*";
            }
            else
            {
                var msgGBK = System.Text.Encoding.GetEncoding("GBK").GetBytes(upDownMessage.Message);
                StringBuilder msg16 = new StringBuilder();
                int ASCIIBegin = 0x01;
                int ASCIIEnd = 0x7F;
                foreach (var s in msgGBK)
                {
                    int target = (int)s;
                    if (ASCIIBegin <= target && target <= ASCIIEnd)
                    {
                        msg16.Append("00");
                    }

                    msg16.Append(Convert.ToString(s, 16));
                }

                data += $"#{string.Join("", msg16)}*";
            }
            byte[] byteArray = System.Text.Encoding.GetEncoding("GBK").GetBytes(data);
            socketSend.Send(byteArray);
        }
        private void ReceiveBoardMsg()
        {
            try
            {
                while (true)
                {
                    byte[] buffer = new byte[1024 * 1024 * 5];
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
                        for (int i = 0; i < commandArr.Count; i++)
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
                                UpDownCommand = (UpDownCommand)Enum.Parse(typeof(UpDownCommand), d[1], true),
                                Type = d[0]
                            };
                            if (upDownMessage.UpDownCommand == UpDownCommand.PutIng)
                            {
                                var latticeNo = upDownMessage.LatticeByUpDown.First().LatticeNo;
                                var upDownMessage1 = new UpDownMessage()
                                {
                                    UpDownCommand = UpDownCommand.PutSuccess,
                                    LatticeByUpDown = new List<LatticeByUpDown>
                                     {
                                        new LatticeByUpDown {LatticeNo=latticeNo,Num=1 }
                                     }
                                };
                                Invoke((MethodInvoker)delegate ()
                                {
                                    var btn = this.Controls.Find("btn" + latticeNo, true)[0] as Button;
                                    btn.BackColor = Color.Gainsboro;
                                });
                                SendMsg(upDownMessage1);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //检测LED
        private void btnCheckLED_Click(object sender, EventArgs e)
        {
            Task1 = Task.Run(() =>
            {
                Enumerable.Range(1, 50).ToList().ForEach(o =>
                {
                    var upDownMessage = new UpDownMessage()
                    {
                        UpDownCommand = UpDownCommand.WaitPut,
                        LatticeByUpDown = new List<LatticeByUpDown>
                         {
                            new LatticeByUpDown {LatticeNo=o.ToString(),Num=1 }
                         }
                    };
                    Invoke((MethodInvoker)delegate ()
                    {
                        var btn = this.Controls.Find("btn" + o.ToString(), true)[0] as Button;
                        btn.BackColor = Color.Green; 
                    });
                    SendMsg(upDownMessage);
                    Thread.Sleep(200);
                });
            });
           
        }

        private void btnPutError_Click(object sender, EventArgs e)
        {
             Task2 = Task.Run(() =>
            {
                Enumerable.Range(1, 50).ToList().ForEach(o =>
                {
                    var upDownMessage = new UpDownMessage()
                    {
                        UpDownCommand = UpDownCommand.PutError,
                        LatticeByUpDown = new List<LatticeByUpDown>
                         {
                            new LatticeByUpDown {LatticeNo=o.ToString(),Num=1 }
                         }
                    };
                    Invoke((MethodInvoker)delegate ()
                    {
                        var btn = this.Controls.Find("btn" + o.ToString(), true)[0] as Button;
                        btn.BackColor = Color.Red;
                    });
                     SendMsg(upDownMessage);
                    Thread.Sleep(200);
                });
            });
        }

        private void btnPutSuccess_Click(object sender, EventArgs e)
        {
            Task3 = Task.Run(() =>
            {
                Enumerable.Range(1, 50).ToList().ForEach(o =>
                {
                    var upDownMessage = new UpDownMessage()
                    {
                        UpDownCommand = UpDownCommand.PutSuccess,
                        LatticeByUpDown = new List<LatticeByUpDown>
                         {
                            new LatticeByUpDown {LatticeNo=o.ToString(),Num=1 }
                         },
                        Type="0"
                    };
                    Invoke((MethodInvoker)delegate ()
                    {
                        var btn = this.Controls.Find("btn" + o.ToString(), true)[0] as Button;
                        btn.BackColor = Color.Gainsboro;
                    });
                     SendMsg(upDownMessage);
                    Thread.Sleep(200);
                });
            });
        }

        private void btnOneKey_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                btnCheckLED_Click(null, null);
                Task1.Wait();
                btnPutError_Click(null, null);
                Task2.Wait();
                btnPutSuccess_Click(null, null);
            });
           
        }
    }
}
