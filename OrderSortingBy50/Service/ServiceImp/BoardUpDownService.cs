using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Service.IService;
using Service.Model;
using log4net;
using System.Threading;

namespace Service.ServiceImp
{
    public class BoardUpDownService : IUpDownService
    {
        public SlaveInfoService SlaveInfoService { get; set; }
        public Socket Socket { get;  set; }
        ILog log = LogManager.GetLogger("log");

        public BoardUpDownService(SlaveInfoService slaveInfoService)
        {
            SlaveInfoService = slaveInfoService;
        }

        public void LoginSuccess(UpDownMessage upDownMessage)
        {
            
        }

        public void BeginSort(SlaveInfo slaveInfo)
        {
            var upDownMessage = new UpDownMessage
            {
                UpDownCommand = UpDownCommand.BeginSort,
                Message = $"开始波次"
            };
            SendMsg(upDownMessage);
        }

        public void WaveOverError(string waveNo)
        {
           
        }

        public void WaveNotfound(UpDownMessage upDownMessage)
        {
            SendMsg(upDownMessage);
        }

        public void WaitPut(List<LatticeInfo> latticeInfoList, string barCode)
        {
            var upDownMessage = new UpDownMessage
            {
                UpDownCommand = UpDownCommand.WaitPut,
                LatticeByUpDown=new List<LatticeByUpDown>(),
                Message = "待投递"
            };
            latticeInfoList.ForEach(o =>
            {
                if (!o.Product.First(p => p.BarCode == barCode).IsComplete)
                {
                    upDownMessage.LatticeByUpDown.Add(new LatticeByUpDown
                    {
                        LatticeNo = o.LatticeNo,
                        Num = o.Product.Where(p => p.BarCode == barCode).Sum(t => t.WaitNum - t.PutNum)
                    });
                }
            });
            SendMsg(upDownMessage);
        }

        public void PutIng(UpDownMessage upDownMessage)
        {
            
        }

        public void PutError(UpDownMessage upDownMessage)
        {
            SendMsg(upDownMessage);
        }

        public void RemovePutError(UpDownMessage upDownMessage)
        {
            SendMsg(upDownMessage);
        }

        public void PutSuccess(UpDownMessage upDownMessage)
        {
            SendMsg(upDownMessage);
        }

        public void LatticeComplete(UpDownMessage upDownMessage)
        {
            SendMsg(upDownMessage);
        }

        public void BlockError(UpDownMessage upDownMessage)
        {
            
        }

        public void RemoveBlockError(UpDownMessage upDownMessage)
        {
           
        }

        public void PrintSingle(UpDownMessage upDownMessage)
        {
            
        }

        public void PrintAll(UpDownMessage upDownMessage)
        {
            
        }

        public void WaveCancel(UpDownMessage upDownMessage)
        {
            
        }

        public void WaveComplete(UpDownMessage upDownMessage)
        {
            SendMsg(upDownMessage);
        }

        public void WavePowerComplete(UpDownMessage upDownMessage)
        {
           
        }

        public void SetSocket(Socket socket)
        {
            this.Socket = socket;
        }
        private void SendMsg(UpDownMessage upDownMessage)
        {
            upDownMessage.Message = upDownMessage.Message + $"【{SlaveInfoService.SlaveInfo.NeedWaitPutByApi}:{SlaveInfoService.SlaveInfo.PutNum}】";
            string data = $"*#{upDownMessage.UpDownCommand}#";

            if (upDownMessage.LatticeByUpDown != null && upDownMessage.LatticeByUpDown.Count != 0)
            {
                upDownMessage.LatticeByUpDown.ForEach(o =>
                {
                    if (upDownMessage.UpDownCommand==UpDownCommand.PutSuccess 
                        || upDownMessage.UpDownCommand == UpDownCommand.PutError 
                        || upDownMessage.UpDownCommand == UpDownCommand.LatticeComplete
                        || upDownMessage.UpDownCommand == UpDownCommand.BlockErrorSuccess
                        || upDownMessage.UpDownCommand == UpDownCommand.RemoveBlockErrorSuccess
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
                data = data.Substring(0,data.Length-1);
            }
            if (string.IsNullOrWhiteSpace(upDownMessage.Message))
            {
                data += $"#*";
            }
            else
            {
                var msgGBK =  System.Text.Encoding.GetEncoding("GBK").GetBytes(upDownMessage.Message);
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
            Socket.Send(byteArray);
            log.Debug(data);
        }

        private void LogSaveAync(string data)
        {
            Task.Run(() =>
            {
                log.Debug(data);
            });
        }

        public void ProductNotFound(UpDownMessage upDownMessage)
        {
            
        }

        public void WaveCancelSuccess(UpDownMessage upDownMessage)
        {
            SendMsg(upDownMessage);
        }

        public void WaveCancelError(UpDownMessage upDownMessage)
        {
           
        }

        public void PutNumError(UpDownMessage upDownMessage)
        {
           
        }

        public void SetText(UpDownMessage upDownMessage)
        {
            SendMsg(upDownMessage);
        }

        public void BlockErrorSuccess(UpDownMessage upDownMessage)
        {
            SendMsg(upDownMessage);
        }

        public void RemoveBlockErrorSuccess(UpDownMessage upDownMessage)
        {
            SendMsg(upDownMessage);
        }

        public void WavePowerCompleteSuccess(UpDownMessage upDownMessage)
        {
            UpDownMessage udm = new UpDownMessage
            {
                UpDownCommand = UpDownCommand.WavePowerCompleteSuccess,
                LatticeByUpDown = new List<LatticeByUpDown>(),
                Message = "强制完成成功"
            };

            var latticeList = SlaveInfoService.LatticeInfoList;
            latticeList.ForEach(o =>
            {
                udm.LatticeByUpDown.Add(new LatticeByUpDown
                {
                    LatticeNo=o.LatticeNo,
                    Num=o.WaitNum-o.PutNum
                });
            });
            SendMsg(udm);
        }
        public void WavePrintOver(UpDownMessage upDownMessage)
        {
            SendMsg(upDownMessage);
        }
    }
}
