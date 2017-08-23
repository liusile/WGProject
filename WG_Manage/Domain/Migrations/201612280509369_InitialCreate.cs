namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Company",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Generalization = c.String(),
                        Culture = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        ImgUrl = c.String(),
                        Describe = c.String(),
                        isValid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ApplyJob",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Age = c.String(),
                        Email = c.String(),
                        ApplyJobName = c.String(),
                        Talk = c.String(),
                        ApplyTime = c.String(),
                        Status = c.Int(nullable: false),
                        ApprovePepple = c.String(),
                        ApproveTime = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        KeyWord = c.String(),
                        ImgUrl = c.String(),
                        Year = c.String(),
                        Month = c.String(),
                        Day = c.String(),
                        summary = c.String(),
                        Content = c.String(),
                        CreatePeople = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        Describe = c.String(),
                        Instructions = c.String(),
                        ImgUrl1 = c.String(),
                        ImgDescribe1 = c.String(),
                        ImgUrl2 = c.String(),
                        ImgDescribe2 = c.String(),
                        ImgUrl3 = c.String(),
                        ImgDescribe3 = c.String(),
                        ImgUrl4 = c.String(),
                        ImgDescribe4 = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ProductAttribute",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AtrName = c.String(),
                        AtrValue = c.String(),
                        isValid = c.Boolean(nullable: false),
                        Product_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Product", t => t.Product_ID)
                .Index(t => t.Product_ID);
            
            CreateTable(
                "dbo.RecruitJob",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        JobName = c.String(nullable: false),
                        Condition1 = c.String(),
                        Condition2 = c.String(),
                        Condition3 = c.String(),
                        Condition4 = c.String(),
                        Condition5 = c.String(),
                        JobDetail1 = c.String(),
                        JobDetail2 = c.String(),
                        JobDetail3 = c.String(),
                        JobDetail4 = c.String(),
                        JobDetail5 = c.String(),
                        Address = c.String(),
                        Seq = c.Int(nullable: false),
                        isValid = c.Boolean(nullable: false),
                        CreatePeople = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserInfo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Pwd = c.String(),
                        RoleID = c.String(),
                        IsValid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductAttribute", "Product_ID", "dbo.Product");
            DropIndex("dbo.ProductAttribute", new[] { "Product_ID" });
            DropTable("dbo.UserInfo");
            DropTable("dbo.RecruitJob");
            DropTable("dbo.ProductAttribute");
            DropTable("dbo.Product");
            DropTable("dbo.News");
            DropTable("dbo.ApplyJob");
            DropTable("dbo.Employee");
            DropTable("dbo.Company");
        }
    }
}
