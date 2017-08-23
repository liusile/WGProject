using Domain;
using Domain.Models.HeadStage;
using Service.IService.HeadStage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WG_BackStage.Areas.HeadStage.Controllers
{
    public class ProductListController : Controller
    {
        IProduct ProductBz { get; set; }
        // GET: HeadStage/ProductList
        public ActionResult SortingRack()
        {
            var model = ProductBz.Get(o => o.ID == 1);
            return View(model);
        }
        public ActionResult AutoCar()
        {
            var model = ProductBz.Get(o => o.ID == 2);
            return View(model);
        }
    }
}