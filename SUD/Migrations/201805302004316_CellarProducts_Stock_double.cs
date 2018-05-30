namespace SUD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CellarProducts_Stock_double : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tbl_CellarProducts", "Stock", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tbl_CellarProducts", "Stock", c => c.Int(nullable: false));
        }
    }
}
