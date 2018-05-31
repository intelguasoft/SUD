namespace SUD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MaestroDetalle_Ventas1 : DbMigration
    {
        public override void Up()
        {
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
            CreateIndex("dbo.tbl_SaleDetails", "KardexId");
            AddForeignKey("dbo.tbl_SaleDetails", "KardexId", "dbo.Kardexes", "KardexId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SaleDetailBks", "ProductId", "dbo.tbl_Products");
            DropForeignKey("dbo.tbl_SaleDetails", "KardexId", "dbo.Kardexes");
            DropIndex("dbo.SaleDetailBks", new[] { "ProductId" });
            DropIndex("dbo.tbl_SaleDetails", new[] { "KardexId" });
            AlterColumn("dbo.tbl_SaleDetails", "Quantity", c => c.Int(nullable: false));
            AlterColumn("dbo.tbl_SaleDetails", "Price", c => c.Single(nullable: false));
            DropTable("dbo.SaleDetailBks");
        }
    }
}
