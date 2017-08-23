namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductAttribute", "Product_ID", "dbo.Product");
            DropIndex("dbo.ProductAttribute", new[] { "Product_ID" });
            RenameColumn(table: "dbo.ProductAttribute", name: "Product_ID", newName: "ProductID");
            AlterColumn("dbo.ProductAttribute", "ProductID", c => c.Int(nullable: false));
            CreateIndex("dbo.ProductAttribute", "ProductID");
            AddForeignKey("dbo.ProductAttribute", "ProductID", "dbo.Product", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductAttribute", "ProductID", "dbo.Product");
            DropIndex("dbo.ProductAttribute", new[] { "ProductID" });
            AlterColumn("dbo.ProductAttribute", "ProductID", c => c.Int());
            RenameColumn(table: "dbo.ProductAttribute", name: "ProductID", newName: "Product_ID");
            CreateIndex("dbo.ProductAttribute", "Product_ID");
            AddForeignKey("dbo.ProductAttribute", "Product_ID", "dbo.Product", "ID");
        }
    }
}
