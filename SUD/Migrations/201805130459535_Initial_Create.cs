namespace SUD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_Create : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tbl_Products", "Department_DepartmentId", "dbo.tbl_Departments");
            DropForeignKey("dbo.tbl_Products", "Measure_MeasureId", "dbo.tbl_Measures");
            DropForeignKey("dbo.tbl_Inventories", "Cellar_CellarId", "dbo.tbl_Cellars");
            DropForeignKey("dbo.tbl_InventoryDetails", "Inventory_InventoryId", "dbo.tbl_Inventories");
            DropIndex("dbo.tbl_Products", new[] { "Department_DepartmentId" });
            DropIndex("dbo.tbl_Products", new[] { "Measure_MeasureId" });
            DropIndex("dbo.tbl_Inventories", new[] { "Cellar_CellarId" });
            DropIndex("dbo.tbl_InventoryDetails", new[] { "Inventory_InventoryId" });
            RenameColumn(table: "dbo.tbl_Products", name: "Department_DepartmentId", newName: "DepartmentId");
            RenameColumn(table: "dbo.tbl_Products", name: "Measure_MeasureId", newName: "MeasureId");
            RenameColumn(table: "dbo.tbl_Inventories", name: "Cellar_CellarId", newName: "CellarId");
            RenameColumn(table: "dbo.tbl_InventoryDetails", name: "Inventory_InventoryId", newName: "InventoryId");
            CreateTable(
                "dbo.Kardexes",
                c => new
                    {
                        KardexId = c.Int(nullable: false, identity: true),
                        CellarId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Document = c.String(),
                        Entry = c.Single(nullable: false),
                        Egress = c.Single(nullable: false),
                        Balance = c.Single(nullable: false),
                        LastCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AverageCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.KardexId);
            
            CreateTable(
                "dbo.EgressDetails",
                c => new
                    {
                        EgressDetailsId = c.Int(nullable: false, identity: true),
                        EgressId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Description = c.String(),
                        Quantity = c.Single(nullable: false),
                        KardexId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EgressDetailsId)
                .ForeignKey("dbo.Egresses", t => t.EgressId, cascadeDelete: true)
                .ForeignKey("dbo.Kardexes", t => t.KardexId, cascadeDelete: true)
                .ForeignKey("dbo.tbl_Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.EgressId)
                .Index(t => t.ProductId)
                .Index(t => t.KardexId);
            
            CreateTable(
                "dbo.Egresses",
                c => new
                    {
                        EgressId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        ConceptId = c.Int(nullable: false),
                        CellarId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EgressId)
                .ForeignKey("dbo.tbl_Cellars", t => t.CellarId, cascadeDelete: true)
                .ForeignKey("dbo.Concepts", t => t.ConceptId, cascadeDelete: true)
                .Index(t => t.ConceptId)
                .Index(t => t.CellarId);
            
            CreateTable(
                "dbo.Concepts",
                c => new
                    {
                        ConceptId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ConceptId);
            
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        PurchaseId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        SupplierId = c.Int(nullable: false),
                        CellarId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PurchaseId)
                .ForeignKey("dbo.tbl_Cellars", t => t.CellarId, cascadeDelete: true)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId, cascadeDelete: true)
                .Index(t => t.SupplierId)
                .Index(t => t.CellarId);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        SupplierId = c.Int(nullable: false, identity: true),
                        Tradename = c.String(),
                        DocumentTypeId = c.Int(nullable: false),
                        Document = c.String(),
                        ContactFirstName = c.String(),
                        ContactLastName = c.String(),
                        Address = c.String(),
                        Phone1 = c.Long(nullable: false),
                        Phone2 = c.Long(nullable: false),
                        Email = c.String(),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.SupplierId)
                .ForeignKey("dbo.tbl_DocumentsType", t => t.DocumentTypeId, cascadeDelete: true)
                .Index(t => t.DocumentTypeId);
            
            CreateTable(
                "dbo.tbl_DocumentsType",
                c => new
                    {
                        DocumentTypeId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.DocumentTypeId);
            
            CreateTable(
                "dbo.tbl_Clients",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        Document = c.String(nullable: false, maxLength: 13),
                        ComertialName = c.String(),
                        FirstNameContact = c.String(nullable: false),
                        LastNameContact = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Telephone1 = c.Int(nullable: false),
                        Telephone2 = c.Int(nullable: false),
                        Mail = c.String(),
                        Note = c.String(),
                        DocumentTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ClientId)
                .ForeignKey("dbo.tbl_DocumentsType", t => t.DocumentTypeId, cascadeDelete: true)
                .Index(t => t.DocumentTypeId);
            
            CreateTable(
                "dbo.tbl_Sales",
                c => new
                    {
                        SaleId = c.Int(nullable: false, identity: true),
                        Datetime = c.DateTime(nullable: false),
                        ClientId = c.Int(nullable: false),
                        CellarId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SaleId)
                .ForeignKey("dbo.tbl_Cellars", t => t.CellarId, cascadeDelete: true)
                .ForeignKey("dbo.tbl_Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .Index(t => t.CellarId);
            
            CreateTable(
                "dbo.tbl_ClientRefunds",
                c => new
                    {
                        ClientRefundId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        SaleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ClientRefundId)
                .ForeignKey("dbo.tbl_Sales", t => t.SaleId, cascadeDelete: true)
                .Index(t => t.SaleId);
            
            CreateTable(
                "dbo.tbl_ClientRefundDetails",
                c => new
                    {
                        ClientRefundDetailId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 140),
                        price = c.Single(nullable: false),
                        Cantidad = c.Int(nullable: false),
                        DiscountRate = c.Single(nullable: false),
                        ClientRefundId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        KardexId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ClientRefundDetailId)
                .ForeignKey("dbo.tbl_ClientRefunds", t => t.ClientRefundId, cascadeDelete: true)
                .ForeignKey("dbo.tbl_Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ClientRefundId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.tbl_SaleDetails",
                c => new
                    {
                        SaleDetailId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 140),
                        Price = c.Single(nullable: false),
                        Quantity = c.Int(nullable: false),
                        IVAPercentage = c.Single(nullable: false),
                        DiscountRate = c.Single(nullable: false),
                        SaleId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        KardexId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SaleDetailId)
                .ForeignKey("dbo.tbl_Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.tbl_Sales", t => t.SaleId, cascadeDelete: true)
                .Index(t => t.SaleId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.PurchaseDetails",
                c => new
                    {
                        PurchaseDetailsId = c.Int(nullable: false, identity: true),
                        PurchaseId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Description = c.String(),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Single(nullable: false),
                        KardexId = c.Int(nullable: false),
                        VATRate = c.Single(nullable: false),
                        DiscountRate = c.Single(nullable: false),
                        ManufacturingLot = c.String(),
                        DueDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PurchaseDetailsId)
                .ForeignKey("dbo.Kardexes", t => t.KardexId, cascadeDelete: true)
                .ForeignKey("dbo.tbl_Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Purchases", t => t.PurchaseId, cascadeDelete: true)
                .Index(t => t.PurchaseId)
                .Index(t => t.ProductId)
                .Index(t => t.KardexId);
            
            AddColumn("dbo.tbl_CellarProducts", "Location", c => c.String(nullable: false));
            AddColumn("dbo.tbl_InventoryDetails", "ProductId", c => c.Int(nullable: false));
            AddColumn("dbo.tbl_InventoryDetails", "KardexId", c => c.Int(nullable: false));
            AddColumn("dbo.tbl_UsersSud", "Status", c => c.Boolean(nullable: false));
            AlterColumn("dbo.tbl_Products", "Image", c => c.String());
            AlterColumn("dbo.tbl_Products", "DepartmentId", c => c.Int(nullable: false));
            AlterColumn("dbo.tbl_Products", "MeasureId", c => c.Int(nullable: false));
            AlterColumn("dbo.tbl_Inventories", "CellarId", c => c.Int(nullable: false));
            AlterColumn("dbo.tbl_InventoryDetails", "InventoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.tbl_Products", "DepartmentId");
            CreateIndex("dbo.tbl_Products", "MeasureId");
            CreateIndex("dbo.tbl_Inventories", "CellarId");
            CreateIndex("dbo.tbl_InventoryDetails", "InventoryId");
            CreateIndex("dbo.tbl_InventoryDetails", "ProductId");
            CreateIndex("dbo.tbl_InventoryDetails", "KardexId");
            AddForeignKey("dbo.tbl_InventoryDetails", "KardexId", "dbo.Kardexes", "KardexId", cascadeDelete: true);
            AddForeignKey("dbo.tbl_InventoryDetails", "ProductId", "dbo.tbl_Products", "ProductId", cascadeDelete: true);
            AddForeignKey("dbo.tbl_Products", "DepartmentId", "dbo.tbl_Departments", "DepartmentId", cascadeDelete: true);
            AddForeignKey("dbo.tbl_Products", "MeasureId", "dbo.tbl_Measures", "MeasureId", cascadeDelete: true);
            AddForeignKey("dbo.tbl_Inventories", "CellarId", "dbo.tbl_Cellars", "CellarId", cascadeDelete: true);
            AddForeignKey("dbo.tbl_InventoryDetails", "InventoryId", "dbo.tbl_Inventories", "InventoryId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_InventoryDetails", "InventoryId", "dbo.tbl_Inventories");
            DropForeignKey("dbo.tbl_Inventories", "CellarId", "dbo.tbl_Cellars");
            DropForeignKey("dbo.tbl_Products", "MeasureId", "dbo.tbl_Measures");
            DropForeignKey("dbo.tbl_Products", "DepartmentId", "dbo.tbl_Departments");
            DropForeignKey("dbo.PurchaseDetails", "PurchaseId", "dbo.Purchases");
            DropForeignKey("dbo.PurchaseDetails", "ProductId", "dbo.tbl_Products");
            DropForeignKey("dbo.PurchaseDetails", "KardexId", "dbo.Kardexes");
            DropForeignKey("dbo.Purchases", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.Suppliers", "DocumentTypeId", "dbo.tbl_DocumentsType");
            DropForeignKey("dbo.tbl_SaleDetails", "SaleId", "dbo.tbl_Sales");
            DropForeignKey("dbo.tbl_SaleDetails", "ProductId", "dbo.tbl_Products");
            DropForeignKey("dbo.tbl_ClientRefunds", "SaleId", "dbo.tbl_Sales");
            DropForeignKey("dbo.tbl_ClientRefundDetails", "ProductId", "dbo.tbl_Products");
            DropForeignKey("dbo.tbl_ClientRefundDetails", "ClientRefundId", "dbo.tbl_ClientRefunds");
            DropForeignKey("dbo.tbl_Sales", "ClientId", "dbo.tbl_Clients");
            DropForeignKey("dbo.tbl_Sales", "CellarId", "dbo.tbl_Cellars");
            DropForeignKey("dbo.tbl_Clients", "DocumentTypeId", "dbo.tbl_DocumentsType");
            DropForeignKey("dbo.Purchases", "CellarId", "dbo.tbl_Cellars");
            DropForeignKey("dbo.tbl_InventoryDetails", "ProductId", "dbo.tbl_Products");
            DropForeignKey("dbo.tbl_InventoryDetails", "KardexId", "dbo.Kardexes");
            DropForeignKey("dbo.EgressDetails", "ProductId", "dbo.tbl_Products");
            DropForeignKey("dbo.EgressDetails", "KardexId", "dbo.Kardexes");
            DropForeignKey("dbo.EgressDetails", "EgressId", "dbo.Egresses");
            DropForeignKey("dbo.Egresses", "ConceptId", "dbo.Concepts");
            DropForeignKey("dbo.Egresses", "CellarId", "dbo.tbl_Cellars");
            DropIndex("dbo.PurchaseDetails", new[] { "KardexId" });
            DropIndex("dbo.PurchaseDetails", new[] { "ProductId" });
            DropIndex("dbo.PurchaseDetails", new[] { "PurchaseId" });
            DropIndex("dbo.tbl_SaleDetails", new[] { "ProductId" });
            DropIndex("dbo.tbl_SaleDetails", new[] { "SaleId" });
            DropIndex("dbo.tbl_ClientRefundDetails", new[] { "ProductId" });
            DropIndex("dbo.tbl_ClientRefundDetails", new[] { "ClientRefundId" });
            DropIndex("dbo.tbl_ClientRefunds", new[] { "SaleId" });
            DropIndex("dbo.tbl_Sales", new[] { "CellarId" });
            DropIndex("dbo.tbl_Sales", new[] { "ClientId" });
            DropIndex("dbo.tbl_Clients", new[] { "DocumentTypeId" });
            DropIndex("dbo.Suppliers", new[] { "DocumentTypeId" });
            DropIndex("dbo.Purchases", new[] { "CellarId" });
            DropIndex("dbo.Purchases", new[] { "SupplierId" });
            DropIndex("dbo.Egresses", new[] { "CellarId" });
            DropIndex("dbo.Egresses", new[] { "ConceptId" });
            DropIndex("dbo.EgressDetails", new[] { "KardexId" });
            DropIndex("dbo.EgressDetails", new[] { "ProductId" });
            DropIndex("dbo.EgressDetails", new[] { "EgressId" });
            DropIndex("dbo.tbl_InventoryDetails", new[] { "KardexId" });
            DropIndex("dbo.tbl_InventoryDetails", new[] { "ProductId" });
            DropIndex("dbo.tbl_InventoryDetails", new[] { "InventoryId" });
            DropIndex("dbo.tbl_Inventories", new[] { "CellarId" });
            DropIndex("dbo.tbl_Products", new[] { "MeasureId" });
            DropIndex("dbo.tbl_Products", new[] { "DepartmentId" });
            AlterColumn("dbo.tbl_InventoryDetails", "InventoryId", c => c.Int());
            AlterColumn("dbo.tbl_Inventories", "CellarId", c => c.Int());
            AlterColumn("dbo.tbl_Products", "MeasureId", c => c.Int());
            AlterColumn("dbo.tbl_Products", "DepartmentId", c => c.Int());
            AlterColumn("dbo.tbl_Products", "Image", c => c.String(nullable: false));
            DropColumn("dbo.tbl_UsersSud", "Status");
            DropColumn("dbo.tbl_InventoryDetails", "KardexId");
            DropColumn("dbo.tbl_InventoryDetails", "ProductId");
            DropColumn("dbo.tbl_CellarProducts", "Location");
            DropTable("dbo.PurchaseDetails");
            DropTable("dbo.tbl_SaleDetails");
            DropTable("dbo.tbl_ClientRefundDetails");
            DropTable("dbo.tbl_ClientRefunds");
            DropTable("dbo.tbl_Sales");
            DropTable("dbo.tbl_Clients");
            DropTable("dbo.tbl_DocumentsType");
            DropTable("dbo.Suppliers");
            DropTable("dbo.Purchases");
            DropTable("dbo.Concepts");
            DropTable("dbo.Egresses");
            DropTable("dbo.EgressDetails");
            DropTable("dbo.Kardexes");
            RenameColumn(table: "dbo.tbl_InventoryDetails", name: "InventoryId", newName: "Inventory_InventoryId");
            RenameColumn(table: "dbo.tbl_Inventories", name: "CellarId", newName: "Cellar_CellarId");
            RenameColumn(table: "dbo.tbl_Products", name: "MeasureId", newName: "Measure_MeasureId");
            RenameColumn(table: "dbo.tbl_Products", name: "DepartmentId", newName: "Department_DepartmentId");
            CreateIndex("dbo.tbl_InventoryDetails", "Inventory_InventoryId");
            CreateIndex("dbo.tbl_Inventories", "Cellar_CellarId");
            CreateIndex("dbo.tbl_Products", "Measure_MeasureId");
            CreateIndex("dbo.tbl_Products", "Department_DepartmentId");
            AddForeignKey("dbo.tbl_InventoryDetails", "Inventory_InventoryId", "dbo.tbl_Inventories", "InventoryId");
            AddForeignKey("dbo.tbl_Inventories", "Cellar_CellarId", "dbo.tbl_Cellars", "CellarId");
            AddForeignKey("dbo.tbl_Products", "Measure_MeasureId", "dbo.tbl_Measures", "MeasureId");
            AddForeignKey("dbo.tbl_Products", "Department_DepartmentId", "dbo.tbl_Departments", "DepartmentId");
        }
    }
}
