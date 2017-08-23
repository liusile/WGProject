using Service.IService;
using Service.ServiceImp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductionManage.Controllers
{
    public class PartialController : BaseController
    {
        public IRoleModulePermissionManage RoleModulePermissionManageBz = new RoleModulePermissionManage();
        public IModuleManage ModuleManageBz = new ModuleManage();

        // GET: BackStage/Partial
        [AllowAnonymous]
        [ChildActionOnly]
        public PartialViewResult PartialHead()
        {
            ViewBag.MenuHead = ModuleManageBz.LoadAll(o => o.ParentModuleCode == 0).OrderBy(o=>o.Sort).ToList();
            if (CurrentUser.IsSuperAdmin)
            {
                ViewBag.MenuSub = ModuleManageBz.LoadAll(o => o.PermissionName == "View").OrderBy(o => o.Sort).ToList();
            }
            else
            {
                var perission = RoleModulePermissionManageBz.LoadAll(o => o.RoleID == CurrentUser.RoleID).Select(o => o.ModuleID).ToList();
                ViewBag.MenuSub = ModuleManageBz.LoadAll(o => o.PermissionName == "View").Where(p => perission.Contains(p.ID)).OrderBy(o => o.Sort).ToList();
            }
            
            return PartialView(this.CurrentUser);
        }


    }
}