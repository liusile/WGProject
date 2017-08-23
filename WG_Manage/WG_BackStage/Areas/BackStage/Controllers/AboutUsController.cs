using Domain.Models.HeadStage;
using Service.IService.HeadStage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WG_BackStage.Areas.BackStage.Controllers
{
    public class AboutUsController : BaseController
    {
        ICompany CompanyBz { get; set; }
        IEmployee EmployeeBz { get; set; }
        
        // GET: BackStage/AboutUs
        public ActionResult Company()
        {
            var model= CompanyBz.Get(o => true);
            return View(model);
        }
        public ActionResult SaveCompany(Company entity)
        {
            bool isSuccess = CompanyBz.Update(entity);
            if (isSuccess)
            {
                ViewBag.Msg = "保存成功！";
            }else
            {
                ViewBag.Msg = "保存失败！";
            }
            return View("Company", entity);
        }

        public ActionResult EmployeeList(int p=1,string searchFild="")
        {
            var model = EmployeeBz.Query(
                             EmployeeBz.LoadAll(o => o.Name.Contains(searchFild)).OrderByDescending(o => o.ID),
                             p,
                             10);
            ViewBag.searchFild = searchFild;
            return View(model);
        }
        public ActionResult EmployeeEdit(int id=0)
        {
            var model = EmployeeBz.Get(o => o.ID == id);
            return View(model);
        }
        public ActionResult SaveEmployee(Employee entity)
        {
            var ValidRecruitJob = EmployeeBz.IsExist(o => o.Name == entity.Name && o.ID != entity.ID);
            if (ValidRecruitJob)
            {
                ViewBag.Msg = $"员工已存在：{entity.Name}！";
                return View("EmployeeEdit", entity);
            }

            var isSuccess = EmployeeBz.SaveOrUpdate(entity, entity.ID > 0);
            if (isSuccess)
            {
                ViewBag.Msg = "保存成功！";
            }
            else
            {
                ViewBag.Msg = "保存失败！";
            }
            var model = EmployeeBz.Get(o => o.Name == entity.Name);
            return View("EmployeeEdit", model);
        }
        public ActionResult EmployeeUnEnable(int id)
        {
            var isSuccess = EmployeeBz.Update(new Employee { ID=id,isValid=false});
            if (isSuccess)
            {
                TempData["Msg"] = "取消成功！";
            }
            else
            {
                TempData["Msg"] = "取消失败！";
            }
            return RedirectToAction("EmployeeList");
        }
        
        public ActionResult EmployeeEnable(int id)
        {
            var isSuccess = EmployeeBz.Update(new Employee { ID = id, isValid = false });
            if (isSuccess)
            {
                TempData["Msg"] = "启动成功！";
            }
            else
            {
                TempData["Msg"] = "启动失败！";
            }
            return RedirectToAction("EmployeeList");
        }
        public ActionResult EmployeeDel(int id)
        {
            var isSuccess = EmployeeBz.Delete(o=>o.ID==id);
            if (isSuccess)
            {
                TempData["Msg"] = "删除成功！";
            }
            else
            {
                TempData["Msg"] = "删除失败！";
            }
            return RedirectToAction("EmployeeList");
        }
    }
}