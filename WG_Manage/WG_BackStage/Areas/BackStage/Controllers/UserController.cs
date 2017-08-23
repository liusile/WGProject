using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WG_BackStage.Areas.BackStage.Controllers
{
    public class UserController : Controller
    {
        // GET: BackStage/User
        public ActionResult UserConfig()
        {
            return View();
        }
    }
}