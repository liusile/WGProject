using Domain.Model;
using Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IUserInfoManage : IRepository<UserInfo>
    {
        /// <summary>
        /// 验证用户登录
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="Pwd">密码</param>
        /// <returns>返回用户对象,</returns>
        UserInfo UserLogin(string UserName, string Pwd);
        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Pwd"></param>
        /// <returns></returns>
        bool SaveUserInfo(string UserName, string Pwd,bool isCommit);
        /// <summary>
        /// 从cookie中获取用户信息
        /// </summary>
        /// <returns></returns>
        UserInfo GetAccountCookie();
        /// <summary>
        /// 保存用户cookie
        /// </summary>
        /// <param name="userInfo"></param>
        void SetAccountCookie(UserInfo userInfo);
        /// <summary>
        /// 清除用户cookie
        /// </summary>
        void ClearAccountCookie();
        /// <summary>
        /// 获取页面权限
        /// </summary>
        /// <param name="RoleID"></param>
        /// <returns></returns>
        IQueryable<PagePermission> GetPagePermission(int RoleID);
        IQueryable<PagePermission> GetPagePermission();
        IEnumerable<RolePermission> GetRolePermission(int RoleID);
        string GetPwdAES(string Pwd);
    }
}
