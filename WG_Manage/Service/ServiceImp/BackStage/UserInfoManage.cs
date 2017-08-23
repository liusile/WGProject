using Common;
using Common.JsonHelper;
using Domain;
using Domain.Models.BackStage;
using Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceImp.BackStage
{
   public class UserInfoManage : RepositoryBase<Domain.Models.BackStage.UserInfo>, IService.BackStage.IUserInfoManage
    {
        private readonly static string PwdKey= "D892D3349349D8F755FB8";
        private readonly static string UserCookieKey = "wg_user";
        private readonly static int CookieExpiresDay = 30;
        public UserInfo UserLogin(string UserName,string Pwd) {
            var entity = this.Get(p => p.UserName == UserName && p.IsValid==true);
           
            if (entity != null && new Common.CryptHelper.AESCrypt().Encrypt(Pwd, PwdKey) == entity.Pwd)
            {
                entity.Pwd = Pwd;
                return entity;
            }
            return null;
        }
        public bool SaveUserInfo(string UserName, string Pwd,bool isCommit=true) {
            return this.Save(new UserInfo
            {
                IsValid = true,
                UserName = UserName,
                Pwd = new Common.CryptHelper.AESCrypt().Encrypt(Pwd, PwdKey)
            },isCommit);
        }
        #region cookie相关
        public UserInfo GetAccountCookie()
        {
            var cookie = CookieHelper.GetCookie(UserCookieKey);
            if (cookie != null)
            {
                //验证json的有效性
                if (!string.IsNullOrEmpty(cookie.Value))
                {
                    //解密
                    var cookievalue = new Common.CryptHelper.AESCrypt().Decrypt(cookie.Value);
                    //是否为json
                    if (!Common.JsonHelper.JsonSplit.IsJson(cookievalue)) return null;
                    try
                    {
                        var userInfo = JsonHelper.FromJson<UserInfo>(cookievalue);
                        return userInfo;
                        //if (userInfo != null)
                        //{
                        //    var users = UserLogin(userInfo.UserName, userInfo.Pwd);
                        //    return users;
                        //}
                    }
                    catch { return null; }
                }
            }
            return null;
        }
        public void SetAccountCookie(UserInfo userInfo)
        {
            try
            {
                var cookievalue = new Common.CryptHelper.AESCrypt().Encrypt(JsonHelper.ToJson(userInfo));
                CookieHelper.SetCookie(UserCookieKey, cookievalue, CookieExpiresDay);
            }
            catch { }
        }
        public void ClearAccountCookie()
        {
            try
            {
                CookieHelper.ClearCookie(UserCookieKey);
            }
            catch { }
        }
        #endregion
        /// <summary>
        /// 是否超级管理员
        /// </summary>
        public bool IsSuperAdmin(string UserName)
        {
            //通过用户ID获取角色
            var entity = this.Get(p => p.UserName == UserName);
            return entity?.IsSuperAdmin==true ? true: false;
        }
        public IQueryable<PagePermission> GetPagePermission(int RoleID)
        {
            var entity = GetPagePermission();
            return entity.Where(o=>o.RoleID== RoleID);
        }
        public IQueryable<PagePermission> GetPagePermission()
        {
            var entity = from m in _Context.RoleModulePermission
                         join role in _Context.RoleInfo on m.RoleID equals role.ID
                         join md in _Context.Module on m.ModuleID equals md.ID
                         join pm in _Context.Permission on m.PermissionID equals pm.ID
                         select new PagePermission
                         {
                             UserName=m.UserName,
                             RoleID =m.RoleID,
                             AreaName = md.AreaName,
                             ControllerName = md.ControllerName,
                             ActionName = md.ActionName,
                             PermissionCode = pm.PermissionCode,
                             PermissionName = pm.PermissionName
                         };
            return entity;
        }
    }

}
