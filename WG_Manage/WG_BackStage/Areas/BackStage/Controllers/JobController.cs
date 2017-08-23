using Domain.Models.HeadStage;
using Service.IService.HeadStage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WG_BackStage.Areas.BackStage.Controllers
{
    public class JobController : BaseController
    {
        IRecruitJob RecruitJobBz { get; set;}
        IApplyJob ApplyJobBz { get; set; }
        /// <summary>
        /// 应聘职位列表
        /// </summary>
        /// <returns></returns>
        public ActionResult AppJobList(int p = 1, string searchFild = "")
        {
            var model = ApplyJobBz.Query(
                              ApplyJobBz.LoadAll(o => o.ApplyJobName.Contains(searchFild)).OrderByDescending(o => o.ApplyTime),
                              p,
                              10);
            ViewBag.searchFild = searchFild;
            return View(model);
        }
        public ActionResult ApplyJobDetail(int id)
        {
            var model = ApplyJobBz.Get(o => o.ID == id);
            return View(model);
        }
        public ActionResult ApplyJobDel(int id)
        {
            var isSuccess = ApplyJobBz.Delete(o => o.ID == id);
            if (isSuccess)
            {
                TempData["Msg"] = "删除成功！";
            }
            else
            {
                TempData["Msg"] = "删除失败！";
            }
            return RedirectToAction("AppJobList");
        }
        public ActionResult ApplyJobApprove(int id)
        {
            var isSuccess = ApplyJobBz.Update(new ApplyJob { ID = id, ApproveTime = DateTime.Now.ToString(), ApprovePepple = CurrentUser.UserName, Status = 1 });
            if (isSuccess)
            {
                TempData["Msg"] = "审批成功！";
            }
            else
            {
                TempData["Msg"] = "审批失败！";
            }
            return RedirectToAction("AppJobList");
        }
        public ActionResult ApplyJobBack(int id)
        {
            var isSuccess = ApplyJobBz.Update(new ApplyJob { ID = id, ApproveTime = DateTime.Now.ToString(), ApprovePepple = CurrentUser.UserName, Status = 0 });
            if (isSuccess)
            {
                TempData["Msg"] = "驳回成功！";
            }else
            {
                TempData["Msg"] = "驳回失败！";
            }
            return RedirectToAction("AppJobList");
        }
        /// <summary>
        /// 招聘职位列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="searchFild"></param>
        /// <returns></returns>
        public ActionResult RecruitJobList(int p=1,string searchFild="")
        {
            var  model = RecruitJobBz.Query(
                                RecruitJobBz.LoadAll(o => o.JobName.Contains(searchFild)).OrderByDescending(o => o.Seq),
                                p,
                                10);
            ViewBag.searchFild = searchFild;
                return View(model);
        }
       
        public ActionResult RecruitJobEdit(int id)
        {
            var model = RecruitJobBz.Get(o => o.ID == id);
            return View(model);
        }
        public ActionResult RecruitJobAdd()
        {
            return View("RecruitJobEdit");
        }
        public ActionResult RecruitJobDel(int id)
        {
            var isSuccess = RecruitJobBz.Delete(o => o.ID == id);
            if (isSuccess)
            {
                TempData["Msg"] = "删除成功！";
            }
            else
            {
                TempData["Msg"] = "删除失败！";
            }
            return RedirectToAction("RecruitJobList");
        }
        public ActionResult Save(RecruitJob entity)
        {
            var ValidRecruitJob = RecruitJobBz.IsExist(o => o.JobName == entity.JobName && o.ID!=entity.ID);
            if (ValidRecruitJob)
            {
                ViewBag.Msg = $"招聘的职位已存在{entity.JobName}！";
                return View("RecruitJobEdit", entity);
            }
            
            var isSuccess = RecruitJobBz.SaveOrUpdate(entity, entity.ID > 0);
            if (isSuccess)
            {
                ViewBag.Msg = "保存成功！";
            }
            else
            {
                ViewBag.Msg = "保存失败！";
            }
            var model = RecruitJobBz.Get(o => o.JobName == entity.JobName);
            return View("RecruitJobEdit", model);
        }
        
    }
}