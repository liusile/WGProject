namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate8 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Module",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ModuleCode = c.String(),
                        ModuleName = c.String(),
                        ControllerName = c.String(),
                        ActionName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Permission",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PermissionCode = c.String(),
                        PermissionName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RoleInfo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RoleModulePermission",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RoleID = c.String(),
                        ModuleID = c.String(),
                        PermissionID = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.UserInfo", "IsSuperAdmin", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserInfo", "IsSuperAdmin");
            DropTable("dbo.RoleModulePermission");
            DropTable("dbo.RoleInfo");
            DropTable("dbo.Permission");
            DropTable("dbo.Module");
        }
    }
}
