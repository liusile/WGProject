using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Service.IService;
using Service.Model;
using Domain.Models;
using log4net;
using System.Net.Sockets;
using Service.API;

namespace Service.ServiceImp
{
    public class UpDownService
    {
        List<IUpDownService> service=new List<IUpDownService>();
        public OrderAPI OrderAPI = new OrderAPI();
        private SlaveInfoService slaveInfoService;
        private Domain.Models.UserInfo curUser;
        private SysConfig sysConfig;

        public UpDownService(SysConfig sysConfig,SlaveInfoService slaveInfoService, Domain.Models.UserInfo curUser)
        {
            this.slaveInfoService = slaveInfoService;
            this.curUser = curUser;
            this.sysConfig = sysConfig;
        }

        public void AddService(IUpDownService upDownService)
        {
            service.Add(upDownService);
        }
        public void SetSocket(Socket socket)
        {
            service.ForEach(o=>o.SetSocket(socket));
        }
        /// <summary>
        /// 开始分拣
        /// </summary>
        public void BeginSort(SlaveInfo slaveInfo)
        {
            
            Parallel.ForEach(service, o =>o.BeginSort(slaveInfo));
            //WaitPut
            var latticeInfoWaitPut = slaveInfo.LatticeInfo.Where(o => o.Status == LatticeStatus.WaitPut).ToList();
            latticeInfoWaitPut.ForEach(o =>
            {
                var productCodeWaitPut = o.Product.First(p => p.Status == ProductStatus.WaitPut).BarCode;
                WaitPut(latticeInfoWaitPut, productCodeWaitPut);
            });
            
            //BlockError
            var latticeInfoBlockError = slaveInfo.LatticeInfo.Where(o => o.Status == LatticeStatus.BlockError).ToList();
            if (latticeInfoBlockError.Count > 0)
            {
                BlockError(new UpDownMessage()
                {
                    LatticeByUpDown = latticeInfoBlockError.Select(o => new LatticeByUpDown()
                    {
                        LatticeNo = o.LatticeNo
                    }).ToList()
                });
            }
            //PutError
            var latticeInfoPutError = slaveInfo.LatticeInfo.Where(o => o.Status == LatticeStatus.PutError).ToList();
            if (latticeInfoPutError.Count > 0)
            {
                PutError(new UpDownMessage()
                {
                    LatticeByUpDown = latticeInfoPutError.Select(o => new LatticeByUpDown()
                    {
                        LatticeNo = o.LatticeNo
                    }).ToList()
                });
            }
            //LatticeComplete
            var latticeInfoComplete = slaveInfo.LatticeInfo.Where(o => o.IsComplete && o.Product.Count>0).ToList();
            if (latticeInfoComplete.Count > 0)
            {
                LatticeComplete(new UpDownMessage()
                {
                    LatticeByUpDown = latticeInfoComplete.Select(o => new LatticeByUpDown()
                    {
                        LatticeNo = o.LatticeNo
                    }).ToList()
                });
            }
        }
        /// <summary>
        /// 扫描已完成的波次，错误
        /// </summary>
        public void WaveOverError(string waveNo)
        {
            Parallel.ForEach(service, o => o.WaveOverError(waveNo));
            SetText($"该波次[{waveNo}]已完成");
        }

        public void WaveNotfound(string wave)
        {
            var upDownMessage = new UpDownMessage()
            {
                UpDownCommand = UpDownCommand.WaveNotfound,
                Message = $"未找到波次号[{wave}]"
            };

            Parallel.ForEach(service, o => o.WaveNotfound(upDownMessage));
        }

        public void WaitPut(List<LatticeInfo> latticeInfoList,string barCode)
        {
            var result = Parallel.ForEach(service, o => o.WaitPut(latticeInfoList, barCode));
        }

        public void PutIng(UpDownMessage upDownMessage)
        {
            upDownMessage.LatticeByUpDown.ForEach(o =>
            {
                //过滤
                var latticeInfo = slaveInfoService.GetLatticeInfo(o.LatticeNo);
                var waitputNum = latticeInfo.Product.Where(p => p.Status == ProductStatus.WaitPut).Sum(t => t.WaitNum - t.PutNum);
                if (upDownMessage.Type == "1" && waitputNum > 1)//1代表光栅投递
                {
                    
                }else if (waitputNum < upDownMessage.LatticeByUpDown.First().Num)
                {
                    //upDownMessage.Message = $"超出可投数：{waitputNum}";
                    //PutNumError(upDownMessage);
                }
                else if (latticeInfo.Status != LatticeStatus.WaitPut && slaveInfoService.SlaveInfo.LatticeStatus==LatticeStatus.WaitPut)
                {
                    upDownMessage.Message = $"格口[{latticeInfo.LatticeNo}]投递出错";
                    PutError(upDownMessage);
                }
                else if(latticeInfo.Status == LatticeStatus.WaitPut && slaveInfoService.SlaveInfo.LatticeStatus == LatticeStatus.WaitPut)
                {
                    PutSuccess(upDownMessage);
                }
            });
        }

