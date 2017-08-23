using Domain.Models.HeadStage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.IService.HeadStage;
using Spring.Context.Support;
using Spring.Context;
using Common.WebHelper;

namespace WG_BackStage.Areas.HeadStage.Controllers
{
    public class RecruitController : Controller
    {
        public IApplyJob ApplyJobBz { get; set; }
        public IRecruitJob RecruitJobBz { get; set; }
        // GET: HeadStage/Recruit
        public ActionResult Recruit()
        {
            var list = RecruitJobBz.LoadAll(o => o.isValid == true);
            return View(list);
        }
        public ActionResult AddTeam()
        {
            return View();
        }
        public JsonResult Save(ApplyJob job)
        {
            if (ApplyJobBz.Save(job))
            {
                return Json(new { IsSuccess=true,Msg= "提交成功！" });
            }
            else
            {
                return Json(new { IsSuccess = false, Msg = "提交失败！" });
            }
        }
    }
}