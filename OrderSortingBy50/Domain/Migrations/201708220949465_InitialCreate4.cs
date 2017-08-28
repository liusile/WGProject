namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SysConfig", "ScanPortName", c => c.String(maxLength: 4000));
            DropColumn("dbo.SysConfig", "ScanType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SysConfig", "ScanType", c => c.Int(nullable: false));
            DropColumn("dbo.SysConfig", "ScanPortName");
        }
    }
}
