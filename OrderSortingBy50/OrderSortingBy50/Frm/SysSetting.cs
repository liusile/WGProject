using Domain.Models;
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
    public partial class SysSetting : Form
    {
        public SysSetting()
        {
            InitializeComponent();
            Init();
        }
        private void Init()
        {
            var sysConfigService = new SysConfigService();
            var entity = sysConfigService.Get();
            txtIP.Text = entity.IP;
            txtPort.Text = entity.Port;
            txtDomainName.Text = entity.DomainName;
            //PrintType
            var enumArray = Enum.GetValues(typeof(PrintType));
            List<KeyValuePair<int, string>> statusList = new List<KeyValuePair<int, string>>();

            foreach (var enumItem in enumArray)
            {
                statusList.Add(new KeyValuePair<int, string>((int)enumItem, enumItem.ToString()));
            }
            cmbPrintType.DataSource = statusList;
            cmbPrintType.DisplayMember = "Value";
            cmbPrintType.ValueMember = "Key";
            cmbPrintType.SelectedValue = (int)entity.PrintType;
            txtScanPortName.Text = entity.ScanPortName;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var sysConfigService = new SysConfigService();
            var entity = sysConfigService.Get();
            entity.IP = txtIP.Text.Trim();
            entity.ScanPortName = txtScanPortName.Text.Trim();
            entity.Port = txtPort.Text.Trim();
            entity.PrintType =(PrintType)cmbPrintType.SelectedValue;
            entity.DomainName = txtDomainName.Text.Trim();
            sysConfigService.Save(entity);
            this.Close();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
