using Service.IService.BackStage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WG_BackStage.Areas.BackStage.Controllers
{
    public class HomeController : BaseController
    {
       
        // GET: BackStage/Home
        public ActionResult Index()
        {
            //CurUserInfo = UserInfoBz.GetAccountCookie();
            return View();
        }
    }
}