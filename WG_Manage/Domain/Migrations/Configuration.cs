namespace Domain.Migrations
{
    using Models.BackStage;
    using Models.HeadStage;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Domain.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Domain.ApplicationDbContext context)
        {
            //Permission
            List<Permission> PermissionList = new List<Permission>
            {
                new Permission {PermissionCode="View",PermissionName="游览" },
                new Permission {PermissionCode="Add", PermissionName="新增" },
                new Permission {PermissionCode="Update",PermissionName="更新" },
                new Permission {PermissionCode="Delete",PermissionName="删除" },
                new Permission {PermissionCode="Approve",PermissionName="审批" },
                new Permission {PermissionCode="Reject",PermissionName="驳回" }
            };
            PermissionList.ForEach(o =>
                context.Permission.AddOrUpdate(p => p.PermissionCode,o)
            );
            //RoleInfo
            context.RoleInfo.AddOrUpdate(p => p.RoleCode, new RoleInfo {RoleCode="admind",RoleName="管理员"});
            //Module
            List<Module> ModuleList = new List<Module>
            {
                new Module { AreaName = "BackStage",ControllerName = "Home",ActionName="Index",ModuleCode= "BackStage_Home_Index",ModuleName="后台首页" }
            };
            ModuleList.ForEach(o =>
               context.Module.AddOrUpdate(p => p.ModuleCode, o)
            );
            //RoleModulePermission 
            context.RoleModulePermission.AddOrUpdate(p => p.ID,
                    new RoleModulePermission
                    {
                        ID=1,
                        UserName="admin",
                        ModuleID=1,
                        RoleID=1,
                        PermissionID = 1
                    });
            //UserInfo
            context.UserInfo.AddOrUpdate(p => p.UserCode,
                    new UserInfo
                    {
                        HeadImgUrl = "",
                        IsSuperAdmin = false,
                        IsValid = true,
                        Pwd = "4438393244333334393334394438463735354642382020202020202020202020172559787AF01F38C23E66B53DB1D839", //明文密码：admin
                        RoleID = 1,
                        UserCode = "admin",
                        UserName = "admin"
                    });
            //

        }
    }
}