        private void PutNumError(UpDownMessage upDownMessage)
        {
            upDownMessage.UpDownCommand = UpDownCommand.PutNumError;
            Parallel.ForEach(service, o => o.PutNumError(upDownMessage));
        }

        public void PutError(UpDownMessage upDownMessage)
        {
            upDownMessage.UpDownCommand = UpDownCommand.PutError;
            upDownMessage.Message = $"格口[{upDownMessage.LatticeByUpDown.First().LatticeNo}]投递出错";
            Parallel.ForEach(service, o => o.PutError(upDownMessage));
        }

        public void RemovePutError(UpDownMessage upDownMessage)
        {
            upDownMessage.UpDownCommand = UpDownCommand.RemovePutError;
            upDownMessage.Message = $"格口[{upDownMessage.LatticeByUpDown.First().LatticeNo}]已解除";
            Parallel.ForEach(service, o => o.RemovePutError(upDownMessage));
        }

        public void PutSuccess(UpDownMessage upDownMessage)
        {
            upDownMessage.UpDownCommand = UpDownCommand.PutSuccess;
            upDownMessage.Message = $"投递成功";

            Parallel.ForEach(service, o => o.PutSuccess(upDownMessage));
            //格口是否完成
            upDownMessage.LatticeByUpDown.ForEach(o =>
            {
                var latticeInfo = slaveInfoService.GetLatticeInfo(o.LatticeNo);
                if (latticeInfo.IsComplete)
                {
                    LatticeComplete(new UpDownMessage()
                    {
                        LatticeByUpDown = new List<LatticeByUpDown>()
                        {
                            new LatticeByUpDown() {LatticeNo = o.LatticeNo},

                        },
                        Message = $"格口[{latticeInfo.LatticeNo}]已完成"
                    });
                }
            });
            //波次是否完成
            if (slaveInfoService.IsComplete)
            {
                WaveComplete(new UpDownMessage { Message = $"完成波次：{slaveInfoService.SlaveInfo.WaveNo}" });
            }
        }

        public void LatticeComplete(UpDownMessage upDownMessage)
        {
            upDownMessage.UpDownCommand = UpDownCommand.LatticeComplete;
            upDownMessage.Message = $"格口[{upDownMessage.LatticeByUpDown.First().LatticeNo}]已完成";
            Parallel.ForEach(service, o => o.LatticeComplete(upDownMessage));
        }

        public void BlockError(UpDownMessage upDownMessage)
        {
            upDownMessage.UpDownCommand = UpDownCommand.BlockError;

            upDownMessage.LatticeByUpDown.ForEach(o =>
            {
                var latticeInfo = slaveInfoService.GetLatticeInfo(o.LatticeNo);
                if (latticeInfo.Status == LatticeStatus.WaitPut && latticeInfo.Product.Where(p => p.Status == ProductStatus.WaitPut).Sum(t => t.WaitNum - t.PutNum) == 1)
                {
                    PutSuccess(new UpDownMessage
                    {
                        LatticeByUpDown=new List<LatticeByUpDown>
                        {
                            new LatticeByUpDown {LatticeNo= latticeInfo.LatticeNo,Num=1}
                        }
                    });
                }else
                {
                    BlockErrorSuccess(upDownMessage); 
                }    
            });  
        }
        public void BlockErrorSuccess(UpDownMessage upDownMessage)
        {
            upDownMessage.UpDownCommand = UpDownCommand.BlockErrorSuccess;

            Parallel.ForEach(service, o => o.BlockErrorSuccess(upDownMessage));
           
            SetText($"格口[{upDownMessage.LatticeByUpDown.First().LatticeNo}]阻挡");
        }

        public void RemoveBlockError(UpDownMessage upDownMessage)
        {
            upDownMessage.UpDownCommand = UpDownCommand.RemoveBlockError;
           
            RemoveBlockErrorSuccess(upDownMessage);
        }
        public void RemoveBlockErrorSuccess(UpDownMessage upDownMessage)
        {
            upDownMessage.UpDownCommand = UpDownCommand.RemoveBlockErrorSuccess;

            Parallel.ForEach(service, o => o.RemoveBlockErrorSuccess(upDownMessage));
            upDownMessage.LatticeByUpDown.ForEach(o =>
            {
                var latticeInfo = slaveInfoService.GetLatticeInfo(o.LatticeNo);
                if (latticeInfo.Product.Any(p => p.Status == ProductStatus.WaitPut))
                {
                    WaitPut(new List<LatticeInfo> { latticeInfo }, latticeInfo.Product.First(p=>p.Status==ProductStatus.WaitPut).BarCode);
                }
              
            });
            SetText($"格口[{upDownMessage.LatticeByUpDown.First().LatticeNo}]已解除");

        }
        public void PrintSingle(UpDownMessage upDownMessage)
        {
            if (sysConfig.PrintType == PrintType.一次性打印)
                return;
            upDownMessage.UpDownCommand = UpDownCommand.PrintSingle;

            Parallel.ForEach(service, o => o.PrintSingle(upDownMessage));

            SetText($"格口[{upDownMessage.LatticeByUpDown.First().LatticeNo}]已打印");
            //波次是否完成
            if (slaveInfoService.IsComplete)
            {
                if (sysConfig.PrintType == PrintType.逐个打印 && slaveInfoService.LatticeInfoList.All(o=>o.IsPrint))
                {
                    WaveComplete(new UpDownMessage { });
                }
               
            }
            if (slaveInfoService.SlaveInfo.IsPrintOver)
            {
                WavePrintOver();
            }

        }

