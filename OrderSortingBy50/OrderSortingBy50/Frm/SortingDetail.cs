using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Service.ServiceImp;
using Domain;
using OrderSortingBy50.Model;

namespace OrderSortingBy50.Frm
{
    public partial class SortingDetail : Form
    {
        private SlaveInfoService slaveInfoService;

        public SortingDetail()
        {
            InitializeComponent();
        }

        public SortingDetail(SlaveInfoService slaveInfoService) : this()
        {
            this.slaveInfoService = slaveInfoService;
            var waveNo = slaveInfoService?.SlaveInfo?.WaveNo;
            if (string.IsNullOrWhiteSpace(waveNo))
            {
                return;
            }

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
                                 IsPrint=lattice.IsPrint
                             };
                var result2 = 
                             from wave in db.WaveApi
                             from order in wave.OrderApi
                             from product in order.ProductApi
                             where wave.WaveNo == waveNo && order.Status==Domain.Models.OrderStatus.Normal
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
                                 IsPrint = false
                             };
                var data = result1.Concat(result2.Where(o=>!result1.Select(r=>r.OrderNo).Contains(o.OrderNo))).ToList();
                dataGridView1.DataSource = data;
            }  
        }
    }
}
