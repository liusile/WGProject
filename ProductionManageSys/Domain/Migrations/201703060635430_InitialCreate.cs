namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AcceptOrder",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PurchaseOrderNo = c.String(),
                        AcceptOrderNo = c.String(),
                        Accepter = c.String(),
                        AcceptDate = c.String(),
                        AcceptNum = c.Int(nullable: false),
                        Status = c.String(),
                        Remark = c.String(),
                        Oper = c.String(),
                        SumPrice = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AcceptOrderDetail",
                c => new
                    {
                        AcceptOrderId = c.Int(nullable: false),
                        ProductId = c.String(nullable: false, maxLength: 128),
                        ProductName = c.String(),
                        AcceptNum = c.Int(nullable: false),
                        Price = c.Single(nullable: false),
                        SumPrice = c.Single(nullable: false),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => new { t.AcceptOrderId, t.ProductId })
                .ForeignKey("dbo.AcceptOrder", t => t.AcceptOrderId, cascadeDelete: true)
                .Index(t => t.AcceptOrderId);
            
            CreateTable(
                "dbo.BOM",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ModuleName = c.String(),
                        Type = c.String(),
                        BOMStatus = c.String(),
                        CreatePeople = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        LastOper = c.String(),
                        LastOperTime = c.DateTime(nullable: false),
                        ParentModuleID = c.Int(nullable: false),
                        ModuleNum = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ProductBOM",
                c => new
                    {
                        BOMID = c.Int(nullable: false),
                        ProductName = c.String(nullable: false, maxLength: 128),
                        Num = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BOMID, t.ProductName })
                .ForeignKey("dbo.BOM", t => t.BOMID, cascadeDelete: true)
                .Index(t => t.BOMID);
            
            CreateTable(
                "dbo.Module",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ModuleCode = c.Int(nullable: false),
                        ParentModuleCode = c.Int(nullable: false),
                        ModuleName = c.String(),
                        AreaName = c.String(),
                        ControllerName = c.String(),
                        ActionName = c.String(),
                        PermissionName = c.String(),
                        Sort = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        Price = c.Single(nullable: false),
                        SumNum = c.Int(nullable: false),
                        LockNum = c.Int(nullable: false),
                        From = c.String(),
                        Remark = c.String(),
                        WaringMin = c.Int(),
                        WaringMax = c.Int(),
                        Location = c.String(),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductLog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        In1Out = c.String(),
                        num = c.Int(nullable: false),
                        Remark = c.String(),
                        OperTime = c.DateTime(nullable: false),
                        Oper = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductPutOutOrder",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProductOrderNo = c.String(),
                        Content = c.String(),
                        ProductPutOrderDate = c.DateTime(nullable: false),
                        SumPrice = c.Single(nullable: false),
                        Status = c.String(),
                        Remark = c.String(),
                        Oper = c.String(),
                        SumNum = c.Int(nullable: false),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ProductPutOutOrderDetail",
                c => new
                    {
                        ProductPutOutOrderId = c.Int(nullable: false),
                        ProductName = c.String(nullable: false, maxLength: 128),
                        Num = c.Int(nullable: false),
                        Price = c.Single(nullable: false),
                        SumPrice = c.Single(nullable: false),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => new { t.ProductPutOutOrderId, t.ProductName })
                .ForeignKey("dbo.ProductPutOutOrder", t => t.ProductPutOutOrderId, cascadeDelete: true)
                .Index(t => t.ProductPutOutOrderId);
            
            CreateTable(
                "dbo.PurchaseOrder",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PurchaseOrderNo = c.String(),
                        Content = c.String(),
                        Purchaser = c.String(),
                        PurchaseDate = c.String(),
                        PreCompleteDate = c.String(),
                        Supplier = c.String(),
                        SumPrice = c.Single(nullable: false),
                        Status = c.String(),
                        Remark = c.String(),
                        Oper = c.String(),
                        PurchaseNum = c.Int(nullable: false),
                        PurchaseNumWait = c.Int(nullable: false),
                        PurchaseNumOver = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PurchaseOrderDetail",
                c => new
                    {
                        PurchaseOrderId = c.Int(nullable: false),
                        ProductId = c.String(nullable: false, maxLength: 128),
                        ProductName = c.String(),
                        PurchaseNum = c.Int(nullable: false),
                        PurchaseNumWait = c.Int(nullable: false),
                        PurchaseNumOver = c.Int(nullable: false),
                        Price = c.Single(nullable: false),
                        SumPrice = c.Single(nullable: false),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => new { t.PurchaseOrderId, t.ProductId })
                .ForeignKey("dbo.PurchaseOrder", t => t.PurchaseOrderId, cascadeDelete: true)
                .Index(t => t.PurchaseOrderId);
            
            CreateTable(
                "dbo.RoleInfo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RoleCode = c.String(),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RoleModulePermission",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RoleID = c.Int(nullable: false),
                        ModuleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Supplier",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CorpName = c.String(),
                        Address = c.String(),
                        People = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Level = c.Int(nullable: false),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SupplierProduct",
                c => new
                    {
                        SupplierId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        Price = c.String(),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => new { t.SupplierId, t.ProductId });
            
            CreateTable(
                "dbo.UserInfo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserCode = c.String(),
                        UserName = c.String(),
                        Pwd = c.String(),
                        RoleID = c.Int(nullable: false),
                        HeadImgUrl = c.String(),
                        IsValid = c.Boolean(nullable: false),
                        IsSuperAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PurchaseOrderDetail", "PurchaseOrderId", "dbo.PurchaseOrder");
            DropForeignKey("dbo.ProductPutOutOrderDetail", "ProductPutOutOrderId", "dbo.ProductPutOutOrder");
            DropForeignKey("dbo.ProductBOM", "BOMID", "dbo.BOM");
            DropForeignKey("dbo.AcceptOrderDetail", "AcceptOrderId", "dbo.AcceptOrder");
            DropIndex("dbo.PurchaseOrderDetail", new[] { "PurchaseOrderId" });
            DropIndex("dbo.ProductPutOutOrderDetail", new[] { "ProductPutOutOrderId" });
            DropIndex("dbo.ProductBOM", new[] { "BOMID" });
            DropIndex("dbo.AcceptOrderDetail", new[] { "AcceptOrderId" });
            DropTable("dbo.UserInfo");
            DropTable("dbo.SupplierProduct");
            DropTable("dbo.Supplier");
            DropTable("dbo.RoleModulePermission");
            DropTable("dbo.RoleInfo");
            DropTable("dbo.PurchaseOrderDetail");
            DropTable("dbo.PurchaseOrder");
            DropTable("dbo.ProductPutOutOrderDetail");
            DropTable("dbo.ProductPutOutOrder");
            DropTable("dbo.ProductLog");
            DropTable("dbo.Product");
            DropTable("dbo.Module");
            DropTable("dbo.ProductBOM");
            DropTable("dbo.BOM");
            DropTable("dbo.AcceptOrderDetail");
            DropTable("dbo.AcceptOrder");
        }
    }
}
