using Service.IService.HeadStage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WG_BackStage.Areas.HeadStage.Controllers
{
    public class AboutUsController : Controller
    {
        ICompany CompanyBz { get; set; }
        IEmployee EmployeeBz { get; set; }
        // GET: HeadStage/AboutUs
        public ActionResult AboutUs()
        {
            var Company = CompanyBz.Get(o => true);
            ViewBag.Generalization = Company.Generalization;
            ViewBag.Culture = Company.Culture;

            var Employee = EmployeeBz.LoadAll(o => o.isValid == true);
            return View(Employee);
        }
       
    }
}