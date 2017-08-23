using Service.IService.HeadStage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WG_BackStage.Areas.HeadStage.Controllers
{
    public class NewsListController : Controller
    {
        INews NewsBz { get; set; }
        // GET: HeadStage/NewsList
        public ActionResult NewsList(int p=1)
        {
            var list = NewsBz.Query(
                            NewsBz.LoadAll(o => true).OrderByDescending(o => o.ID),
                            p,
                            10
                        );
            return View(list);
        }
        public ActionResult NewsDetail(int id)
        {
            var model = NewsBz.Get(o => o.ID == id);
            return View(model);
        }
    }
}