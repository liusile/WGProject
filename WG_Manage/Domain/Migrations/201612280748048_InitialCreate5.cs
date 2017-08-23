namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.testD", "testMID", "dbo.testM");
            DropIndex("dbo.testD", new[] { "testMID" });
            RenameColumn(table: "dbo.testD", name: "testMID", newName: "testM_ID");
            AddColumn("dbo.testD", "dfdf", c => c.Int(nullable: false));
            AlterColumn("dbo.testD", "testM_ID", c => c.Int());
            CreateIndex("dbo.testD", "testM_ID");
            AddForeignKey("dbo.testD", "testM_ID", "dbo.testM", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.testD", "testM_ID", "dbo.testM");
            DropIndex("dbo.testD", new[] { "testM_ID" });
            AlterColumn("dbo.testD", "testM_ID", c => c.Int(nullable: false));
            DropColumn("dbo.testD", "dfdf");
            RenameColumn(table: "dbo.testD", name: "testM_ID", newName: "testMID");
            CreateIndex("dbo.testD", "testMID");
            AddForeignKey("dbo.testD", "testMID", "dbo.testM", "ID", cascadeDelete: true);
        }
    }
}
