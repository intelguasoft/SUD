namespace SUD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrecionRelacionDocumentType : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tbl_DocumentsType", "DocumentType_DocumentTypeId", "dbo.tbl_DocumentsType");
            DropIndex("dbo.tbl_DocumentsType", new[] { "DocumentType_DocumentTypeId" });
            DropColumn("dbo.tbl_DocumentsType", "DocumentType_DocumentTypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbl_DocumentsType", "DocumentType_DocumentTypeId", c => c.Int());
            CreateIndex("dbo.tbl_DocumentsType", "DocumentType_DocumentTypeId");
            AddForeignKey("dbo.tbl_DocumentsType", "DocumentType_DocumentTypeId", "dbo.tbl_DocumentsType", "DocumentTypeId");
        }
    }
}
