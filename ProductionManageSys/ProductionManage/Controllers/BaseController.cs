using Common;
using Domain.Model;
using Service.IService;
using Service.Model;
using Service.ServiceImp;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ProductionManage.Controllers
{
    public class BaseController : Controller
    {
        public int page { get; set; }
        public int pagesize { get; set; }
        public IUserInfoManage UserInfoBz { get; set; } = new UserInfoManage();

        #region 用户对象
        /// <summary>
        /// 获取当前用户对象
        /// </summary>
        public UserInfo CurrentUser
        {
            get
            {
                return SessionHelper.GetSession("CurUser") as UserInfo;
                //return UserInfoBz.GetAccountCookie();
            }
        }
        #endregion
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            #region 公共Get变量
            //分页页码
            object p = filterContext.HttpContext.Request["p"];
            if (p == null || p.ToString() == "") { page = 1; } else { page = int.Parse(p.ToString()); }

            //显示分页条数
            string size = filterContext.HttpContext.Request.QueryString["c"];
            if (!string.IsNullOrEmpty(size) && System.Text.RegularExpressions.Regex.IsMatch(size.ToString(), @"^\d+$")) { pagesize = int.Parse(size.ToString()); } else { pagesize = 10; }
            #endregion

        }
        public List<PagePermission> GetPagePermission( int RoleID, string areaName, string controllerName)
        {
            var permission = UserInfoBz.GetPagePermission();
            return permission.Where(o =>
                                        o.RoleID == RoleID &&
                                        o.AreaName == areaName &&
                                        o.ControllerName == controllerName )
                              .ToList();
        }
    }
}