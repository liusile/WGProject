using Domain.Models;
using Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.API
{
    public class LoginAPI
    {
        public Domain.Models.UserInfo Login(string email, string pwd, ref string errorMsg)
        {
            try
            {
                string url = "http://oldsystemservice.sellercube.com/Account/Login";
                var request = new LoginRequestContract
                {
                    Token = "5A9C85B6E068F2236A039E6157C5DF5B",
                    LoginName = email,
                    Password = pwd
                };
                var response =new  Common.HttpHelper().Post<LoginResponseContract>(url, request);
                if (response == null)
                {
                    errorMsg=  "获取用户信息异常, 请检查服务是否已关闭!";
                    return null;
                }
                if (!response.Success)
                {
                    errorMsg = response.Message;
                    return null;
                }
                return new Domain.Models.UserInfo
                {
                    UserId = Convert.ToInt32(response.userID),
                    UserName = response.userName,
                    Pcid = response.Pcid,
                    PcName = response.PcName
                };
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return null;
            }
        }
    }
}
