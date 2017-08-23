namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserInfo", "UserCode", c => c.String());
            AddColumn("dbo.UserInfo", "HeadImgUrl", c => c.String());
            AlterColumn("dbo.UserInfo", "RoleID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserInfo", "RoleID", c => c.String());
            DropColumn("dbo.UserInfo", "HeadImgUrl");
            DropColumn("dbo.UserInfo", "UserCode");
        }
    }
}
