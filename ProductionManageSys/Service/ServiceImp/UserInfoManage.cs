using Common;
using Common.JsonHelper;
using Domain;
using Domain.Model;
using Service.IService;
using Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceImp
{
   public class UserInfoManage : RepositoryBase<UserInfo>, IUserInfoManage
    {
        private readonly static string PwdKey= "D892D3349349D8F755FB8";
        private readonly static string UserCookieKey = "wg_productionManage_user";
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
        public string GetPwdAES(string Pwd)
        {
            return new Common.CryptHelper.AESCrypt().Encrypt(Pwd, PwdKey);
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
            catch (Exception ex){ }
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
        public IEnumerable<RolePermission> GetRolePermission(int RoleID)
        {
            IRoleModulePermissionManage RoleModulePermissionBz = new RoleModulePermissionManage();
            IModuleManage ModuleManageBz = new ModuleManage();

            var moduleList = ModuleManageBz.LoadAll(o => o.PermissionName!=null).ToList();
            var model = new List<RolePermission>();
            var RoleModuleList =RoleModulePermissionBz.LoadAll(o => o.RoleID==RoleID).ToList();

            foreach (Module row in moduleList)
            {
                string moduleName = row.ModuleName;
                string PermissionName = row.PermissionName;
                bool isSelect = RoleModuleList.Any(o => o.ModuleID == row.ID);

                if (!model.Any(o=>o.ModuleName == row.ModuleName))
                {
                    model.Add(new RolePermission
                    {
                        ModuleName = row.ModuleName
                    });
                }
                var module=model.Find(o => o.ModuleName == row.ModuleName);
                switch (row.PermissionName)
                {
                    case "View":
                        module.View = true;
                        module.ViewCheck = isSelect;
                        module.ViewID = row.ID;
                        break;
                    case "Delete":
                        module.Delete = true;
                        module.DeleteCheck = isSelect;
                        module.DeleteID = row.ID;
                        break;
                    case "Edit":
                        module.Edit = true;
                        module.EditCheck = isSelect;
                        module.EditID = row.ID;
                        break;
                    case "Approve":
                        module.Approve = true;
                        module.ApproveCheck = isSelect;
                        module.ApproveID = row.ID;
                        break;
                    case "Reject":
                        module.Reject = true;
                        module.RejectCheck = isSelect;
                        module.RejectID = row.ID;
                        break;
                    default :
                        break;
                }

            }
            return model;
        }
        public IQueryable<PagePermission> GetPagePermission()
        {
            var entity = from m in _Context.RoleModulePermission
                         join role in _Context.RoleInfo on m.RoleID equals role.ID
                         join md in _Context.Module on m.ModuleID equals md.ID
                         select new PagePermission
                         {
                             ID=md.ID,
                             RoleID =m.RoleID,
                             ModuleName = md.ModuleName,
                             AreaName = md.AreaName,
                             ControllerName = md.ControllerName,
                             ActionName = md.ActionName,
                             PermissionName = md.PermissionName

                         };
            return entity;
        }

        public IQueryable<PagePermission> GetPagePermission(int RoleID)
        {
            throw new NotImplementedException();
        }
    }

}
