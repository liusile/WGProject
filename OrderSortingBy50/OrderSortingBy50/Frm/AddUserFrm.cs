using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Domain.Models;
using Service.ServiceImp;

namespace OrderSortingBy50.Frm
{
    public partial class AddUserFrm : Form
    {
        public AddUserFrm()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //string userName = txtUser.Text.Trim();
            //if (string.IsNullOrEmpty(userName))
            //{
            //    lblError.Visible = true;
            //    txtUser.Focus();
            //    return;
            //}
            //new UserService().Add(new UserInfo
            //{
            //    Name= userName,
            //    LastLoginTime=DateTime.Now
            //});
            //DialogResult = DialogResult.OK;
            //Close();
        }
    }
}
