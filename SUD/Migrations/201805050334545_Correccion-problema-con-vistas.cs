namespace SUD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Correccionproblemaconvistas : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Purchases", "CellarId", "dbo.tbl_Cellars");
            DropForeignKey("dbo.Suppliers", "DocumentTypeId", "dbo.tbl_DocumentsType");
            DropForeignKey("dbo.Purchases", "SupplierId", "dbo.Suppliers");
            DropIndex("dbo.Purchases", new[] { "SupplierId" });
            DropIndex("dbo.Purchases", new[] { "CellarId" });
            DropIndex("dbo.Suppliers", new[] { "DocumentTypeId" });
            DropTable("dbo.Purchases");
            DropTable("dbo.Suppliers");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.SupplierId);
            
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        PurchaseId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        SupplierId = c.Int(nullable: false),
                        CellarId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PurchaseId);
            
            CreateIndex("dbo.Suppliers", "DocumentTypeId");
            CreateIndex("dbo.Purchases", "CellarId");
            CreateIndex("dbo.Purchases", "SupplierId");
            AddForeignKey("dbo.Purchases", "SupplierId", "dbo.Suppliers", "SupplierId", cascadeDelete: true);
            AddForeignKey("dbo.Suppliers", "DocumentTypeId", "dbo.tbl_DocumentsType", "DocumentTypeId", cascadeDelete: true);
            AddForeignKey("dbo.Purchases", "CellarId", "dbo.tbl_Cellars", "CellarId", cascadeDelete: true);
        }
    }
}
