namespace SUD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelacionesEntidadBodega : DbMigration
    {
        public override void Up()
        {
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
            
            CreateIndex("dbo.tbl_InventoryDetails", "KardexId");
            AddForeignKey("dbo.tbl_InventoryDetails", "KardexId", "dbo.Kardexes", "KardexId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PurchaseDetails", "PurchaseId", "dbo.Purchases");
            DropForeignKey("dbo.PurchaseDetails", "ProductId", "dbo.tbl_Products");
            DropForeignKey("dbo.PurchaseDetails", "KardexId", "dbo.Kardexes");
            DropForeignKey("dbo.tbl_InventoryDetails", "KardexId", "dbo.Kardexes");
            DropForeignKey("dbo.EgressDetails", "ProductId", "dbo.tbl_Products");
            DropForeignKey("dbo.EgressDetails", "KardexId", "dbo.Kardexes");
            DropForeignKey("dbo.EgressDetails", "EgressId", "dbo.Egresses");
            DropForeignKey("dbo.Egresses", "ConceptId", "dbo.Concepts");
            DropForeignKey("dbo.Egresses", "CellarId", "dbo.tbl_Cellars");
            DropIndex("dbo.PurchaseDetails", new[] { "KardexId" });
            DropIndex("dbo.PurchaseDetails", new[] { "ProductId" });
            DropIndex("dbo.PurchaseDetails", new[] { "PurchaseId" });
            DropIndex("dbo.Egresses", new[] { "CellarId" });
            DropIndex("dbo.Egresses", new[] { "ConceptId" });
            DropIndex("dbo.EgressDetails", new[] { "KardexId" });
            DropIndex("dbo.EgressDetails", new[] { "ProductId" });
            DropIndex("dbo.EgressDetails", new[] { "EgressId" });
            DropIndex("dbo.tbl_InventoryDetails", new[] { "KardexId" });
            DropTable("dbo.PurchaseDetails");
            DropTable("dbo.Concepts");
            DropTable("dbo.Egresses");
            DropTable("dbo.EgressDetails");
            DropTable("dbo.Kardexes");
        }
    }
}
