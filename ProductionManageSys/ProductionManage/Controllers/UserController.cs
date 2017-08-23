using Common;
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
    public class UserController : BaseController
    {
       
        private IRoleInfoManage RoleInfoManageBz = new RoleInfoManage();
        // GET: BackStage/User
        public ActionResult UserConfig(string msg="")
        {
            var model = UserInfoBz.Get(o => o.ID == CurrentUser.ID);
            ViewBag.selectList = GetSelectList();
            ViewBag.Msg = msg;
            return View(model);
        }

        public IEnumerable<SelectListItem> GetSelectList()
        {
            var selectList = RoleInfoManageBz.LoadAll(o=>true).Select(a => new SelectListItem
            {
                Text = a.RoleName,
                Value = a.ID.ToString()
            }).ToList();
            selectList.Insert(0, new SelectListItem
            {
                Text = "请选择",
                Value = "0"
            });
            return selectList;
        }
    
        public ActionResult SaveUserConfig(UserInfo entity)
        {
            var userInfo = UserInfoBz.Get(o => o.ID == entity.ID);
            userInfo.UserName = entity.UserName;
            userInfo.IsValid = entity.IsValid;
            userInfo.IsSuperAdmin = entity.IsSuperAdmin;
            userInfo.HeadImgUrl = entity.HeadImgUrl;
            userInfo.RoleID = entity.RoleID;
            var isSuccess = UserInfoBz.Update(userInfo);
            if (isSuccess)
                ViewBag.Msg = "保存成功！";
            else
                ViewBag.Msg = "保存失败！";
            return RedirectToAction("UserConfig",new { msg= ViewBag.Msg });
        }
        //用户管理  
        public ActionResult UserManageIndex(int p = 1, string searchFild = "")
        {
            var model = UserInfoBz.Query(
                             UserInfoBz.LoadAll(o => o.UserName.Contains(searchFild)).OrderByDescending(o => o.ID),
                             p,
                             10);
            ViewBag.searchFild = searchFild;
            var role = RoleInfoManageBz.LoadAll(o => true);
           
            ViewBag.role = role;
            return View(model);
        }
        public ActionResult UserDel(int id)
        {
            var isSuccess = UserInfoBz.Delete(o => o.ID == id);
            if (isSuccess)
            {
                TempData["Msg"] = "删除成功！";
            }
            else
            {
                TempData["Msg"] = "删除失败！";
            }
            return RedirectToAction("UserManageIndex");
        }
        public ActionResult UserEidt(int id = 0)
        {
            var model = UserInfoBz.Get(o => o.ID == id);
            ViewBag.selectList = GetSelectList();
            return View(model);
        }
        [ValidateInput(false)]
        public ActionResult UserSave(UserInfo entity)
        {
            ViewBag.selectList = GetSelectList();
            var ValidUser = UserInfoBz.IsExist(o => o.UserName == entity.UserName && o.ID != entity.ID);
            if (ValidUser)
            {
                ViewBag.Msg = $"该用户已存在：{entity.UserName}！";
                return View("UserEidt", entity);
            }
            if (string.IsNullOrWhiteSpace(entity.UserName))
            {
                ViewBag.Msg = $"用户名不能为空";
                return View("UserEidt", entity);
            }
            if (entity.ID <= 0)
            {
                entity.Pwd = UserInfoBz.GetPwdAES("123456");
            }
            var isSuccess = UserInfoBz.SaveOrUpdate(entity, entity.ID > 0);
            if (isSuccess)
            {
                ViewBag.Msg = "保存成功！";
            }
            else
            {
                ViewBag.Msg = "保存失败！";
            }
            var model = UserInfoBz.Get(o => o.UserName == entity.UserName);
            return View("UserEidt", model);
        }
       
    }
}