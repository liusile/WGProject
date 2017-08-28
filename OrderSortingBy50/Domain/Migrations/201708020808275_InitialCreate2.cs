namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "ProductCode", c => c.String(maxLength: 4000));
            AddColumn("dbo.Product", "PropertyCode", c => c.String(maxLength: 4000));
            AddColumn("dbo.Product", "Feature", c => c.String(maxLength: 4000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "Feature");
            DropColumn("dbo.Product", "PropertyCode");
            DropColumn("dbo.Product", "ProductCode");
        }
    }
}
