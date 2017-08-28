using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Models;
using Service.ServiceImp;
using Service.API;

namespace CustomTask
{
    public  class OrderDownloadTask
    {
        public OrderDownloadTask()
        {
            WaveApiService = new WaveApiService();
            OrderAPI = new OrderAPI();
        }

        public WaveApiService WaveApiService { get; set; }
        public OrderAPI OrderAPI { get; set; }

        public void RunSync(SysConfig sysConfig, UserInfo userInfo, CancellationToken cancellationToken)
        {
            Task.Run(() => DownOrderLoop(sysConfig, userInfo),cancellationToken);
        }

        public void DownOrderLoop(SysConfig sysConfig, UserInfo userInfo)
        {
            while (true)
            {
                // DownOrder();
                DownWave(sysConfig.DomainName, userInfo?.Pcid);
               // DownWave(sysConfig.DomainName,"1039");// DownWave(sysConfig.DomainName, userInfo?.Pcid);
                Thread.Sleep(TimeSpan.FromMinutes(10));
            }
        }
        public void DownOrder()
        {
            var random = new Random();
            string flow = DateTime.Now.ToString("MMddHHmmss");
            var wave = new WaveApi()
            {
                LastTime = DateTime.Now,
                Status = 0,
                WaveNo = flow + "waveNo",
                OrderApi = new List<OrderApi>()
            };
            for(int i = 1; i <= 1; i++)
            {
                wave.OrderApi.Add(new OrderApi
                {
                    OrderNo = flow + "orderNo"+i,
                    Status = OrderStatus.Normal,
                    ProductApi = new List<ProductApi> {
                         new ProductApi {Num=random.Next(1,3),ProductCode="0727085908orderNo"+random.Next(1,50),ProductName="ProductName" }
                     }
                });
            }
            bool ifds=WaveApiService.AddOrUpdate(wave);
        }

        public void DownWave(string domainName,string Processcenterid)
        {
            if (string.IsNullOrWhiteSpace(domainName) || string.IsNullOrWhiteSpace(Processcenterid))
                return;
            
            DateTime DateS = WaveApiService.GetLastTime();
            DateTime DateE = DateTime.Now;
            var data = OrderAPI.GetWaveAPI(domainName,Processcenterid, DateS, DateE);
            if(data!=null)
                 WaveApiService.UpdateWaveApi(data);
        }
    }
}
