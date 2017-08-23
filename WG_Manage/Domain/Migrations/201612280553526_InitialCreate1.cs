namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ProductAttribute", "isValid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductAttribute", "isValid", c => c.Boolean(nullable: false));
        }
    }
}
