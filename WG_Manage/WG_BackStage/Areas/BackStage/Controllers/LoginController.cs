using Common;
using Common.JsonHelper;
using Domain.Models.BackStage;
using Service;
using Service.IService.BackStage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WG_BackStage.Areas.BackStage.Controllers
{
    public class LoginController : Controller
    {
       public IUserInfoManage UserInfoBz { get; set; }

        [AllowAnonymous]
        public ActionResult Index()
        {
            var userInfo = UserInfoBz.GetAccountCookie();
            return View(userInfo);
        }
        [AllowAnonymous]
        public ActionResult Login(string UserName, string Pwd,bool remember)
        {
            var userInfo = UserInfoBz.UserLogin(UserName, Pwd);
            if (userInfo == null)
            {
               return Json(new { isSuccess = false, Msg = "账号或密码错误！" });
            }
            if (remember) {
                UserInfoBz.SetAccountCookie(userInfo);
            } else {
                UserInfoBz.ClearAccountCookie();
            }
            SessionHelper.SetSession("CurUser", userInfo);
            return Json(new { isSuccess = true, Msg = "登录成功！" });
        }
        [AllowAnonymous]
        public ActionResult LoginOut()
        {
            SessionHelper.Del("CurUser");
            UserInfoBz.ClearAccountCookie();
            return RedirectToAction("Index");
        }
    }
}