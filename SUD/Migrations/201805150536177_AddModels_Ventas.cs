namespace SUD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModels_Ventas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_AccountingDocuments",
                c => new
                    {
                        AccountingDocumentId = c.Int(nullable: false, identity: true),
                        Document = c.String(nullable: false),
                        Description = c.String(),
                        SerialNumber = c.String(nullable: false),
                        InitialNumber = c.Int(nullable: false),
                        FinalNumber = c.Int(nullable: false),
                        ResolutionNumber = c.String(nullable: false),
                        ExpirationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AccountingDocumentId);
            
            CreateTable(
                "dbo.tbl_PaymentMethods",
                c => new
                    {
                        PaymentMethodId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        Details = c.String(),
                    })
                .PrimaryKey(t => t.PaymentMethodId);
            
            AddColumn("dbo.tbl_Sales", "AccountingDocumentId", c => c.Int(nullable: false));
            AddColumn("dbo.tbl_Sales", "DocumentNumber", c => c.Int(nullable: false));
            AddColumn("dbo.tbl_Sales", "PaymentMethodId", c => c.Int(nullable: false));
            CreateIndex("dbo.tbl_Sales", "AccountingDocumentId");
            CreateIndex("dbo.tbl_Sales", "PaymentMethodId");
            AddForeignKey("dbo.tbl_Sales", "AccountingDocumentId", "dbo.tbl_AccountingDocuments", "AccountingDocumentId", cascadeDelete: true);
            AddForeignKey("dbo.tbl_Sales", "PaymentMethodId", "dbo.tbl_PaymentMethods", "PaymentMethodId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_Sales", "PaymentMethodId", "dbo.tbl_PaymentMethods");
            DropForeignKey("dbo.tbl_Sales", "AccountingDocumentId", "dbo.tbl_AccountingDocuments");
            DropIndex("dbo.tbl_Sales", new[] { "PaymentMethodId" });
            DropIndex("dbo.tbl_Sales", new[] { "AccountingDocumentId" });
            DropColumn("dbo.tbl_Sales", "PaymentMethodId");
            DropColumn("dbo.tbl_Sales", "DocumentNumber");
            DropColumn("dbo.tbl_Sales", "AccountingDocumentId");
            DropTable("dbo.tbl_PaymentMethods");
            DropTable("dbo.tbl_AccountingDocuments");
        }
    }
}
