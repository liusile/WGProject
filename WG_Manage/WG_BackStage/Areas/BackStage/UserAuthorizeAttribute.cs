using Domain.Models.BackStage;
using Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using WG_BackStage.Areas.BackStage.Controllers;

namespace WG_BackStage.Areas.BackStage
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class UserAuthorizeAttribute: AuthorizeAttribute
    {
        /// <summary>
        /// 基类实例化
        /// </summary>
        public BaseController baseController = new BaseController();


        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            string areaName = filterContext.RouteData.DataTokens["area"]?.ToString();
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string actionName = filterContext.ActionDescriptor.ActionName;

            if (filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Length > 0)
            {
                return;
            }
            if(areaName== "HeadStage")
            {
                return;
            }
            //1.判断是否已经登录
            if (baseController.CurrentUser == null)
            {
                SetHttpContext(filterContext,false, "登录已过期，请重新登录！");
                return;
            }
            if (baseController.CurrentUser.IsSuperAdmin)
            {
                return;
            }
            //2.判断是否有权限
            var PagePermissionList = baseController.GetPagePermission(
                                                baseController.CurrentUser.IsSuperAdmin,
                                                baseController.CurrentUser.RoleID,
                                                areaName,
                                                controllerName,
                                                actionName);
            if (PagePermissionList.Count < 1)
            {
                SetHttpContext(filterContext, false, "您无权限操作！");
                return;
            }
            filterContext.Controller.ViewData["PagePermissionList"] = PagePermissionList;
            return;
           // base.OnAuthorization(filterContext);
        }
       
        private void SetHttpContext(AuthorizationContext filterContext, bool isSuccess, string Msg)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                var data = new { isSuccess = isSuccess, Msg = Msg };
                filterContext.Result = new JsonResult() { Data = data };
            }
            else if(!isSuccess)
            {
                filterContext.Result = new System.Web.Mvc.RedirectResult(string.Format("~/BackStage/Login?ReturnUrl={0}", filterContext.HttpContext.Request.Url.OriginalString));
            }
        }
    }
}