using Domain;
using Domain.Models;
using Service.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceImp
{
    public class SlaveInfoService
    {
        public SlaveInfo SlaveInfo { get; set; }
        public bool IsComplete => SlaveInfo.IsComplete;
        public LatticeStatus Status => SlaveInfo.LatticeStatus;
        public List<LatticeInfo> LatticeInfoList => SlaveInfo.LatticeInfo;

        public SlaveInfoService()
        {
            InitSlaveInfo();
        }

        #region 获取数据
        /// <summary>
        /// 获取格口
        /// </summary>
        /// <param name="latticeNo">格口号</param>
        /// <returns></returns>
        public LatticeInfo GetLatticeInfo(string latticeNo)
        {
            return SlaveInfo.LatticeInfo.FirstOrDefault(o => o.LatticeNo == latticeNo);
        }
        /// <summary>
        /// 获取格口
        /// </summary>
        /// <param name="upDownMessage"></param>
        /// <returns></returns>
        public List<LatticeInfo> GetLatticeInfoList(UpDownMessage upDownMessage)
        {
            List<LatticeInfo> latticeInfoList = new List<LatticeInfo>();
            upDownMessage.LatticeByUpDown.ForEach(o =>
            {
                latticeInfoList.Add(GetLatticeInfo(o.LatticeNo));
            });
            return latticeInfoList;
        }
        /// <summary>
        /// 获取批次
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public SlaveInfo GetSlaveInfo(Func<SlaveInfo, bool> condition)
        {
            using (var db = new SortingDbContext())
            {
                return db.SlaveInfo.Include("LatticeInfo").Include("LatticeInfo.Product").FirstOrDefault(condition);
            }
        }
        #endregion

        #region 更改数据
        /// <summary>
        /// 更改产品状态
        /// </summary>
        /// <param name="o">格口信息</param>
        /// <param name="productCode">产品代号</param>
        /// <param name="status">更改状态</param>
        internal void SetProductStatus(LatticeInfo latticeInfo, string barCode, ProductStatus status)
        {
            foreach (var product in latticeInfo.Product.Where(p => p.BarCode == barCode))
            {
                product.Status = status;
            }
        }
        /// <summary>
        /// 更改格口所有的产品状态为None
        /// </summary>
        /// <param name="latticeInfo"></param>
        public void ProductTransformNone(LatticeInfo latticeInfo)
        {
            var lattice = SlaveInfo.LatticeInfo.FirstOrDefault(o => o.LatticeNo == latticeInfo.LatticeNo);
            if (lattice != null)
            {
                lattice.Product.FindAll(p => p.Status == ProductStatus.WaitPut).ForEach(o=>o.Status= ProductStatus.None);
            }
        }
        /// <summary>
        /// 更改格口的状态
        /// </summary>
        /// <param name="latticeInfo"></param>
        /// <param name="status"></param>
        public void SetLatticeStatus(LatticeInfo latticeInfo, LatticeStatus status)
        {
            var lattice = SlaveInfo.LatticeInfo.FirstOrDefault(o => o.LatticeNo == latticeInfo.LatticeNo);
            if (lattice != null)
            {
                lattice.Status = status;
            }
        }
        /// <summary>
        /// 取消波次分拣
        /// </summary>
        /// <param name="slaveInfo">波次信息</param>
        public bool DeleteSlaveInfo(SlaveInfo slaveInfo = null)
        {
            if (slaveInfo == null)
                slaveInfo = SlaveInfo;
            using (var db = new SortingDbContext())
            {
                db.WaveApi.AddOrUpdate(new WaveApi()
                {
                    WaveNo = slaveInfo.WaveNo,
                    Status = WaveStatus.Unwork
                });
                var entity = db.SlaveInfo.Include("LatticeInfo").Include("LatticeInfo.Product").FirstOrDefault(o => o.WaveNo == slaveInfo.WaveNo);
                db.SlaveInfo.Remove(entity);
                bool isSuccess = db.SaveChanges() > 0;
                return isSuccess;
            }
        }
        /// <summary>
        /// 未待投递的格口添加产品数量
        /// </summary>
        /// <param name="latticeNo"></param>
        /// <param name="putNum"></param>
        public void AddProductNum(string latticeNo, int putNum)
        {
            var lattice = SlaveInfo.LatticeInfo.FirstOrDefault(o => o.LatticeNo == latticeNo);
            if (lattice != null)
            {
                lattice.Product.First(o => o.Status == ProductStatus.WaitPut).PutNum += putNum;
            }
        }
        #endregion

        /// <summary>
        /// 初始化波次
        /// </summary>
        public void InitSlaveInfo()
        {
            List<LatticeInfo> latticeInfoList = new List<LatticeInfo>();

            Enumerable.Range(1, 50).ToList().ForEach(o => latticeInfoList.Add(
                new LatticeInfo
                {
                    LatticeNo = o.ToString(),
                    Status  = LatticeStatus.None,
                    Product = new List<Product>()
                }
            ));
            SlaveInfo = new SlaveInfo
            {
                LatticeInfo = latticeInfoList
            };
        }

        /// <summary>
        /// 持久化数据到数据库
        /// </summary>
        public void SaveAync()
        {
            using (var db = new SortingDbContext())
            {
                var slave = db.SlaveInfo.Include("LatticeInfo").Include("LatticeInfo.Product").FirstOrDefault(o=>o.Id==SlaveInfo.Id);
                if (slave == null)
                    return;
                slave.IsCompleteHand = SlaveInfo.IsCompleteHand;
                SlaveInfo.LatticeInfo.ForEach(o =>
                {
                    var lattice = slave.LatticeInfo.First(b => b.LatticeNo == o.LatticeNo);
                    lattice.OrderNo = o.OrderNo;
                    lattice.Status = o.Status;
                    lattice.IsPrint = o.IsPrint;
                    o.Product.ForEach(p =>
                    {
                        var product = lattice.Product.FirstOrDefault(i => i.BarCode == p.BarCode);
                        if (product == null)
                        {
                            lattice.Product.Add(p);
                        }
                        else
                        {
                            product.PutNum = p.PutNum;
                            product.Status = p.Status;
                        }
                    });
                });
                db.SlaveInfo.AddOrUpdate(slave);
                db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// 装载格口
        /// </summary>
        /// <param name="orderApi"></param>
        /// <returns></returns>
        public LatticeInfo LoadLattice(OrderApi orderApi)
        {
            var latticeInfo =
                    SlaveInfo.LatticeInfo.Where(o => string.IsNullOrWhiteSpace(o.OrderNo))
                    .OrderBy(o => Convert.ToInt16(o.LatticeNo))
                    .FirstOrDefault();
            if (latticeInfo != null)
            {
                latticeInfo.OrderNo = orderApi.OrderNo;
                orderApi.ProductApi.ForEach(o =>
                {
                    latticeInfo.Product.Add(new Product()
                    {
                        BarCode = o.BarCode,
                        ProductName=o.ProductName,
                        PutNum=0,
                        WaitNum=o.Num,
                        ProductCode=o.ProductCode,
                        PropertyCode=o.PropertyCode,
                        Feature=o.Feature
                    });
                });
            }
            return latticeInfo;
        }     
        /// <summary>
        /// 清楚历史旧数据
        /// </summary>
        /// <param name="PreDay"></param>
        public void ClearDataAsync(int PreDay = 7)
        {
            using (var db = new SortingDbContext())
            {
                var date = DateTime.Now.AddDays(-PreDay);
                var waveList = db.WaveApi.Include("OrderApi").Include("OrderApi.ProductApi").Where(o=>o.LastTime<= date);
                var slave = db.SlaveInfo.Join(waveList,s=>s.WaveNo,w=>w.WaveNo,(s,w)=>s).Include("LatticeInfo").Include("LatticeInfo.Product");
                db.WaveApi.RemoveRange(waveList);
                db.SlaveInfo.RemoveRange(slave);
                db.SaveChangesAsync();
            }
        }  
        public List<SortingDetailView> GetSortingDetail()
        {
            string waveNo = this.SlaveInfo.WaveNo;
            using (var db = new SortingDbContext())
            {
                var result1 = from slave in db.SlaveInfo
                              from lattice in slave.LatticeInfo
                              from product in lattice.Product
                              where slave.WaveNo == waveNo
                              select new SortingDetailView
                              {
                                  WaveNo = slave.WaveNo,
                                  LatticeNo = lattice.LatticeNo,
                                  OrderNo = lattice.OrderNo,
                                  latticeStatus = lattice.Status,
                                  BarCode = product.BarCode,
                                  ProductName = product.ProductName,
                                  PutNum = product.PutNum,
                                  WaitNum = product.WaitNum,
                                  ProductStatus = product.Status,
                                  IsPrint = lattice.IsPrint,
                                  ProductCode = product.ProductCode,
                                  PropertyCode = product.PropertyCode
                              };
                var result2 =
                             from wave in db.WaveApi
                             from order in wave.OrderApi
                             from product in order.ProductApi
                             where wave.WaveNo == waveNo && order.Status == Domain.Models.OrderStatus.Normal
                             select new SortingDetailView
                             {
                                 WaveNo = wave.WaveNo,
                                 LatticeNo = "",
                                 OrderNo = order.OrderNo,
                                 latticeStatus = Domain.Models.LatticeStatus.None,
                                 BarCode = product.BarCode,
                                 ProductName = product.ProductName,
                                 PutNum = 0,
                                 WaitNum = product.Num,
                                 ProductStatus = Domain.Models.ProductStatus.None,
                                 IsPrint = false,
                                 ProductCode = product.ProductCode,
                                 PropertyCode = product.PropertyCode
                             };
                var data = result1.Concat(result2.Where(o => !result1.Select(r => r.OrderNo).Contains(o.OrderNo))).ToList();
                return data;
            }
        } 
    }
}
