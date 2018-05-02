namespace SUD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_BarCodes",
                c => new
                    {
                        BarCodeId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Bar = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BarCodeId)
                .ForeignKey("dbo.tbl_Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId, unique: true, name: "Barra_Producto")
                .Index(t => t.Bar, unique: true, name: "Barra_Codigo");
            
            CreateTable(
                "dbo.tbl_Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Note = c.String(nullable: false),
                        Image = c.String(nullable: false),
                        Medida = c.String(nullable: false),
                        Department_DepartmentId = c.Int(),
                        Measure_MeasureId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.tbl_Departments", t => t.Department_DepartmentId)
                .ForeignKey("dbo.tbl_Measures", t => t.Measure_MeasureId)
                .Index(t => t.Department_DepartmentId)
                .Index(t => t.Measure_MeasureId);
            
            CreateTable(
                "dbo.tbl_CellarProducts",
                c => new
                    {
                        CellarProductId = c.Int(nullable: false, identity: true),
                        CellarId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Stock = c.Int(nullable: false),
                        Minimum = c.Int(nullable: false),
                        Maximum = c.Int(nullable: false),
                        ReplacementDays = c.Int(nullable: false),
                        MinimumAmount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CellarProductId)
                .ForeignKey("dbo.tbl_Cellars", t => t.CellarId, cascadeDelete: true)
                .ForeignKey("dbo.tbl_Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.CellarId, unique: true, name: "CellarProduct_Bodega")
                .Index(t => t.ProductId, unique: true, name: "CellarProducto_Producto");
            
            CreateTable(
                "dbo.tbl_Cellars",
                c => new
                    {
                        CellarId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CellarId);
            
            CreateTable(
                "dbo.tbl_Inventories",
                c => new
                    {
                        InventoryId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Step = c.Int(nullable: false),
                        Cellar_CellarId = c.Int(),
                    })
                .PrimaryKey(t => t.InventoryId)
                .ForeignKey("dbo.tbl_Cellars", t => t.Cellar_CellarId)
                .Index(t => t.Cellar_CellarId);
            
            CreateTable(
                "dbo.tbl_InventoryDetails",
                c => new
                    {
                        LineId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        Stock = c.Single(nullable: false),
                        Count1 = c.Single(nullable: false),
                        Count2 = c.Single(nullable: false),
                        Count3 = c.Single(nullable: false),
                        Adjustment = c.Single(nullable: false),
                        Inventory_InventoryId = c.Int(),
                    })
                .PrimaryKey(t => t.LineId)
                .ForeignKey("dbo.tbl_Inventories", t => t.Inventory_InventoryId)
                .Index(t => t.Inventory_InventoryId);
            
            CreateTable(
                "dbo.tbl_Departments",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.DepartmentId);
            
            CreateTable(
                "dbo.tbl_Measures",
                c => new
                    {
                        MeasureId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.MeasureId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.tbl_RolsPermission",
                c => new
                    {
                        PermissionId = c.Int(nullable: false, identity: true),
                        RolId = c.Int(nullable: false),
                        Form = c.String(nullable: false),
                        CanSee = c.Boolean(nullable: false),
                        CanModify = c.Boolean(nullable: false),
                        CanErase = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PermissionId)
                .ForeignKey("dbo.tbl_Rols", t => t.RolId, cascadeDelete: true)
                .Index(t => t.RolId);
            
            CreateTable(
                "dbo.tbl_Rols",
                c => new
                    {
                        RolId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.RolId);
            
            CreateTable(
                "dbo.tbl_UsersSud",
                c => new
                    {
                        UserSudId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 255),
                        ModificationDatePassword = c.DateTime(nullable: false),
                        RolId = c.Int(nullable: false),
                        Email = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.UserSudId)
                .ForeignKey("dbo.tbl_Rols", t => t.RolId, cascadeDelete: true)
                .Index(t => t.RolId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.tbl_UsersSud", "RolId", "dbo.tbl_Rols");
            DropForeignKey("dbo.tbl_RolsPermission", "RolId", "dbo.tbl_Rols");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.tbl_Products", "Measure_MeasureId", "dbo.tbl_Measures");
            DropForeignKey("dbo.tbl_Products", "Department_DepartmentId", "dbo.tbl_Departments");
            DropForeignKey("dbo.tbl_CellarProducts", "ProductId", "dbo.tbl_Products");
            DropForeignKey("dbo.tbl_InventoryDetails", "Inventory_InventoryId", "dbo.tbl_Inventories");
            DropForeignKey("dbo.tbl_Inventories", "Cellar_CellarId", "dbo.tbl_Cellars");
            DropForeignKey("dbo.tbl_CellarProducts", "CellarId", "dbo.tbl_Cellars");
            DropForeignKey("dbo.tbl_BarCodes", "ProductId", "dbo.tbl_Products");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.tbl_UsersSud", new[] { "RolId" });
            DropIndex("dbo.tbl_RolsPermission", new[] { "RolId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.tbl_InventoryDetails", new[] { "Inventory_InventoryId" });
            DropIndex("dbo.tbl_Inventories", new[] { "Cellar_CellarId" });
            DropIndex("dbo.tbl_CellarProducts", "CellarProducto_Producto");
            DropIndex("dbo.tbl_CellarProducts", "CellarProduct_Bodega");
            DropIndex("dbo.tbl_Products", new[] { "Measure_MeasureId" });
            DropIndex("dbo.tbl_Products", new[] { "Department_DepartmentId" });
            DropIndex("dbo.tbl_BarCodes", "Barra_Codigo");
            DropIndex("dbo.tbl_BarCodes", "Barra_Producto");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.tbl_UsersSud");
            DropTable("dbo.tbl_Rols");
            DropTable("dbo.tbl_RolsPermission");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.tbl_Measures");
            DropTable("dbo.tbl_Departments");
            DropTable("dbo.tbl_InventoryDetails");
            DropTable("dbo.tbl_Inventories");
            DropTable("dbo.tbl_Cellars");
            DropTable("dbo.tbl_CellarProducts");
            DropTable("dbo.tbl_Products");
            DropTable("dbo.tbl_BarCodes");
        }
    }
}
