namespace SUD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NoSeQueRallosEstaPasando : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Purchases", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.Suppliers", "DocumentTypeId", "dbo.tbl_DocumentsType");
            DropForeignKey("dbo.Purchases", "CellarId", "dbo.tbl_Cellars");
            DropIndex("dbo.Suppliers", new[] { "DocumentTypeId" });
            DropIndex("dbo.Purchases", new[] { "CellarId" });
            DropIndex("dbo.Purchases", new[] { "SupplierId" });
            DropTable("dbo.Suppliers");
            DropTable("dbo.Purchases");
        }
    }
}
