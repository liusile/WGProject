namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Module", "AreaName", c => c.String());
            AddColumn("dbo.RoleInfo", "RoleCode", c => c.String());
            AlterColumn("dbo.RoleModulePermission", "RoleID", c => c.Int(nullable: false));
            AlterColumn("dbo.RoleModulePermission", "ModuleID", c => c.Int(nullable: false));
            AlterColumn("dbo.RoleModulePermission", "PermissionID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RoleModulePermission", "PermissionID", c => c.String());
            AlterColumn("dbo.RoleModulePermission", "ModuleID", c => c.String());
            AlterColumn("dbo.RoleModulePermission", "RoleID", c => c.String());
            DropColumn("dbo.RoleInfo", "RoleCode");
            DropColumn("dbo.Module", "AreaName");
        }
    }
}
