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
    public class SupplierProductController : BaseController
    {
        public ISupplierProductManage SupplierProductManageBz = new SupplierProductManage();
        public ISupplier SupplierManageBz = new SupplierManage();
        // GET: Supplier
        public ActionResult Index(int p = 1, string searchFild = "")
        {
            var model = SupplierProductManageBz.Query(
                            SupplierProductManageBz.LoadAll(o => o.ProductName.Contains(searchFild)).OrderByDescending(o => o.ProductId),
                            p,
                            10);
            ViewBag.searchFild = searchFild;
            return View(model);
        }

        public ActionResult SupplierProductDel(int id)
        {
            var isSuccess = SupplierProductManageBz.Delete(o => o.ProductId == id);
            if (isSuccess)
            {
                TempData["Msg"] = "删除成功！";
            }
            else
            {
                TempData["Msg"] = "删除失败！";
            }
            return RedirectToAction("Index");
        }
        public ActionResult SupplierProductEidt(int id = 0)
        {
            var model = SupplierProductManageBz.Get(o => o.ProductId == id);
            ViewBag.GetSupplierList = GetSupplierList();
            return View(model);
        }
        [ValidateInput(false)]
        public ActionResult Save(SupplierProduct entity)
        {
            ViewBag.GetSupplierList = GetSupplierList();
            var ValidRecruitJob = SupplierProductManageBz.IsExist(o => o.SupplierId == entity.SupplierId && o.ProductName == entity.ProductName && o.ProductId!=entity.ProductId);
            if (ValidRecruitJob)
            {
                ViewBag.Msg = $"该供应商的产品已存在：{entity.ProductName}！";
                return View("SupplierProductEidt", entity);
            }
            if (string.IsNullOrWhiteSpace(entity.ProductName))
            {
                ViewBag.Msg = $"产品名称不能为空";
                return View("SupplierProductEidt", entity);
            }
            if (string.IsNullOrWhiteSpace(entity.Price))
            {
                ViewBag.Msg = $"产品单价不能为空";
                return View("SupplierProductEidt", entity);
            }
            var isSuccess = SupplierProductManageBz.SaveOrUpdate(entity, entity.ProductId > 0);
            if (isSuccess)
            {
                ViewBag.Msg = "保存成功！";
            }
            else
            {
                ViewBag.Msg = "保存失败！";
            }
            var model = SupplierProductManageBz.Get(o => o.ProductId == entity.ProductId);
            return View("SupplierProductEidt", model);
        }
        /// <summary>
        /// 供应商列表
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetSupplierList()
        {
            var list = SupplierManageBz.LoadAll(o => true).Select(o => new SelectListItem { Text = o.CorpName, Value = o.ID.ToString() }).ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "请选择",
                Value = ""
            });
            return list;
        }
    }
}