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
using Domain.Models;
using System.Reflection;

namespace OrderSortingBy50
{
    public partial class LatticeCacheDetail : Form
    {
        private SlaveInfoService slaveInfoService;

        public LatticeCacheDetail()
        {
            InitializeComponent();
            dgvContent.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dgvContent, true, null);
            //初始化dgvContent的列绑定
            dgvContent.AutoGenerateColumns = false;
            dgvBarCode.DataPropertyName = "ProductCode";
            dgvProductName.DataPropertyName = "ProductName";
            dgvWaitNum.DataPropertyName = "WaitNum";
            dgvPutNum.DataPropertyName = "PutNum";
            dgvIsComplete.DataPropertyName = "IsComplete";
            dgvStatus.DataPropertyName = "Status";
            dgvId.DataPropertyName = "Id";
        }
        public LatticeCacheDetail(string latticeNo, SlaveInfoService slaveInfoService) :this()
        {
            this.LatticeNo = latticeNo;
            this.slaveInfoService = slaveInfoService;
            Init();
        }

        public string LatticeNo { get; private set; }

        public void Init()
        {
            var latticeInfo = slaveInfoService.SlaveInfo.LatticeInfo.FirstOrDefault(o => o.LatticeNo == LatticeNo);
            lblLatticeNo.Text = LatticeNo;
            if (latticeInfo != null)
            {
                lblWave.Text = slaveInfoService.SlaveInfo.WaveNo;
                lblOrder.Text = latticeInfo.OrderNo;
                lblSumNum.Text = latticeInfo.WaitNum.ToString();
                lblWaitNum.Text = (latticeInfo.WaitNum - latticeInfo.PutNum).ToString();
                lblPutSuccessNum.Text = latticeInfo.PutNum.ToString();
                lblIsPrint.Text = latticeInfo.IsPrint.ToString();
                lblStatus.Text = Enum.GetName(typeof(LatticeStatus), latticeInfo.Status);

                dgvContent.DataSource = latticeInfo.Product;
            }

        }

    }
}
