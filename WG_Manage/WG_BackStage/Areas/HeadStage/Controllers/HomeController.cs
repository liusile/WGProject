using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WG_BackStage.Areas.HeadStage.Controllers
{
    public class HomeController : Controller
    {
        // GET: HeadStage/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}