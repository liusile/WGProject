using Domain;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceImp
{
    public class WaveApiService
    {
        public WaveApi Get(string waveNo)
        {
            using (var db = new SortingDbContext())
            {
                var wave = db.WaveApi.Include("OrderApi").Include("OrderApi.ProductApi").FirstOrDefault(o=>o.WaveNo == waveNo);
                if(wave==null)
                {
                    return null;
                }
                if (wave.OrderApi.Exists(o => o.Status == OrderStatus.Normal))
                {
                    return wave;
                }
                else
                {
                    return null;
                }
            }
        }
        public DateTime GetLastTime()
        {
            using (var db = new SortingDbContext())
            {
                var waveApi = db.WaveApi.OrderByDescending(o=>o.LastTime).FirstOrDefault();
                if (waveApi == null)
                {
                    return new DateTime(2017, 8, 27);
                }
                else
                {
                    return waveApi.LastTime??new DateTime(2017,8,27);
                }
            }
        }
        public bool AddOrUpdate(WaveApi waveList)
        {
            using (var db = new SortingDbContext())
            {
                db.WaveApi.AddOrUpdate(waveList);
                return db.SaveChanges() > 0 ;
            }
        }
        public bool AddOrUpdate(List<WaveApi> waveList)
        {
            using (var db = new SortingDbContext())
            {
                db.WaveApi.AddOrUpdate(waveList.ToArray());

                return db.SaveChanges() > 0 ;
            }
        }
        public int Count()
        {
            using (var db = new SortingDbContext())
            {
                return db.WaveApi.Count();
            }
        }
        public SlaveInfo WaveApiTransformSlaveInfo(WaveApi wave)
        {
            using (var db = new SortingDbContext())
            {
                var latticeInfoList = new List<LatticeInfo>();
                Enumerable.Range(1, 50).ToList().ForEach(o => latticeInfoList.Add(
                    new LatticeInfo
                    {
                        LatticeNo = o.ToString(),
                        Status = LatticeStatus.None,
                        Product = new List<Product>()
                    }
                ));
                //1.更新WaveApi为已作业
                wave.Status = WaveStatus.Work;
                db.WaveApi.AddOrUpdate(wave);
                //2.载入到SlaveInfo
                var slaveInfo = db.SlaveInfo.Add(new SlaveInfo()
                {
                    WaveNo = wave.WaveNo,
                    IsCompleteHand = false,
                    NeedWaitPutByApi= wave.OrderApi.Where(o=>o.Status==OrderStatus.Normal).Sum(o=>o.ProductApi.Sum(p=>p.Num)),
                    LatticeInfo = latticeInfoList
                });
                bool isSuccess = db.SaveChanges() > 0;
                return isSuccess ? slaveInfo : null;
            }
        }
        /// <summary>
        /// 获取OrderApi
        /// </summary>
        /// <param name="waveNo"></param>
        /// <param name="productCode"></param>
        /// <returns></returns>
        public List<OrderApi> GetOrderApiList(string waveNo,string barCode)
        {
            using (var db = new SortingDbContext())
            {
                var result = from wave in db.WaveApi
                            from order in wave.OrderApi
                            from product in order.ProductApi
                            where wave.WaveNo == waveNo && product.BarCode == barCode && order.Status==OrderStatus.Normal
                            select order;
                return result.Include("ProductApi").ToList();
            }
        }

        internal void Complete(string waveNo)
        {
            var wave = Get(waveNo);
            wave.Status = WaveStatus.Complete;
            AddOrUpdate(wave);
        }

        public void UpdateWaveApi(List<WaveApi> data)
        {
            try
            {
                using (var db = new SortingDbContext())
                {
                    var waveListOld = data.Select(d => d.WaveNo);
                    var waveList = db.WaveApi.Include("OrderApi").Include("OrderApi.ProductApi").Where(o => waveListOld.Contains(o.WaveNo));
                    db.WaveApi.RemoveRange(waveList.Where(o=>o.Status==WaveStatus.Unwork));
                    db.SaveChanges();

                    var ErrorWaveNo = waveList.Where(o => o.Status != WaveStatus.Unwork).Select(o => o.WaveNo).ToList();
                    db.WaveApi.AddOrUpdate(data.Where(o => !ErrorWaveNo.Contains(o.WaveNo)).ToArray());
                    bool isSuccess = db.SaveChanges()>0;
                    Debug.Write(isSuccess);
                }
            }catch(Exception ex)
            {
                Console.Write(ex.Message);
            }
        }
    }
}
