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
    public class RegisterUserController : Controller
    {
        private IUserInfoManage UserInfoBz = new UserInfoManage();
        private IRoleInfoManage RoleInfoManageBz = new RoleInfoManage();

        // GET: RegisterUser 
        public ActionResult Index()
        {
            ViewBag.selectList = GetSelectList();
            return View();
        }

        private IEnumerable<SelectListItem> GetSelectList()
        {
            var selectList = RoleInfoManageBz.LoadAll(o => true).Select(a => new SelectListItem
            {
                Text = a.RoleName,
                Value = a.ID.ToString()
            }).ToList();
            selectList.Insert(0,new SelectListItem
            {
                Text = "请选择",
                Value = "0"
            });
            return selectList;
        }

        public ActionResult SaveUser(UserInfo entity)
        {
            ViewBag.selectList = GetSelectList();
            var VididUser = UserInfoBz.IsExist(o => o.UserName == entity.UserName && o.ID != entity.ID);
            if (VididUser)
            {
                ViewBag.Msg = $"该用户已存在：{entity.UserName}！";
                return View("Index", entity);
            }
            if (string.IsNullOrWhiteSpace(entity.UserName))
            {
                ViewBag.Msg = $"用户名称不能为空";
                return View("Index", entity);
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
            return View("Index", model);

        }
    }
}