using CustomTask;
using Domain;
using Domain.Models;
using Service;
using Service.ServiceImp;
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
    public partial class frmWaveApiSync : Form
    {
        public WaveApiService WaveApiService { get; set; }
        public SysConfig SysConfig { get; private set; }

        public frmWaveApiSync( SysConfig SysConfig)
        {
            InitializeComponent();
            WaveApiService = new WaveApiService();
            this.SysConfig = SysConfig;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var waveNo = txtWaveNo.Text;
            if (string.IsNullOrWhiteSpace(waveNo))
            {
                using (var db = new SortingDbContext())
                {
                    var result2 =
                                 from wave in db.WaveApi
                                 from order in wave.OrderApi
                                 from product in order.ProductApi
                                 where wave.Status == Domain.Models.WaveStatus.Unwork
                                 select new
                                 {
                                     WaveNo = wave.WaveNo,
                                     OrderNo = order.OrderNo,
                                     ProductCode = product.ProductCode,
                                     ProductName = product.ProductName,
                                     Num = product.Num,
                                     Status = wave.Status
                                 };
                    
                    dataGridView1.DataSource = result2.Take(100).ToList();
                }
            }
            else
            {
                using (var db = new SortingDbContext())
                {
                    var result1 = from slave in db.SlaveInfo
                                  from lattice in slave.LatticeInfo
                                  from product in lattice.Product
                                  where slave.WaveNo == waveNo
                                  select new
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
                                      IsPrint = lattice.IsPrint
                                  };
                    var result2 =
                                 from wave in db.WaveApi
                                 from order in wave.OrderApi
                                 from product in order.ProductApi
                                 where wave.WaveNo == waveNo && order.Status==Domain.Models.OrderStatus.Normal
                                 select new
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
                                     IsPrint = false
                                 };
                    var data = result1.Concat(result2.Where(o => !result1.Select(r => r.OrderNo).Contains(o.OrderNo))).ToList();
                    dataGridView1.DataSource = data;
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            new OrderDownloadTask().DownWave(SysConfig.DomainName,Main.CurUser?.Pcid);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var db = new SortingDbContext())
            {
                db.WaveApi.RemoveRange(db.WaveApi);
                db.SaveChanges();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string waveNo = txtWaveNo.Text.Trim();
            if (string.IsNullOrWhiteSpace(waveNo))
            {
                MessageBox.Show("请输入所需打印的波次号");
                return;
            }
            using (var db = new SortingDbContext())
            {
                var result =
                                from wave in db.WaveApi
                                from order in wave.OrderApi
                                from product in order.ProductApi
                                where wave.WaveNo == waveNo && order.Status == Domain.Models.OrderStatus.Normal
                                select new
                                {
                                    WaveNo = wave.WaveNo,
                                    BarCode = product.BarCode
                                };
                if (result.Count() > 0)
                {
                    new CustomPrint().PrintSetup(new LatticeInfo
                    {
                        OrderNo = result.FirstOrDefault().WaveNo,
                       LatticeNo ="波次号" 
                    });
                }
                result.ToList().ForEach(o =>
                {
                    new CustomPrint().PrintSetup(new LatticeInfo
                    {
                        OrderNo = o.BarCode
                    });
                });

            }
        }
    }
}
