namespace SUD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Purchases", "StateId", "dbo.States");
            DropIndex("dbo.Purchases", new[] { "StateId" });
            RenameColumn(table: "dbo.Purchases", name: "StateId", newName: "States_StateId");
            CreateTable(
                "dbo.SaleDetailBks",
                c => new
                    {
                        SaleDetailBkId = c.Int(nullable: false, identity: true),
                        User = c.String(),
                        Description = c.String(nullable: false, maxLength: 140),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Double(nullable: false),
                        IVAPercentage = c.Single(nullable: false),
                        DiscountRate = c.Single(nullable: false),
                        ProductId = c.Int(nullable: false),
                        KardexId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SaleDetailBkId)
                .ForeignKey("dbo.tbl_Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            AlterColumn("dbo.tbl_SaleDetails", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.tbl_SaleDetails", "Quantity", c => c.Double(nullable: false));
            AlterColumn("dbo.PurchaseDetails", "Quantity", c => c.Double(nullable: false));
            AlterColumn("dbo.Purchases", "States_StateId", c => c.Short());
            CreateIndex("dbo.Purchases", "States_StateId");
            AddForeignKey("dbo.Purchases", "States_StateId", "dbo.States", "StateId");
            DropColumn("dbo.tbl_SaleDetails", "KardexId");
            DropColumn("dbo.PurchaseDetails", "KardexId");
            DropColumn("dbo.PurchaseDetails", "VATRate");
            DropColumn("dbo.PurchaseDetails", "DiscountRate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PurchaseDetails", "DiscountRate", c => c.Single(nullable: false));
            AddColumn("dbo.PurchaseDetails", "VATRate", c => c.Single(nullable: false));
            AddColumn("dbo.PurchaseDetails", "KardexId", c => c.Long(nullable: false));
            AddColumn("dbo.tbl_SaleDetails", "KardexId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Purchases", "States_StateId", "dbo.States");
            DropForeignKey("dbo.SaleDetailBks", "ProductId", "dbo.tbl_Products");
            DropIndex("dbo.SaleDetailBks", new[] { "ProductId" });
            DropIndex("dbo.Purchases", new[] { "States_StateId" });
            AlterColumn("dbo.Purchases", "States_StateId", c => c.Short(nullable: false));
            AlterColumn("dbo.PurchaseDetails", "Quantity", c => c.Single(nullable: false));
            AlterColumn("dbo.tbl_SaleDetails", "Quantity", c => c.Int(nullable: false));
            AlterColumn("dbo.tbl_SaleDetails", "Price", c => c.Single(nullable: false));
            DropTable("dbo.SaleDetailBks");
            RenameColumn(table: "dbo.Purchases", name: "States_StateId", newName: "StateId");
            CreateIndex("dbo.Purchases", "StateId");
            AddForeignKey("dbo.Purchases", "StateId", "dbo.States", "StateId", cascadeDelete: true);
        }
    }
}
