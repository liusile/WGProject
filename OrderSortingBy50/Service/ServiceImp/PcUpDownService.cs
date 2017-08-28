using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Domain.Models;
using Service.IService;
using Service.Model;
using static System.Windows.Forms.Form;
using Service.API;

namespace Service.ServiceImp
{
    public  class PcUpDownService:IUpDownService
    {
        public SlaveInfoService SlaveInfoService { get; set; }
        public SlaveInfoService s { get; set; }
        public ButtonService ButtonService { get; private set; }
        public SysConfig SysConfig { get; private set; }
        public Domain.Models.UserInfo UserInfo { get;  set; }

        public OrderAPI OrderAPI  = new OrderAPI();

        public SoundService SoundService = new SoundService();
        private SynchronizationContext current;


        public PcUpDownService(SlaveInfoService slaveInfoService, ButtonService buttonService,SynchronizationContext current, SysConfig SysConfig, Domain.Models.UserInfo UserInfo)
        {
            this.SlaveInfoService = slaveInfoService;
            this.ButtonService = buttonService;
            this.SysConfig = SysConfig;
            this.current = current;
            this.UserInfo = UserInfo;
        }

        public void LatticeWaitPut(UpDownMessage upDownMessage)
        {
            upDownMessage.LatticeByUpDown.ForEach(o =>
            {
                SlaveInfoService.LatticeInfoList.First(l => l.LatticeNo == o.LatticeNo).Status =
                    LatticeStatus.WaitPut;
            });
            UpdateButton(upDownMessage);
            
        }
        /// <summary>
        /// 投递成功
        /// </summary>
        /// <param name="upDownMessage"></param>
        public void LatticePutSuccess(UpDownMessage upDownMessage)
        {
            upDownMessage.LatticeByUpDown.ForEach(o =>
            {
                SlaveInfoService.LatticeInfoList.First(l => l.LatticeNo == o.LatticeNo).Status =
                    LatticeStatus.None;
            });
            
        }
        public void BeginSort(SlaveInfo slaveInfo)
        {
            SlaveInfoService.SlaveInfo = slaveInfo;
            UpdateButton();
            SoundService.PlaySoundAsny(SoundType.BeginSort);
        }

        public void WaveOverError(string waveNo)
        {
            SoundService.PlaySoundAsny(SoundType.WaveOverError);
        }

        public void WaveNotfound(UpDownMessage upDownMessage)
        {
            SoundService.PlaySoundAsny(SoundType.WaveNotfound);
        }

        public void WaitPut(List<LatticeInfo> latticeInfoList, string barCode)
        {
            latticeInfoList.ForEach(o =>
            {
                if (o.Product.Exists(p => p.BarCode == barCode && !p.IsComplete))
                {
                    SlaveInfoService.SetLatticeStatus(o, LatticeStatus.WaitPut);
                    SlaveInfoService.SetProductStatus(o, barCode, ProductStatus.WaitPut);
                }
            });
            current.Post((o) => { ButtonService.UpdateButton(latticeInfoList); }, null);
            SlaveInfoService.SaveAync();
        }

        public void PutIng(UpDownMessage upDownMessage)
        {
           
        }

        public void PutError(UpDownMessage upDownMessage)
        {
            upDownMessage.LatticeByUpDown.ForEach(o =>
            {
                var latticeInfo = SlaveInfoService.GetLatticeInfo(o.LatticeNo);
                SlaveInfoService.SetLatticeStatus(latticeInfo, LatticeStatus.PutError);
            });
            UpdateButton(upDownMessage);
            SlaveInfoService.SaveAync();
            SoundService.PlaySoundAsny(SoundType.PutError);
        }

        public void RemovePutError(UpDownMessage upDownMessage)
        {
            upDownMessage.LatticeByUpDown.ForEach(o =>
            {
                var latticeInfo = SlaveInfoService.GetLatticeInfo(o.LatticeNo);
                SlaveInfoService.SetLatticeStatus(latticeInfo, LatticeStatus.None);
            });
            UpdateButton(upDownMessage);
            SlaveInfoService.SaveAync();
            SoundService.PlaySoundAsny(SoundType.RemovePutError);
        }

        public void PutSuccess(UpDownMessage upDownMessage)
        {
            upDownMessage.LatticeByUpDown.ForEach(o =>
            {
                var tempPutNum = o.Num;
                var latticeInfo = SlaveInfoService.GetLatticeInfo(o.LatticeNo);
                latticeInfo.Product
                .FindAll(p=>p.Status == ProductStatus.WaitPut)
                .ForEach(p =>
                {
                    var MaxWaitPut = p.WaitNum - p.PutNum;
                    if (MaxWaitPut >= tempPutNum)
                    {
                        p.PutNum += tempPutNum;
                    }
                    else
                    {
                        p.PutNum += MaxWaitPut;
                        tempPutNum -= MaxWaitPut;
                    }
                });
                //latticeInfo.Product.First(p=>p.Status==ProductStatus.WaitPut).PutNum += o.Num;
                SlaveInfoService.SetLatticeStatus(latticeInfo, LatticeStatus.None);
                SlaveInfoService.ProductTransformNone(latticeInfo);
            });
            UpdateButton(upDownMessage);
            SlaveInfoService.SaveAync();
        }

        public void LatticeComplete(UpDownMessage upDownMessage)
        {
            
        }

        public void BlockError(UpDownMessage upDownMessage)
        {
            
        }

        public void RemoveBlockError(UpDownMessage upDownMessage)
        {
           
        }

