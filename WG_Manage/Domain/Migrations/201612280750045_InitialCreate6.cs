namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate6 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.testD", "testM_ID", "dbo.testM");
            DropIndex("dbo.testD", new[] { "testM_ID" });
            DropTable("dbo.testD");
            DropTable("dbo.testM");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.testM",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AtrName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.testD",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        dfdf = c.Int(nullable: false),
                        AtrNamedd = c.String(),
                        testM_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.testD", "testM_ID");
            AddForeignKey("dbo.testD", "testM_ID", "dbo.testM", "ID");
        }
    }
}
