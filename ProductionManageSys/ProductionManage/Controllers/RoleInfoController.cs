using Domain.Model;
using Service.IService;
using Service.Model;
using Service.ServiceImp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductionManage.Controllers
{
    public class RoleInfoController : BaseController
    {
        public IRoleInfoManage RoleInfoManageBz = new RoleInfoManage();
        public IUserInfoManage UserInfoManageBz = new UserInfoManage();
        public RoleModulePermissionManage RoleModulePermissionManageBz = new RoleModulePermissionManage();

        // GET: RoleInfo
        public ActionResult Index(int p = 1, string searchFild = "")
        {
            var model = RoleInfoManageBz.Query(
                            RoleInfoManageBz.LoadAll(o => o.RoleName.Contains(searchFild)).OrderByDescending(o => o.ID),
                            p,
                            10);
            ViewBag.searchFild = searchFild;
            return View(model);
        }
        
        public ActionResult RoleAdd()
        {
            var model = new RoleInfo();
            return View(model);
        }
        public ActionResult RoleEidtPermission(int id = 0)
        {
            ViewBag.id = id;
            var model = UserInfoManageBz.GetRolePermission(id);            
            return View(model);
        }
        
        public ActionResult Save(RoleInfo entity)
        {
            var ValidRecruitJob = RoleInfoManageBz.IsExist(o => o.RoleName == entity.RoleName && o.ID != entity.ID);
            if (ValidRecruitJob)
            {
                ViewBag.Msg = $"该角色已存在：{entity.RoleName}！";
                return View("RoleAdd", entity);
            }
            if (string.IsNullOrWhiteSpace(entity.RoleName))
            {
                ViewBag.Msg = $"角色名称不能为空";
                return View("RoleAdd", entity);
            }
            var isSuccess = RoleInfoManageBz.SaveOrUpdate(entity, entity.ID > 0);
            if (isSuccess)
            {
                ViewBag.Msg = "保存成功！";
            }
            else
            {
                ViewBag.Msg = "保存失败！";
            }
            var model = RoleInfoManageBz.Get(o => o.RoleName == entity.RoleName);
            return View("RoleAdd", model);
        }
        public JsonResult SavePermission(List<int> a,int id)
        {
            RoleModulePermissionManageBz.Delete(o => o.RoleID == id,false);
            var modelList = new List<RoleModulePermission>();
            a.ForEach(o =>
            {
                modelList.Add(new RoleModulePermission
                {
                    ModuleID = o,
                    RoleID = id
                });
            });

            bool isSuccess = RoleModulePermissionManageBz.SaveList(modelList);
            if(isSuccess)
            {
                return Json(new { isSuccess = true, Msg = "提交成功！" });
            }else
            {
                return Json(new { isSuccess = false, Msg = "提交失败！" });
            }
           
        }

    }
}