        public void PrintAll(UpDownMessage upDownMessage)
        {
            if (sysConfig.PrintType == PrintType.逐个打印)
                return;
            if (!slaveInfoService.IsComplete)
                return;
            upDownMessage.UpDownCommand = UpDownCommand.PrintAll;

            Parallel.ForEach(service, o => o.PrintAll(upDownMessage));
            WavePrintOver();
            SetText($"已全部打印");
        }

        public void WaveCancel(UpDownMessage upDownMessage)
        {
            upDownMessage.UpDownCommand = UpDownCommand.WaveCancel;
            bool isSuccess = slaveInfoService.DeleteSlaveInfo();
            if (isSuccess)
            {
                WaveCancelSuccess();
            }else
            {
                WaveCancelError();
            }  
        }

        public void WaveComplete(UpDownMessage upDownMessage)
        {
            upDownMessage.UpDownCommand = UpDownCommand.WaveComplete;
            var data = slaveInfoService.GetSortingDetail().Where(o => o.WaitNum > o.PutNum).ToList();
            if (OrderAPI.CompleteWaveAPI(sysConfig.DomainName,slaveInfoService.SlaveInfo.WaveNo , data, curUser.UserId))
            {
                Parallel.ForEach(service, o => o.WaveComplete(upDownMessage));

                SetText($"完成波次：{slaveInfoService.SlaveInfo.WaveNo}");
            }
            else
            {
                SetText($"接口报错：CompleteWaveAPI");
            }
        }

        public void WavePowerComplete(UpDownMessage upDownMessage)
        {
            upDownMessage.UpDownCommand = UpDownCommand.WavePowerComplete;
            var data = slaveInfoService.GetSortingDetail().Where(o => o.WaitNum > o.PutNum).ToList();
            if (OrderAPI.CompleteWaveAPI(sysConfig.DomainName, slaveInfoService.SlaveInfo.WaveNo , data , curUser.UserId))
            {
                Parallel.ForEach(service, o => o.WavePowerCompleteSuccess(upDownMessage));
            }
            else
            {
                SetText($"接口报错：CompleteWaveAPI");
            }
        }
        public void WavePowerCompleteSuccess(UpDownMessage upDownMessage)
        {
            upDownMessage.UpDownCommand = UpDownCommand.WavePowerCompleteSuccess;

            Parallel.ForEach(service, o => o.WavePowerCompleteSuccess(upDownMessage));
            SetText("强制完成成功");
        }
        public void LoginSuccess()
        {
            Parallel.ForEach(service, o => o.LoginSuccess(new UpDownMessage {
                UpDownCommand=UpDownCommand.LoginSuccess
            }));
        }

        public void ProductNotFound()
        {
            Parallel.ForEach(service, o => o.ProductNotFound(new UpDownMessage
            {
                UpDownCommand = UpDownCommand.ProductNotFound
            }));
            SetText("该波次产品不存在");
        }
        public void WaveCancelSuccess()
        {
            Parallel.ForEach(service, o => o.WaveCancelSuccess(new UpDownMessage
            {
                UpDownCommand = UpDownCommand.WaveCancelSuccess,
                Message="分播中断成功"
            }));
        }
        public void WaveCancelError()
        {
            Parallel.ForEach(service, o => o.WaveCancelError(new UpDownMessage
            {
                UpDownCommand = UpDownCommand.WaveCancelError,
                Message = "分播中断失败"
            }));
        }
        public void SetText(string msg)
        {
            Parallel.ForEach(service, o => o.SetText(new UpDownMessage
            {
                UpDownCommand = UpDownCommand.SetText,
                Message= msg
            }));
        }
        public void WavePrintOver(string msg= "波次已全部打印")
        {
            Parallel.ForEach(service, o => o.WavePrintOver(new UpDownMessage
            {
                UpDownCommand = UpDownCommand.WavePrintOver,
                Message = msg
            }));
        }
        
    }
}
