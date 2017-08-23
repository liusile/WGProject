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
    public class SupplierController : BaseController
    {
        public ISupplier SupplierBz = new SupplierManage();
        // GET: Supplier
        public ActionResult Index(int p = 1, string searchFild = "")
        {
            var model = SupplierBz.Query(
                            SupplierBz.LoadAll(o => o.CorpName.Contains(searchFild)).OrderByDescending(o => o.ID),
                            p,
                            10);
            ViewBag.searchFild = searchFild;
            return View(model);
        }
        
        public ActionResult SupplierDel(int id)
        {
            var isSuccess = SupplierBz.Delete(o => o.ID == id);
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
        public ActionResult SupplierEidt(int id = 0)
        {
            var model = SupplierBz.Get(o => o.ID == id);
            return View(model);
        }
        [ValidateInput(false)]
        public ActionResult Save(Supplier entity)
        {
            var ValidRecruitJob = SupplierBz.IsExist(o => o.CorpName == entity.CorpName && o.ID != entity.ID);
            if (ValidRecruitJob)
            {
                ViewBag.Msg = $"该公司已存在：{entity.CorpName}！";
                return View("SupplierEidt", entity);
            }
            if (string.IsNullOrWhiteSpace(entity.CorpName))
            {
                ViewBag.Msg = $"公司名称不能为空";
                return View("SupplierEidt", entity);
            }
            var isSuccess = SupplierBz.SaveOrUpdate(entity, entity.ID > 0);
            if (isSuccess)
            {
                ViewBag.Msg = "保存成功！";
            }
            else
            {
                ViewBag.Msg = "保存失败！";
            }
            var model = SupplierBz.Get(o => o.CorpName == entity.CorpName);
            return View("SupplierEidt", model);
        }
    }
}