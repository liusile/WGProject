namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.testD",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        testMID = c.Int(nullable: false),
                        AtrNamedd = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.testM", t => t.testMID, cascadeDelete: true)
                .Index(t => t.testMID);
            
            CreateTable(
                "dbo.testM",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AtrName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.testD", "testMID", "dbo.testM");
            DropIndex("dbo.testD", new[] { "testMID" });
            DropTable("dbo.testM");
            DropTable("dbo.testD");
        }
    }
}
