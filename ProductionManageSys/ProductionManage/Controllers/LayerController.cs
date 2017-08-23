using ProductionManage.Models;
using Service.IService;
using Service.ServiceImp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductionManage.Controllers
{
    public class LayerController : BaseController
    {
        public ISupplierProductManage SupplierProductManageBz = new SupplierProductManage();
        public IPurchaseOrderManage PurchaseOrderManageBz = new PurchaseOrderManage();
        public IProductManage ProductManageBz = new ProductManage();
        public SupplierManage SupplierManageBz = new SupplierManage();
        // GET: Layer
        [AllowAnonymous]
        public ActionResult ProductList(string SupplierName,string filter, int p = 1, string searchFild = "")
        {
            var arr = filter.Split('|');
            ViewBag.filter = filter;
            var supplierModel = SupplierManageBz.Get(o => o.CorpName == SupplierName);
            if (supplierModel != null) {
                int SupplierId = supplierModel.ID;
                var model = SupplierProductManageBz.Query(
                               SupplierProductManageBz.LoadAll(o => o.SupplierId == SupplierId && !arr.Contains(o.ProductName) && o.ProductName.Contains(searchFild)).OrderByDescending(o => o.ProductId),
                               p,
                               10);
                ViewBag.searchFild = searchFild;
                return View(model);
            }
            else
            {
                return  View();
            }
        }
        /// <summary>
        /// 产品库存
        /// </summary>
        /// <param name="SupplierName"></param>
        /// <param name="filter"></param>
        /// <param name="p"></param>
        /// <param name="searchFild"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ProductStockList(string filter, int p = 1, string searchFild = "")
        {
            var arr = filter.Split('|');
            ViewBag.filter = filter;

            var model = ProductManageBz.Query(
                            ProductManageBz.LoadAll(o=>!arr.Contains(o.ProductName) && o.ProductName.Contains(searchFild)).OrderByDescending(o => o.ProductId),
                            p,
                            10);
            ViewBag.searchFild = searchFild;
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult PurchaseOrderListForAccept(string searchFild = "", int p = 1)
        {
                var model = PurchaseOrderManageBz.Query(
                               PurchaseOrderManageBz.LoadAll(o => (o.Status==OrderStatus.待验收 || o.Status == OrderStatus.部分验收) && o.PurchaseOrderNo.Contains(searchFild)).OrderByDescending(o => o.ID),
                               p,
                               10);
                ViewBag.searchFild = searchFild;
                return View(model);
        }
    }
}