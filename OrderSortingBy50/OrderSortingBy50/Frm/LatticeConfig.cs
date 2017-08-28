using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Service.Model;
using Service.ServiceImp;

namespace OrderSortingBy50
{
    public partial class LatticeConfig : Form
    {
        private ButtonService buttonService;
        private SlaveInfoService slaveInfoService;
        private UpDownService upDownService;

        public LatticeConfig()
        {
            InitializeComponent();
        }
        public LatticeConfig(string latticeNo)
        {
            LatticeNo = latticeNo;
            InitializeComponent();
            this.Location = new Point(Cursor.Position.X, Cursor.Position.Y);
        }

        public LatticeConfig(string latticeNo, UpDownService upDownService, SlaveInfoService slaveInfoService, ButtonService buttonService) : this(latticeNo)
        {
            this.upDownService = upDownService;
            this.slaveInfoService = slaveInfoService;
            this.buttonService = buttonService;
        }

        public string LatticeNo { get; private set; }

        private void btnPut_Click(object sender, EventArgs e)
        {
            try
            {
                int num = 0;
                int.TryParse(txtPutNum.Text.Trim(), out num);
                var latticeInfo = slaveInfoService.GetLatticeInfo(LatticeNo);
                //验证
                if (slaveInfoService.Status != Domain.Models.LatticeStatus.WaitPut)
                {
                    MessageBox.Show("非待投递状态，无法触发已投递");
                    return;
                }
                upDownService.PutIng(new UpDownMessage()
                {
                    LatticeByUpDown = new List<LatticeByUpDown>()
                    {
                        new LatticeByUpDown() {LatticeNo = LatticeNo, Num = num}
                    },
                    Type = txtType.Text.Trim()
                    
                });
                buttonService.UpdateButton(latticeInfo);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBlock_Click(object sender, EventArgs e)
        {
            var latticeInfo = slaveInfoService.GetLatticeInfo(LatticeNo);
            upDownService.BlockError(new UpDownMessage
            {
                LatticeByUpDown=new List<LatticeByUpDown>()
                {
                    new LatticeByUpDown()
                    {
                        LatticeNo = LatticeNo
                    }
                }
            });
            buttonService.UpdateButton(latticeInfo);
            this.Close();
        }

        private void btnRemoveBlockError_Click(object sender, EventArgs e)
        {
            var latticeInfo = slaveInfoService.GetLatticeInfo(LatticeNo);
            upDownService.RemoveBlockError(new UpDownMessage
            {
                LatticeByUpDown = new List<LatticeByUpDown>()
                {
                    new LatticeByUpDown()
                    {
                        LatticeNo = LatticeNo
                    }
                }
            });
            buttonService.UpdateButton(latticeInfo);
            this.Close();
        }

        private void btnLatticeDetail_Click(object sender, EventArgs e)
        {
            Form frm = new LatticeCacheDetail(LatticeNo, slaveInfoService);
            frm.ShowDialog();
        }
        /// <summary>
        /// 单个打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrintSingle_Click(object sender, EventArgs e)
        {
            upDownService.PrintSingle(new UpDownMessage()
            {
                LatticeByUpDown = new List<LatticeByUpDown>()
                {
                    new LatticeByUpDown() {LatticeNo = LatticeNo}
                }
            });
        }
        /// <summary>
        /// 全打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrintAll_Click(object sender, EventArgs e)
        {
            upDownService.PrintAll(new UpDownMessage()
            {
                LatticeByUpDown = new List<LatticeByUpDown>() { }
            });
        }
    }
}
