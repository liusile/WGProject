namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SysConfig", "ScanType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SysConfig", "ScanType");
        }
    }
}
