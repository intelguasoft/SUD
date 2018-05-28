using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SUD.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Rol> Rols { get; set; }

        public DbSet<UserSud> UserSuds { get; set; }

        public DbSet<RolPermission> RolPermissions { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Measure> Measures { get; set; }

        public DbSet<Cellar> Cellars { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<BarCode> BarCodes { get; set; }

        public DbSet<CellarProduct> CellarProducts { get; set; }

        public DbSet<Inventory> Inventories { get; set; }

        public DbSet<InventoryDetail> InventoryDetails { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<DocumentType> DocumentTypes { get; set; }

        public DbSet<ClientRefund> ClientRefunds { get; set; }

        public DbSet<Sale> Sales { get; set; }

        public DbSet<ClientRefundDetail> ClientRefundDetails { get; set; }

        public DbSet<SaleDetail> SaleDetails { get; set; }

        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseDetail> PurchaseDetails { get; set; }
        public DbSet<PurchaseDetailBk> PurchaseDetailBkps { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<AccountingDocument> AccountingDocuments { get; set; }

        public DbSet<PaymentMethod> PaymentMethods { get; set; }

        public System.Data.Entity.DbSet<SUD.Models.State> States { get; set; }

        public System.Data.Entity.DbSet<SUD.Models.Route> Routes { get; set; }

        public System.Data.Entity.DbSet<SUD.Models.Order> Orders { get; set; }

        public System.Data.Entity.DbSet<SUD.Models.OrderDetail> OrderDetails { get; set; }

        public System.Data.Entity.DbSet<SUD.Models.OrderDetailBk> OrderDetailBkps { get; set; }

        public System.Data.Entity.DbSet<SUD.Models.Shipping> Shippings { get; set; }
    }
}