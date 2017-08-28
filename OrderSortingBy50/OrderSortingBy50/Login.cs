using Service.API;
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

namespace OrderSortingBy50
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            Init();
        }
        public void Init()
        {
            var userInfo = new UserService().GetLastUserInfo();
            if (userInfo != null)
            {
                if(userInfo.IsRemeberUserName)
                {
                    txtEmail.Text = userInfo.Email;
                    txtPwd.Select();
                    txtPwd.Focus();
                    chkUser.Checked = true;
                }
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string errorMsg = "";
            string email = txtEmail.Text.Trim();
            string pwd = txtPwd.Text.Trim();
            if(string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(pwd))
            {
                MessageBox.Show("请输入账号密码！");
                return;
            }
           
            var userInfo = new LoginAPI().Login(email,pwd,ref errorMsg);
            if (userInfo == null)
            {
                MessageBox.Show(errorMsg);
                return;
            }
            if (chkPwd.Checked)
            {
                new UserService().UpdateAutoLogin(userInfo, email);
            }
            else if (chkUser.Checked)
            {
                new UserService().UpdateRemeberUserName(userInfo, email);
            }
            Main.CurUser.CopyValues(userInfo);

            this.Close();
        }

        private void txtPwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnLogin_Click(null,null);
            }
        }
    }
}
