using ProductionManage.Controllers;
using System;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ProductionManage
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class UserAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        ///     基类实例化
        /// </summary>
        public BaseController baseController = new BaseController();


        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var areaName = filterContext.RouteData.DataTokens["area"]?.ToString()??"";
            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var actionName = filterContext.ActionDescriptor.ActionName;

            if (filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Length > 0)
            {
                filterContext.Controller.ViewBag.Delete = true;
                filterContext.Controller.ViewBag.Edit = true;
                filterContext.Controller.ViewBag.Approve = true;
                filterContext.Controller.ViewBag.Reject = true;
                return;
            }
                
            if (areaName == "HeadStage")
                return;
            //1.判断是否已经登录
            if (baseController.CurrentUser == null)
            {
                SetHttpContext(filterContext, false, "登录已过期，请重新登录！");
                return;
            }
            if (baseController.CurrentUser.IsSuperAdmin)
            {
                filterContext.Controller.ViewBag.Delete = true;
                filterContext.Controller.ViewBag.Edit = true;
                filterContext.Controller.ViewBag.Approve = true;
                filterContext.Controller.ViewBag.Reject = true;
                return;
            }
            //2.判断是否有权限
            var PagePermissionList = baseController.GetPagePermission(
                baseController.CurrentUser.RoleID,
                areaName,
                controllerName
                );
            if (!PagePermissionList.Exists(o=>o.PermissionName== "View"))
            {
                ViewContext dd = filterContext.ParentActionViewContext;

                filterContext.Result = new ContentResult { Content="您无权限操作！"};
               
                return;
            }
            filterContext.Controller.ViewBag.Delete = false;
            filterContext.Controller.ViewBag.Edit = false;
            filterContext.Controller.ViewBag.Approve = false;
            filterContext.Controller.ViewBag.Reject = false;
            if (PagePermissionList.Exists(o=>o.PermissionName== "Delete"))
            {
                filterContext.Controller.ViewBag.Delete = true;
            }
            if (PagePermissionList.Exists(o => o.PermissionName == "Edit"))
            {
                filterContext.Controller.ViewBag.Edit = true;
            }
            if (PagePermissionList.Exists(o => o.PermissionName == "Approve"))
            {
                filterContext.Controller.ViewBag.Approve = true;
            }
            if (PagePermissionList.Exists(o => o.PermissionName == "Reject"))
            {
                filterContext.Controller.ViewBag.Reject = true;
            }
            // base.OnAuthorization(filterContext);
        }

        private void SetHttpContext(AuthorizationContext filterContext, bool isSuccess, string Msg)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                var data = new {isSuccess, Msg};
                filterContext.Result = new JsonResult {Data = data};
            }
            else if (!isSuccess)
            {
                filterContext.Result =
                    new RedirectResult(string.Format("~/Login?ReturnUrl={0}",
                        filterContext.HttpContext.Request.Url.OriginalString));
            }
        }
    }
}