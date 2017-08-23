namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RoleModulePermission", "UserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RoleModulePermission", "UserName");
        }
    }
}
