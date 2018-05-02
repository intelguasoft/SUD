namespace SUD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modificacion_Relaciones_Inventario : DbMigration
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
            AddColumn("dbo.tbl_InventoryDetails", "ProductId", c => c.Int(nullable: false));
            AddColumn("dbo.tbl_InventoryDetails", "KardexId", c => c.Int(nullable: false));
            AlterColumn("dbo.tbl_Products", "DepartmentId", c => c.Int(nullable: false));
            AlterColumn("dbo.tbl_Products", "MeasureId", c => c.Int(nullable: false));
            AlterColumn("dbo.tbl_Inventories", "CellarId", c => c.Int(nullable: false));
            AlterColumn("dbo.tbl_InventoryDetails", "InventoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.tbl_Products", "DepartmentId");
            CreateIndex("dbo.tbl_Products", "MeasureId");
            CreateIndex("dbo.tbl_Inventories", "CellarId");
            CreateIndex("dbo.tbl_InventoryDetails", "InventoryId");
            CreateIndex("dbo.tbl_InventoryDetails", "ProductId");
            AddForeignKey("dbo.tbl_InventoryDetails", "ProductId", "dbo.tbl_Products", "ProductId", cascadeDelete: true);
            AddForeignKey("dbo.tbl_Products", "DepartmentId", "dbo.tbl_Departments", "DepartmentId", cascadeDelete: true);
            AddForeignKey("dbo.tbl_Products", "MeasureId", "dbo.tbl_Measures", "MeasureId", cascadeDelete: true);
            AddForeignKey("dbo.tbl_Inventories", "CellarId", "dbo.tbl_Cellars", "CellarId", cascadeDelete: true);
            AddForeignKey("dbo.tbl_InventoryDetails", "InventoryId", "dbo.tbl_Inventories", "InventoryId", cascadeDelete: true);
            DropTable("dbo.tbl_ClientRefundDetails");
        }
        
        public override void Down()
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
            
            DropForeignKey("dbo.tbl_InventoryDetails", "InventoryId", "dbo.tbl_Inventories");
            DropForeignKey("dbo.tbl_Inventories", "CellarId", "dbo.tbl_Cellars");
            DropForeignKey("dbo.tbl_Products", "MeasureId", "dbo.tbl_Measures");
            DropForeignKey("dbo.tbl_Products", "DepartmentId", "dbo.tbl_Departments");
            DropForeignKey("dbo.tbl_InventoryDetails", "ProductId", "dbo.tbl_Products");
            DropIndex("dbo.tbl_InventoryDetails", new[] { "ProductId" });
            DropIndex("dbo.tbl_InventoryDetails", new[] { "InventoryId" });
            DropIndex("dbo.tbl_Inventories", new[] { "CellarId" });
            DropIndex("dbo.tbl_Products", new[] { "MeasureId" });
            DropIndex("dbo.tbl_Products", new[] { "DepartmentId" });
            AlterColumn("dbo.tbl_InventoryDetails", "InventoryId", c => c.Int());
            AlterColumn("dbo.tbl_Inventories", "CellarId", c => c.Int());
            AlterColumn("dbo.tbl_Products", "MeasureId", c => c.Int());
            AlterColumn("dbo.tbl_Products", "DepartmentId", c => c.Int());
            DropColumn("dbo.tbl_InventoryDetails", "KardexId");
            DropColumn("dbo.tbl_InventoryDetails", "ProductId");
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
