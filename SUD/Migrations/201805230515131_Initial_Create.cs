namespace SUD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_Create : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.tbl_AccountingDocuments",
            //    c => new
            //        {
            //            AccountingDocumentId = c.Int(nullable: false, identity: true),
            //            Document = c.String(nullable: false),
            //            Description = c.String(),
            //            SerialNumber = c.String(nullable: false),
            //            InitialNumber = c.Int(nullable: false),
            //            FinalNumber = c.Int(nullable: false),
            //            ResolutionNumber = c.String(nullable: false),
            //            ExpirationDate = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.AccountingDocumentId);
            
            CreateTable(
                "dbo.tbl_Routes",
                c => new
                    {
                        RouteId = c.Int(nullable: false, identity: true),
                        RouteNumber = c.Int(nullable: false),
                        AccountingDocumentId = c.Int(nullable: false),
                        SellerId = c.Int(nullable: false),
                        Territory = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.RouteId)
                .ForeignKey("dbo.tbl_AccountingDocuments", t => t.AccountingDocumentId, cascadeDelete: true)
                .ForeignKey("dbo.tbl_Sellers", t => t.SellerId, cascadeDelete: true)
                .Index(t => t.AccountingDocumentId)
                .Index(t => t.SellerId);
            
            CreateTable(
                "dbo.tbl_Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false),
                        OrderNumber = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        ClientId = c.Int(nullable: false),
                        CellarId = c.Int(nullable: false),
                        RouteId = c.Int(nullable: false),
                        StateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.tbl_Cellars", t => t.CellarId, cascadeDelete: true)
                .ForeignKey("dbo.tbl_Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.tbl_Routes", t => t.RouteId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .Index(t => t.CellarId)
                .Index(t => t.RouteId);
            
            //CreateTable(
            //    "dbo.tbl_Cellars",
            //    c => new
            //        {
            //            CellarId = c.Int(nullable: false, identity: true),
            //            Description = c.String(nullable: false),
            //        })
            //    .PrimaryKey(t => t.CellarId);
            
            //CreateTable(
            //    "dbo.tbl_CellarProducts",
            //    c => new
            //        {
            //            CellarProductId = c.Int(nullable: false, identity: true),
            //            CellarId = c.Int(nullable: false),
            //            ProductId = c.Int(nullable: false),
            //            Stock = c.Int(nullable: false),
            //            Minimum = c.Int(nullable: false),
            //            Maximum = c.Int(nullable: false),
            //            ReplacementDays = c.Int(nullable: false),
            //            MinimumAmount = c.Int(nullable: false),
            //            Location = c.String(nullable: false),
            //        })
            //    .PrimaryKey(t => t.CellarProductId)
            //    .ForeignKey("dbo.tbl_Cellars", t => t.CellarId, cascadeDelete: true)
            //    .ForeignKey("dbo.tbl_Products", t => t.ProductId, cascadeDelete: true)
            //    .Index(t => t.CellarId, unique: true, name: "CellarProduct_Bodega")
            //    .Index(t => t.ProductId, unique: true, name: "CellarProducto_Producto");
            
            //CreateTable(
            //    "dbo.tbl_Products",
            //    c => new
            //        {
            //            ProductId = c.Int(nullable: false, identity: true),
            //            DepartmentId = c.Int(nullable: false),
            //            MeasureId = c.Int(nullable: false),
            //            Description = c.String(nullable: false),
            //            Price = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            Note = c.String(nullable: false),
            //            Image = c.String(),
            //            Medida = c.String(nullable: false),
            //        })
            //    .PrimaryKey(t => t.ProductId)
            //    .ForeignKey("dbo.tbl_Departments", t => t.DepartmentId, cascadeDelete: true)
            //    .ForeignKey("dbo.tbl_Measures", t => t.MeasureId, cascadeDelete: true)
            //    .Index(t => t.DepartmentId)
            //    .Index(t => t.MeasureId);
            
            //CreateTable(
            //    "dbo.tbl_BarCodes",
            //    c => new
            //        {
            //            BarCodeId = c.Int(nullable: false, identity: true),
            //            ProductId = c.Int(nullable: false),
            //            Bar = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.BarCodeId)
            //    .ForeignKey("dbo.tbl_Products", t => t.ProductId, cascadeDelete: true)
            //    .Index(t => t.ProductId, unique: true, name: "Barra_Producto")
            //    .Index(t => t.Bar, unique: true, name: "Barra_Codigo");
            
            //CreateTable(
            //    "dbo.tbl_ClientRefundDetails",
            //    c => new
            //        {
            //            ClientRefundDetailId = c.Int(nullable: false, identity: true),
            //            Description = c.String(nullable: false, maxLength: 140),
            //            price = c.Single(nullable: false),
            //            Cantidad = c.Int(nullable: false),
            //            DiscountRate = c.Single(nullable: false),
            //            ClientRefundId = c.Int(nullable: false),
            //            ProductId = c.Int(nullable: false),
            //            KardexId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.ClientRefundDetailId)
            //    .ForeignKey("dbo.tbl_ClientRefunds", t => t.ClientRefundId, cascadeDelete: true)
            //    .ForeignKey("dbo.tbl_Products", t => t.ProductId, cascadeDelete: true)
            //    .Index(t => t.ClientRefundId)
            //    .Index(t => t.ProductId);
            
            //CreateTable(
            //    "dbo.tbl_ClientRefunds",
            //    c => new
            //        {
            //            ClientRefundId = c.Int(nullable: false, identity: true),
            //            Date = c.DateTime(nullable: false),
            //            SaleId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.ClientRefundId)
            //    .ForeignKey("dbo.tbl_Sales", t => t.SaleId, cascadeDelete: true)
            //    .Index(t => t.SaleId);
            
            //CreateTable(
            //    "dbo.tbl_Sales",
            //    c => new
            //        {
            //            SaleId = c.Int(nullable: false, identity: true),
            //            Datetime = c.DateTime(nullable: false),
            //            ClientId = c.Int(nullable: false),
            //            CellarId = c.Int(nullable: false),
            //            AccountingDocumentId = c.Int(nullable: false),
            //            DocumentNumber = c.Int(nullable: false),
            //            PaymentMethodId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.SaleId)
            //    .ForeignKey("dbo.tbl_AccountingDocuments", t => t.AccountingDocumentId, cascadeDelete: true)
            //    .ForeignKey("dbo.tbl_Cellars", t => t.CellarId, cascadeDelete: true)
            //    .ForeignKey("dbo.tbl_Clients", t => t.ClientId, cascadeDelete: true)
            //    .ForeignKey("dbo.tbl_PaymentMethods", t => t.PaymentMethodId, cascadeDelete: true)
            //    .Index(t => t.ClientId)
            //    .Index(t => t.CellarId)
            //    .Index(t => t.AccountingDocumentId)
            //    .Index(t => t.PaymentMethodId);
            
            //CreateTable(
            //    "dbo.tbl_Clients",
            //    c => new
            //        {
            //            ClientId = c.Int(nullable: false, identity: true),
            //            Document = c.String(nullable: false, maxLength: 13),
            //            ComertialName = c.String(),
            //            FirstNameContact = c.String(nullable: false),
            //            LastNameContact = c.String(nullable: false),
            //            Address = c.String(nullable: false),
            //            Telephone1 = c.Int(nullable: false),
            //            Telephone2 = c.Int(nullable: false),
            //            Mail = c.String(),
            //            Note = c.String(),
            //            DocumentTypeId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.ClientId)
            //    .ForeignKey("dbo.tbl_DocumentsType", t => t.DocumentTypeId, cascadeDelete: true)
            //    .Index(t => t.DocumentTypeId);
            
            //CreateTable(
            //    "dbo.tbl_DocumentsType",
            //    c => new
            //        {
            //            DocumentTypeId = c.Int(nullable: false, identity: true),
            //            Description = c.String(nullable: false, maxLength: 50),
            //        })
            //    .PrimaryKey(t => t.DocumentTypeId);
            
            //CreateTable(
            //    "dbo.tbl_PaymentMethods",
            //    c => new
            //        {
            //            PaymentMethodId = c.Int(nullable: false, identity: true),
            //            Description = c.String(nullable: false),
            //            Details = c.String(),
            //        })
            //    .PrimaryKey(t => t.PaymentMethodId);
            
            //CreateTable(
            //    "dbo.tbl_SaleDetails",
            //    c => new
            //        {
            //            SaleDetailId = c.Int(nullable: false, identity: true),
            //            Description = c.String(nullable: false, maxLength: 140),
            //            Price = c.Single(nullable: false),
            //            Quantity = c.Int(nullable: false),
            //            IVAPercentage = c.Single(nullable: false),
            //            DiscountRate = c.Single(nullable: false),
            //            SaleId = c.Int(nullable: false),
            //            ProductId = c.Int(nullable: false),
            //            KardexId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.SaleDetailId)
            //    .ForeignKey("dbo.tbl_Products", t => t.ProductId, cascadeDelete: true)
            //    .ForeignKey("dbo.tbl_Sales", t => t.SaleId, cascadeDelete: true)
            //    .Index(t => t.SaleId)
            //    .Index(t => t.ProductId);
            
            //CreateTable(
            //    "dbo.tbl_Departments",
            //    c => new
            //        {
            //            DepartmentId = c.Int(nullable: false, identity: true),
            //            Description = c.String(nullable: false),
            //        })
            //    .PrimaryKey(t => t.DepartmentId);
            
            //CreateTable(
            //    "dbo.tbl_Measures",
            //    c => new
            //        {
            //            MeasureId = c.Int(nullable: false, identity: true),
            //            Description = c.String(nullable: false),
            //        })
            //    .PrimaryKey(t => t.MeasureId);
            
            CreateTable(
                "dbo.tbl_OrderDetails",
                c => new
                    {
                        OrderDetailId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Price = c.Single(nullable: false),
                        Quantity = c.Int(nullable: false),
                        IVAPercentage = c.Single(nullable: false),
                        DiscountRate = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.OrderDetailId)
                .ForeignKey("dbo.tbl_Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.tbl_Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            //CreateTable(
            //    "dbo.PurchaseDetails",
            //    c => new
            //        {
            //            PurchaseDetailsId = c.Int(nullable: false, identity: true),
            //            PurchaseId = c.Int(nullable: false),
            //            ProductId = c.Int(nullable: false),
            //            Description = c.String(),
            //            Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            Quantity = c.Single(nullable: false),
            //            KardexId = c.Int(nullable: false),
            //            VATRate = c.Single(nullable: false),
            //            DiscountRate = c.Single(nullable: false),
            //            ManufacturingLot = c.String(),
            //            DueDate = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.PurchaseDetailsId)
            //    .ForeignKey("dbo.Kardexes", t => t.KardexId, cascadeDelete: true)
            //    .ForeignKey("dbo.tbl_Products", t => t.ProductId, cascadeDelete: true)
            //    .ForeignKey("dbo.Purchases", t => t.PurchaseId, cascadeDelete: true)
            //    .Index(t => t.PurchaseId)
            //    .Index(t => t.ProductId)
            //    .Index(t => t.KardexId);
            
            //CreateTable(
            //    "dbo.Kardexes",
            //    c => new
            //        {
            //            KardexId = c.Int(nullable: false, identity: true),
            //            CellarId = c.Int(nullable: false),
            //            ProductId = c.Int(nullable: false),
            //            Date = c.DateTime(nullable: false),
            //            Document = c.String(),
            //            Entry = c.Single(nullable: false),
            //            Egress = c.Single(nullable: false),
            //            Balance = c.Single(nullable: false),
            //            LastCost = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            AverageCost = c.Decimal(nullable: false, precision: 18, scale: 2),
            //        })
            //    .PrimaryKey(t => t.KardexId);
            
            //CreateTable(
            //    "dbo.EgressDetails",
            //    c => new
            //        {
            //            EgressDetailsId = c.Int(nullable: false, identity: true),
            //            EgressId = c.Int(nullable: false),
            //            ProductId = c.Int(nullable: false),
            //            Description = c.String(),
            //            Quantity = c.Single(nullable: false),
            //            KardexId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.EgressDetailsId)
            //    .ForeignKey("dbo.Egresses", t => t.EgressId, cascadeDelete: true)
            //    .ForeignKey("dbo.Kardexes", t => t.KardexId, cascadeDelete: true)
            //    .ForeignKey("dbo.tbl_Products", t => t.ProductId, cascadeDelete: true)
            //    .Index(t => t.EgressId)
            //    .Index(t => t.ProductId)
            //    .Index(t => t.KardexId);
            
            //CreateTable(
            //    "dbo.Egresses",
            //    c => new
            //        {
            //            EgressId = c.Int(nullable: false, identity: true),
            //            Date = c.DateTime(nullable: false),
            //            ConceptId = c.Int(nullable: false),
            //            CellarId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.EgressId)
            //    .ForeignKey("dbo.tbl_Cellars", t => t.CellarId, cascadeDelete: true)
            //    .ForeignKey("dbo.Concepts", t => t.ConceptId, cascadeDelete: true)
            //    .Index(t => t.ConceptId)
            //    .Index(t => t.CellarId);
            
            //CreateTable(
            //    "dbo.Concepts",
            //    c => new
            //        {
            //            ConceptId = c.Int(nullable: false, identity: true),
            //            Description = c.String(),
            //        })
            //    .PrimaryKey(t => t.ConceptId);
            
            //CreateTable(
            //    "dbo.tbl_InventoryDetails",
            //    c => new
            //        {
            //            LineId = c.Int(nullable: false, identity: true),
            //            InventoryId = c.Int(nullable: false),
            //            ProductId = c.Int(nullable: false),
            //            Description = c.String(nullable: false),
            //            Stock = c.Single(nullable: false),
            //            Count1 = c.Single(nullable: false),
            //            Count2 = c.Single(nullable: false),
            //            Count3 = c.Single(nullable: false),
            //            Adjustment = c.Single(nullable: false),
            //            KardexId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.LineId)
            //    .ForeignKey("dbo.tbl_Inventories", t => t.InventoryId, cascadeDelete: true)
            //    .ForeignKey("dbo.Kardexes", t => t.KardexId, cascadeDelete: true)
            //    .ForeignKey("dbo.tbl_Products", t => t.ProductId, cascadeDelete: true)
            //    .Index(t => t.InventoryId)
            //    .Index(t => t.ProductId)
            //    .Index(t => t.KardexId);
            
            //CreateTable(
            //    "dbo.tbl_Inventories",
            //    c => new
            //        {
            //            InventoryId = c.Int(nullable: false, identity: true),
            //            CellarId = c.Int(nullable: false),
            //            Date = c.DateTime(nullable: false),
            //            Step = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.InventoryId)
            //    .ForeignKey("dbo.tbl_Cellars", t => t.CellarId, cascadeDelete: true)
            //    .Index(t => t.CellarId);
            
            //CreateTable(
            //    "dbo.Purchases",
            //    c => new
            //        {
            //            PurchaseId = c.Int(nullable: false, identity: true),
            //            Date = c.DateTime(nullable: false),
            //            SupplierId = c.Int(nullable: false),
            //            CellarId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.PurchaseId)
            //    .ForeignKey("dbo.tbl_Cellars", t => t.CellarId, cascadeDelete: true)
            //    .ForeignKey("dbo.Suppliers", t => t.SupplierId, cascadeDelete: true)
            //    .Index(t => t.SupplierId)
            //    .Index(t => t.CellarId);
            
            //CreateTable(
            //    "dbo.Suppliers",
            //    c => new
            //        {
            //            SupplierId = c.Int(nullable: false, identity: true),
            //            Tradename = c.String(),
            //            DocumentTypeId = c.Int(nullable: false),
            //            Document = c.String(),
            //            ContactFirstName = c.String(),
            //            ContactLastName = c.String(),
            //            Address = c.String(),
            //            Phone1 = c.Long(nullable: false),
            //            Phone2 = c.Long(nullable: false),
            //            Email = c.String(),
            //            Notes = c.String(),
            //        })
            //    .PrimaryKey(t => t.SupplierId)
            //    .ForeignKey("dbo.tbl_DocumentsType", t => t.DocumentTypeId, cascadeDelete: true)
            //    .Index(t => t.DocumentTypeId);
            
            CreateTable(
                "dbo.tbl_Shipments",
                c => new
                    {
                        ShippingId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        InvoiceNumber = c.Int(nullable: false),
                        StateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ShippingId)
                .ForeignKey("dbo.tbl_Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.tbl_Sellers",
                c => new
                    {
                        SellerId = c.Int(nullable: false, identity: true),
                        Document = c.String(nullable: false, maxLength: 13),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Telephone = c.Int(nullable: false),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.SellerId);
            
            //CreateTable(
            //    "dbo.AspNetRoles",
            //    c => new
            //        {
            //            Id = c.String(nullable: false, maxLength: 128),
            //            Name = c.String(nullable: false, maxLength: 256),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            //CreateTable(
            //    "dbo.AspNetUserRoles",
            //    c => new
            //        {
            //            UserId = c.String(nullable: false, maxLength: 128),
            //            RoleId = c.String(nullable: false, maxLength: 128),
            //        })
            //    .PrimaryKey(t => new { t.UserId, t.RoleId })
            //    .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
            //    .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
            //    .Index(t => t.UserId)
            //    .Index(t => t.RoleId);
            
            //CreateTable(
            //    "dbo.tbl_RolsPermission",
            //    c => new
            //        {
            //            PermissionId = c.Int(nullable: false, identity: true),
            //            RolId = c.Int(nullable: false),
            //            Form = c.String(nullable: false),
            //            CanSee = c.Boolean(nullable: false),
            //            CanModify = c.Boolean(nullable: false),
            //            CanErase = c.Boolean(nullable: false),
            //        })
            //    .PrimaryKey(t => t.PermissionId)
            //    .ForeignKey("dbo.tbl_Rols", t => t.RolId, cascadeDelete: true)
            //    .Index(t => t.RolId);
            
            //CreateTable(
            //    "dbo.tbl_Rols",
            //    c => new
            //        {
            //            RolId = c.Int(nullable: false, identity: true),
            //            Description = c.String(nullable: false),
            //        })
            //    .PrimaryKey(t => t.RolId);
            
            //CreateTable(
            //    "dbo.tbl_UsersSud",
            //    c => new
            //        {
            //            UserSudId = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false),
            //            LastName = c.String(nullable: false),
            //            Password = c.String(nullable: false, maxLength: 255),
            //            ModificationDatePassword = c.DateTime(nullable: false),
            //            RolId = c.Int(nullable: false),
            //            Email = c.String(nullable: false, maxLength: 255),
            //            Status = c.Boolean(nullable: false),
            //        })
            //    .PrimaryKey(t => t.UserSudId)
            //    .ForeignKey("dbo.tbl_Rols", t => t.RolId, cascadeDelete: true)
            //    .Index(t => t.RolId);
            
            //CreateTable(
            //    "dbo.AspNetUsers",
            //    c => new
            //        {
            //            Id = c.String(nullable: false, maxLength: 128),
            //            Email = c.String(maxLength: 256),
            //            EmailConfirmed = c.Boolean(nullable: false),
            //            PasswordHash = c.String(),
            //            SecurityStamp = c.String(),
            //            PhoneNumber = c.String(),
            //            PhoneNumberConfirmed = c.Boolean(nullable: false),
            //            TwoFactorEnabled = c.Boolean(nullable: false),
            //            LockoutEndDateUtc = c.DateTime(),
            //            LockoutEnabled = c.Boolean(nullable: false),
            //            AccessFailedCount = c.Int(nullable: false),
            //            UserName = c.String(nullable: false, maxLength: 256),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            //CreateTable(
            //    "dbo.AspNetUserClaims",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            UserId = c.String(nullable: false, maxLength: 128),
            //            ClaimType = c.String(),
            //            ClaimValue = c.String(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
            //    .Index(t => t.UserId);
            
            //CreateTable(
            //    "dbo.AspNetUserLogins",
            //    c => new
            //        {
            //            LoginProvider = c.String(nullable: false, maxLength: 128),
            //            ProviderKey = c.String(nullable: false, maxLength: 128),
            //            UserId = c.String(nullable: false, maxLength: 128),
            //        })
            //    .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
            //    .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
            //    .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.tbl_UsersSud", "RolId", "dbo.tbl_Rols");
            DropForeignKey("dbo.tbl_RolsPermission", "RolId", "dbo.tbl_Rols");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.tbl_Routes", "SellerId", "dbo.tbl_Sellers");
            DropForeignKey("dbo.tbl_Shipments", "OrderId", "dbo.tbl_Orders");
            DropForeignKey("dbo.tbl_Orders", "RouteId", "dbo.tbl_Routes");
            DropForeignKey("dbo.tbl_Orders", "ClientId", "dbo.tbl_Clients");
            DropForeignKey("dbo.tbl_Orders", "CellarId", "dbo.tbl_Cellars");
            DropForeignKey("dbo.PurchaseDetails", "PurchaseId", "dbo.Purchases");
            DropForeignKey("dbo.Purchases", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.Suppliers", "DocumentTypeId", "dbo.tbl_DocumentsType");
            DropForeignKey("dbo.Purchases", "CellarId", "dbo.tbl_Cellars");
            DropForeignKey("dbo.PurchaseDetails", "ProductId", "dbo.tbl_Products");
            DropForeignKey("dbo.PurchaseDetails", "KardexId", "dbo.Kardexes");
            DropForeignKey("dbo.tbl_InventoryDetails", "ProductId", "dbo.tbl_Products");
            DropForeignKey("dbo.tbl_InventoryDetails", "KardexId", "dbo.Kardexes");
            DropForeignKey("dbo.tbl_InventoryDetails", "InventoryId", "dbo.tbl_Inventories");
            DropForeignKey("dbo.tbl_Inventories", "CellarId", "dbo.tbl_Cellars");
            DropForeignKey("dbo.EgressDetails", "ProductId", "dbo.tbl_Products");
            DropForeignKey("dbo.EgressDetails", "KardexId", "dbo.Kardexes");
            DropForeignKey("dbo.EgressDetails", "EgressId", "dbo.Egresses");
            DropForeignKey("dbo.Egresses", "ConceptId", "dbo.Concepts");
            DropForeignKey("dbo.Egresses", "CellarId", "dbo.tbl_Cellars");
            DropForeignKey("dbo.tbl_OrderDetails", "ProductId", "dbo.tbl_Products");
            DropForeignKey("dbo.tbl_OrderDetails", "OrderId", "dbo.tbl_Orders");
            DropForeignKey("dbo.tbl_Products", "MeasureId", "dbo.tbl_Measures");
            DropForeignKey("dbo.tbl_Products", "DepartmentId", "dbo.tbl_Departments");
            DropForeignKey("dbo.tbl_ClientRefundDetails", "ProductId", "dbo.tbl_Products");
            DropForeignKey("dbo.tbl_SaleDetails", "SaleId", "dbo.tbl_Sales");
            DropForeignKey("dbo.tbl_SaleDetails", "ProductId", "dbo.tbl_Products");
            DropForeignKey("dbo.tbl_Sales", "PaymentMethodId", "dbo.tbl_PaymentMethods");
            DropForeignKey("dbo.tbl_ClientRefunds", "SaleId", "dbo.tbl_Sales");
            DropForeignKey("dbo.tbl_Sales", "ClientId", "dbo.tbl_Clients");
            DropForeignKey("dbo.tbl_Clients", "DocumentTypeId", "dbo.tbl_DocumentsType");
            DropForeignKey("dbo.tbl_Sales", "CellarId", "dbo.tbl_Cellars");
            DropForeignKey("dbo.tbl_Sales", "AccountingDocumentId", "dbo.tbl_AccountingDocuments");
            DropForeignKey("dbo.tbl_ClientRefundDetails", "ClientRefundId", "dbo.tbl_ClientRefunds");
            DropForeignKey("dbo.tbl_CellarProducts", "ProductId", "dbo.tbl_Products");
            DropForeignKey("dbo.tbl_BarCodes", "ProductId", "dbo.tbl_Products");
            DropForeignKey("dbo.tbl_CellarProducts", "CellarId", "dbo.tbl_Cellars");
            DropForeignKey("dbo.tbl_Routes", "AccountingDocumentId", "dbo.tbl_AccountingDocuments");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.tbl_UsersSud", new[] { "RolId" });
            DropIndex("dbo.tbl_RolsPermission", new[] { "RolId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.tbl_Shipments", new[] { "OrderId" });
            DropIndex("dbo.Suppliers", new[] { "DocumentTypeId" });
            DropIndex("dbo.Purchases", new[] { "CellarId" });
            DropIndex("dbo.Purchases", new[] { "SupplierId" });
            DropIndex("dbo.tbl_Inventories", new[] { "CellarId" });
            DropIndex("dbo.tbl_InventoryDetails", new[] { "KardexId" });
            DropIndex("dbo.tbl_InventoryDetails", new[] { "ProductId" });
            DropIndex("dbo.tbl_InventoryDetails", new[] { "InventoryId" });
            DropIndex("dbo.Egresses", new[] { "CellarId" });
            DropIndex("dbo.Egresses", new[] { "ConceptId" });
            DropIndex("dbo.EgressDetails", new[] { "KardexId" });
            DropIndex("dbo.EgressDetails", new[] { "ProductId" });
            DropIndex("dbo.EgressDetails", new[] { "EgressId" });
            DropIndex("dbo.PurchaseDetails", new[] { "KardexId" });
            DropIndex("dbo.PurchaseDetails", new[] { "ProductId" });
            DropIndex("dbo.PurchaseDetails", new[] { "PurchaseId" });
            DropIndex("dbo.tbl_OrderDetails", new[] { "ProductId" });
            DropIndex("dbo.tbl_OrderDetails", new[] { "OrderId" });
            DropIndex("dbo.tbl_SaleDetails", new[] { "ProductId" });
            DropIndex("dbo.tbl_SaleDetails", new[] { "SaleId" });
            DropIndex("dbo.tbl_Clients", new[] { "DocumentTypeId" });
            DropIndex("dbo.tbl_Sales", new[] { "PaymentMethodId" });
            DropIndex("dbo.tbl_Sales", new[] { "AccountingDocumentId" });
            DropIndex("dbo.tbl_Sales", new[] { "CellarId" });
            DropIndex("dbo.tbl_Sales", new[] { "ClientId" });
            DropIndex("dbo.tbl_ClientRefunds", new[] { "SaleId" });
            DropIndex("dbo.tbl_ClientRefundDetails", new[] { "ProductId" });
            DropIndex("dbo.tbl_ClientRefundDetails", new[] { "ClientRefundId" });
            DropIndex("dbo.tbl_BarCodes", "Barra_Codigo");
            DropIndex("dbo.tbl_BarCodes", "Barra_Producto");
            DropIndex("dbo.tbl_Products", new[] { "MeasureId" });
            DropIndex("dbo.tbl_Products", new[] { "DepartmentId" });
            DropIndex("dbo.tbl_CellarProducts", "CellarProducto_Producto");
            DropIndex("dbo.tbl_CellarProducts", "CellarProduct_Bodega");
            DropIndex("dbo.tbl_Orders", new[] { "RouteId" });
            DropIndex("dbo.tbl_Orders", new[] { "CellarId" });
            DropIndex("dbo.tbl_Orders", new[] { "ClientId" });
            DropIndex("dbo.tbl_Routes", new[] { "SellerId" });
            DropIndex("dbo.tbl_Routes", new[] { "AccountingDocumentId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.tbl_UsersSud");
            DropTable("dbo.tbl_Rols");
            DropTable("dbo.tbl_RolsPermission");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.tbl_Sellers");
            DropTable("dbo.tbl_Shipments");
            DropTable("dbo.Suppliers");
            DropTable("dbo.Purchases");
            DropTable("dbo.tbl_Inventories");
            DropTable("dbo.tbl_InventoryDetails");
            DropTable("dbo.Concepts");
            DropTable("dbo.Egresses");
            DropTable("dbo.EgressDetails");
            DropTable("dbo.Kardexes");
            DropTable("dbo.PurchaseDetails");
            DropTable("dbo.tbl_OrderDetails");
            DropTable("dbo.tbl_Measures");
            DropTable("dbo.tbl_Departments");
            DropTable("dbo.tbl_SaleDetails");
            DropTable("dbo.tbl_PaymentMethods");
            DropTable("dbo.tbl_DocumentsType");
            DropTable("dbo.tbl_Clients");
            DropTable("dbo.tbl_Sales");
            DropTable("dbo.tbl_ClientRefunds");
            DropTable("dbo.tbl_ClientRefundDetails");
            DropTable("dbo.tbl_BarCodes");
            DropTable("dbo.tbl_Products");
            DropTable("dbo.tbl_CellarProducts");
            DropTable("dbo.tbl_Cellars");
            DropTable("dbo.tbl_Orders");
            DropTable("dbo.tbl_Routes");
            DropTable("dbo.tbl_AccountingDocuments");
        }
    }
}
