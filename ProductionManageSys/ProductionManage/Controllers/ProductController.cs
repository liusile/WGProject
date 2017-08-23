using Domain.Model;
using Service.IService;
using Service.ServiceImp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductionManage.Controllers
{
    public class ProductController : BaseController
    {
        public IProductManage ProductManageBz = new ProductManage();
        // GET: Product
        public ActionResult Index(int p = 1, string searchFild = "")
        {
            var model = ProductManageBz.Query(
                        ProductManageBz.LoadAll(o => o.ProductName.Contains(searchFild)).OrderByDescending(o => o.ProductId),
                        p,
                        10);
            ViewBag.searchFild = searchFild;
            return View(model);
        }
        public ActionResult ProductEidt(int id = 1)
        {
            var model = ProductManageBz.Get(o => o.ProductId == id);
            return View(model);
        }
        public ActionResult Save(Product model)
        {
            var product = ProductManageBz.Get(o => o.ProductId == model.ProductId);
            var NewProduct=Common.Tools.CopyObjectValue(product, model);
            bool isSuccess = ProductManageBz.Update(NewProduct);
            if (isSuccess)
            {
                ViewBag.Msg = "提交成功！";
            }
            else
            {
                ViewBag.Msg = "提交失败！";
            }
            return View("ProductEidt", NewProduct);
        }
    }
}