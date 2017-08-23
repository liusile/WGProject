using Common;
using Service.IService;
using Service.ServiceImp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductionManage.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public IUserInfoManage UserInfoBz = new UserInfoManage();

        [AllowAnonymous]
        public ActionResult Index()
        {
            var userInfo = UserInfoBz.GetAccountCookie();
            return View(userInfo);
        }

        [AllowAnonymous]
        public ActionResult Login(string UserName, string Pwd, bool remember)
        {
            var userInfo = UserInfoBz.UserLogin(UserName, Pwd);
            if (userInfo == null)
                return Json(new { isSuccess = false, Msg = "账号或密码错误！" });
            if (remember) UserInfoBz.SetAccountCookie(userInfo);
            else UserInfoBz.ClearAccountCookie();
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