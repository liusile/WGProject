
using Service.IService;
using Service.ServiceImp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductionManage.Controllers
{
    public class ProductLogController : BaseController
    {
        public IProductLogManage ProductLogManageBz = new ProductLogManage();
        // GET: ProductLog
        public ActionResult Index(int p = 1, string searchFild = "")
        {
            var model = ProductLogManageBz.Query(
                          ProductLogManageBz.LoadAll(o => o.ProductName.Contains(searchFild)).OrderByDescending(o => o.Id),
                          p,
                          10);
            ViewBag.searchFild = searchFild;
            return View(model);
        }
    }
}