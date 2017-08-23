using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductionManage.Controllers
{
    public class PwdController : BaseController
    {
        // GET: Pwd
        public ActionResult UpdatePwd()
        {
            return View();
        }
        public JsonResult SaveUpdatePwd(string oldPwd, string newPwd)
        {
            if (oldPwd != CurrentUser.Pwd)
            {
                return Json(new { isSuccess = false, Msg = "旧密码错误!" });
            }
            var user = UserInfoBz.Get(o => o.UserName == CurrentUser.UserName);
            user.Pwd = UserInfoBz.GetPwdAES(newPwd);
            bool isSuccess = UserInfoBz.Update(user);
            if (isSuccess)
            {
                SessionHelper.Del("CurUser");
                UserInfoBz.ClearAccountCookie();
            }
            return Json(new { isSuccess = isSuccess, Msg = isSuccess ? "修改成功！" : "修改失败！" });
        }
    }
}