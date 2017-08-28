using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderSortingBy50.Frm
{
    public partial class BoardInit : Form
    {
        public BoardInit()
        {
            InitializeComponent();
        }

        private void btnCom_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen)
                {
                    serialPort1.Close();
                }
                serialPort1.PortName = txtCOM.Text.Trim();
                serialPort1.Open();
                Msg.Text += "连接成功";
            }
            catch(Exception ex)
            {
                Msg.Text += "连接失败";
            }
        }
        /// <summary>
        /// 设置从机地址（01-50）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSlaveComfirm_Click(object sender, EventArgs e)
        {
            string seq = txtSeq.Text.Trim();
            string content = $"*#setSlaveAddress#{seq}#*";
            SetMsg(content, MsgType.Send);
            serialPort1.Write(content);
           // byte[] buffer = new byte[28];
            //int i = serialPort1.Read(buffer, 0, buffer.Length);
            //while(i< buffer.Length)
            //{
            //    serialPort1.Read(buffer, i, buffer.Length-i);
            //}
            //string strMsg = Encoding.GetEncoding("GBK").GetString(buffer);
            //SetMsg(strMsg, MsgType.Recivice);
            txtSeq.Text = (int.Parse(txtSeq.Text.Trim()) + 1).ToString().PadLeft(2, '0');
        }
        private void SetMsg(string content,MsgType MsgType)
        {
            Invoke((MethodInvoker)delegate ()
            {
                if (MsgType == MsgType.Send)
                {
                    Msg.Text += "发送指令：" + content;
                }
                else
                {
                    Msg.Text += "接收指令：" + content;
                }

            });
            
        }
        /// <summary>
        /// 设置IP地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnServiceIPComfirm_Click(object sender, EventArgs e)
        {
            string ip = txtServiceIP.Text.Trim();
            string content = $"*#setServerIpAddress#{ip}#*";
            SetMsg(content, MsgType.Send);
            serialPort1.Write(content);
           // byte[] buffer = new byte[42];
           // int i = serialPort1.Read(buffer, 0, buffer.Length);
            //while (i < buffer.Length)
            //{
            //    serialPort1.Read(buffer, i, buffer.Length - i);
            //}
            //string strMsg = Encoding.GetEncoding("GBK").GetString(buffer);
            //SetMsg(strMsg, MsgType.Recivice);
        }
        /// <summary>
        /// 设置网关地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNetGateComfirm_Click(object sender, EventArgs e)
        {
            string ip = txtNetGateIP.Text.Trim();
            string content = $"*#setNetGateWay#{ip}#*";
            SetMsg(content, MsgType.Send);
            serialPort1.Write(content);
           // byte[] buffer = new byte[35];
           // int i = serialPort1.Read(buffer, 0, buffer.Length);
            //while (i < buffer.Length)
            //{
            //    serialPort1.Read(buffer, i, buffer.Length - i);
            //}
            //string strMsg = Encoding.GetEncoding("GBK").GetString(buffer);
            //SetMsg(strMsg, MsgType.Recivice);
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            byte[] buffer = new byte[1024];
            int i = serialPort1.Read(buffer, 0, buffer.Length);
            string strMsg = Encoding.GetEncoding("GBK").GetString(buffer);
            SetMsg(strMsg, MsgType.Recivice);
        }
    }

    internal enum MsgType
    {
        Send,
        Recivice
    }
}