        public void PrintSingle(UpDownMessage upDownMessage)
        {

            upDownMessage.LatticeByUpDown.ForEach(o =>
            {
                var latticeInfo = SlaveInfoService.GetLatticeInfo(o.LatticeNo);
                if (!string.IsNullOrWhiteSpace(latticeInfo.OrderNo))
                {
                    latticeInfo.IsPrint = true;
                    SlaveInfoService.SaveAync();
                    if (SysConfig.PrintType == PrintType.逐个打印)
                    {
                        new CustomPrint().PrintSetup(latticeInfo);  
                    }
                }
            });
        }

        public void PrintAll(UpDownMessage upDownMessage)
        {
            if (SysConfig.PrintType == PrintType.一次性打印)
            {
               
                SlaveInfoService?.LatticeInfoList?.ForEach(o =>
                {
                    var latticeInfo = SlaveInfoService.GetLatticeInfo(o.LatticeNo);
                    if (!string.IsNullOrWhiteSpace(latticeInfo.OrderNo))
                    {
                        latticeInfo.IsPrint = true;
                        SlaveInfoService.SaveAync();
                        new CustomPrint().PrintSetup(latticeInfo);
                    }
                });
            }
        }

        public void WaveCancel(UpDownMessage upDownMessage)
        {

        }
        static int num = 6;
        public void WaveComplete(UpDownMessage upDownMessage)
        {
            new WaveApiService().Complete(SlaveInfoService.SlaveInfo.WaveNo);
            FlashGreenLight();
            SoundService.PlaySoundAsny(SoundType.WaveComplete);
        }
      
        public void WavePowerComplete(UpDownMessage upDownMessage)
        {
           
        }

        public void LoginSuccess(UpDownMessage upDownMessage)
        {
            SoundService.PlaySoundAsny(SoundType.LoginSuccess);
        }

        private void UpdateButton(UpDownMessage upDownMessage = null)
        {
            if (upDownMessage == null)
            {
                current.Post((o) => { ButtonService.UpdateButtons(); }, null);
            }
            else
            {
                current.Post((o) => { ButtonService.UpdateButton(upDownMessage); }, null); 
            }  
        }

        public void SetSocket(Socket socket)
        {
           
        }

        private void FlashGreenLight()
        {
            System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 200;
            timer1.Enabled = true;
            timer1.Tick += new EventHandler((s, o) =>
            {
                if (num % 2 == 0)
                {
                    ButtonService.GainsboroAll();
                }
                else
                {
                    ButtonService.GreenAll();
                }
                if (--num < 0)
                {
                    num = 6;
                    timer1.Stop();
                    timer1.Dispose();
                }
            });
        }

        public void ProductNotFound(UpDownMessage upDownMessage)
        {
            SoundService.PlaySoundAsny(SoundType.ProductNotFound);
        }

        public void WaveCancelSuccess(UpDownMessage upDownMessage)
        {
            SlaveInfoService.InitSlaveInfo();
            SoundService.PlaySoundAsny(SoundType.WaveCancelSuccess);
        }

        public void WaveCancelError(UpDownMessage upDownMessage)
        {
            SoundService.PlaySoundAsny(SoundType.WaveCancelError);
        }

        public void PutNumError(UpDownMessage upDownMessage)
        {
            SoundService.PlaySoundAsny(SoundType.PutNumError);
        }

        public void SetText(UpDownMessage upDownMessage)
        {
            
        }

        public void BlockErrorSuccess(UpDownMessage upDownMessage)
        {
           
            upDownMessage.LatticeByUpDown.ForEach(o =>
            {
                var latticeInfo = SlaveInfoService.GetLatticeInfo(o.LatticeNo);
                SlaveInfoService.SetLatticeStatus(latticeInfo, LatticeStatus.BlockError);
            });
            UpdateButton(upDownMessage);
            SoundService.PlaySoundAsny(SoundType.BlockError);  
        }

        public void RemoveBlockErrorSuccess(UpDownMessage upDownMessage)
        {
            upDownMessage.LatticeByUpDown.ForEach(o =>
            {
                var latticeInfo = SlaveInfoService.GetLatticeInfo(o.LatticeNo);
                SlaveInfoService.SetLatticeStatus(latticeInfo, LatticeStatus.None);
            });
            UpdateButton(upDownMessage);
            SoundService.PlaySoundAsny(SoundType.RemoveBlockError);
        }
        public void WavePrintOver(UpDownMessage upDownMessage)
        {
            SoundService.PlaySoundAsny(SoundType.WavePrintOver);
        }
        public void WavePowerCompleteSuccess(UpDownMessage upDownMessage)
        {
            SlaveInfoService.SlaveInfo.IsCompleteHand = true;
            SlaveInfoService.SaveAync();
            new WaveApiService().Complete(SlaveInfoService.SlaveInfo.WaveNo);
            //打印缺货
            var data  = SlaveInfoService.GetSortingDetail().Where(o=>o.WaitNum>o.PutNum).ToList();
            var lattieInfo = new LatticeInfo
            {
                Product = new List<Product>()
            };
            for (int i = 0; i < data.Count; i++)
            {
                lattieInfo.Product.Add(new Product
                {
                    ProductName=data[i].OrderNo,
                    BarCode = data[i].BarCode,
                    PutNum = data[i].PutNum,
                    WaitNum = data[i].WaitNum
                });
                lattieInfo.OrderNo = data[i].OrderNo;
               
                    new ProductPrint().PrintSetup(lattieInfo);
                    lattieInfo.Product.Clear();
               
            }
            if (lattieInfo.Product.Count > 0)
            {
                new ProductPrint().PrintSetup(lattieInfo);
            }
            // SlaveInfoService.InitSlaveInfo();
            SoundService.PlaySoundAsny(SoundType.WavePowerComplete);
            FlashGreenLight();
        }
    }
}
