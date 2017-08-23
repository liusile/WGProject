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
                new Permission {PermissionCode="View",PermissionName="����" },
                new Permission {PermissionCode="Add", PermissionName="����" },
                new Permission {PermissionCode="Update",PermissionName="����" },
                new Permission {PermissionCode="Delete",PermissionName="ɾ��" },
                new Permission {PermissionCode="Approve",PermissionName="����" },
                new Permission {PermissionCode="Reject",PermissionName="����" }
            };
            PermissionList.ForEach(o =>
                context.Permission.AddOrUpdate(p => p.PermissionCode,o)
            );
            //RoleInfo
            context.RoleInfo.AddOrUpdate(p => p.RoleCode, new RoleInfo {RoleCode="admind",RoleName="����Ա"});
            //Module
            List<Module> ModuleList = new List<Module>
            {
                new Module { AreaName = "BackStage",ControllerName = "Home",ActionName="Index",ModuleCode= "BackStage_Home_Index",ModuleName="��̨��ҳ" }
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
                        Pwd = "4438393244333334393334394438463735354642382020202020202020202020172559787AF01F38C23E66B53DB1D839", //�������룺admin
                        RoleID = 1,
                        UserCode = "admin",
                        UserName = "admin"
                    });
            //

        }
    }
}
