namespace SUD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cambioNombreTablaTblClients : DbMigration
    {
        public override void Up()
        {
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
                .PrimaryKey(t => t.ClientRefundDetailId);
            
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
                "dbo.tbl_DocumentsType",
                c => new
                    {
                        DocumentTypeId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 50),
                        DocumentType_DocumentTypeId = c.Int(),
                    })
                .PrimaryKey(t => t.DocumentTypeId)
                .ForeignKey("dbo.tbl_DocumentsType", t => t.DocumentType_DocumentTypeId)
                .Index(t => t.DocumentType_DocumentTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_ClientRefunds", "SaleId", "dbo.tbl_Sales");
            DropForeignKey("dbo.tbl_Sales", "ClientId", "dbo.tbl_Clients");
            DropForeignKey("dbo.tbl_Clients", "DocumentTypeId", "dbo.tbl_DocumentsType");
            DropForeignKey("dbo.tbl_DocumentsType", "DocumentType_DocumentTypeId", "dbo.tbl_DocumentsType");
            DropForeignKey("dbo.tbl_Sales", "CellarId", "dbo.tbl_Cellars");
            DropIndex("dbo.tbl_DocumentsType", new[] { "DocumentType_DocumentTypeId" });
            DropIndex("dbo.tbl_Clients", new[] { "DocumentTypeId" });
            DropIndex("dbo.tbl_Sales", new[] { "CellarId" });
            DropIndex("dbo.tbl_Sales", new[] { "ClientId" });
            DropIndex("dbo.tbl_ClientRefunds", new[] { "SaleId" });
            DropTable("dbo.tbl_DocumentsType");
            DropTable("dbo.tbl_Clients");
            DropTable("dbo.tbl_Sales");
            DropTable("dbo.tbl_ClientRefunds");
            DropTable("dbo.tbl_ClientRefundDetails");
        }
    }
}
