namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderApi",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderNo = c.String(maxLength: 4000),
                        Status = c.Int(nullable: false),
                        WaveApi_WaveNo = c.String(nullable: false, maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WaveApi", t => t.WaveApi_WaveNo, cascadeDelete: true)
                .Index(t => t.WaveApi_WaveNo);
            
            CreateTable(
                "dbo.ProductApi",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductCode = c.String(maxLength: 4000),
                        PropertyCode = c.String(maxLength: 4000),
                        BarCode = c.String(maxLength: 4000),
                        Feature = c.String(maxLength: 4000),
                        ProductName = c.String(maxLength: 4000),
                        Num = c.Int(nullable: false),
                        OrderApi_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrderApi", t => t.OrderApi_Id, cascadeDelete: true)
                .Index(t => t.OrderApi_Id);
            
            CreateTable(
                "dbo.WaveApi",
                c => new
                    {
                        WaveNo = c.String(nullable: false, maxLength: 4000),
                        Status = c.Int(nullable: false),
                        LastTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.WaveNo);
            
            CreateTable(
                "dbo.SlaveInfo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WaveNo = c.String(maxLength: 4000),
                        NeedWaitPutByApi = c.Int(nullable: false),
                        IsCompleteHand = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LatticeInfo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LatticeNo = c.String(maxLength: 4000),
                        OrderNo = c.String(maxLength: 4000),
                        IsPrint = c.Boolean(nullable: false),
                        Status = c.Int(nullable: false),
                        SlaveInfo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SlaveInfo", t => t.SlaveInfo_Id)
                .Index(t => t.SlaveInfo_Id);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BarCode = c.String(maxLength: 4000),
                        ProductName = c.String(maxLength: 4000),
                        WaitNum = c.Int(nullable: false),
                        PutNum = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        LatticeInfo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LatticeInfo", t => t.LatticeInfo_Id)
                .Index(t => t.LatticeInfo_Id);
            
            CreateTable(
                "dbo.SysConfig",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IP = c.String(maxLength: 4000),
                        LatticeLineCount = c.Int(nullable: false),
                        Port = c.String(maxLength: 4000),
                        PrintType = c.Int(nullable: false),
                        DomainName = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserInfo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        UserName = c.String(maxLength: 4000),
                        Pcid = c.String(maxLength: 4000),
                        PcName = c.String(maxLength: 4000),
                        IsRemeberUserName = c.Boolean(nullable: false),
                        IsAutoLogin = c.Boolean(nullable: false),
                        LastLoginTime = c.DateTime(nullable: false),
                        Email = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LatticeInfo", "SlaveInfo_Id", "dbo.SlaveInfo");
            DropForeignKey("dbo.Product", "LatticeInfo_Id", "dbo.LatticeInfo");
            DropForeignKey("dbo.OrderApi", "WaveApi_WaveNo", "dbo.WaveApi");
            DropForeignKey("dbo.ProductApi", "OrderApi_Id", "dbo.OrderApi");
            DropIndex("dbo.Product", new[] { "LatticeInfo_Id" });
            DropIndex("dbo.LatticeInfo", new[] { "SlaveInfo_Id" });
            DropIndex("dbo.ProductApi", new[] { "OrderApi_Id" });
            DropIndex("dbo.OrderApi", new[] { "WaveApi_WaveNo" });
            DropTable("dbo.UserInfo");
            DropTable("dbo.SysConfig");
            DropTable("dbo.Product");
            DropTable("dbo.LatticeInfo");
            DropTable("dbo.SlaveInfo");
            DropTable("dbo.WaveApi");
            DropTable("dbo.ProductApi");
            DropTable("dbo.OrderApi");
        }
    }
}
