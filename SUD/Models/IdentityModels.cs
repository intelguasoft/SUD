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

        public System.Data.Entity.DbSet<SUD.Models.Rol> Rols { get; set; }
        //No se generaba al crear el controlador
        public System.Data.Entity.DbSet<SUD.Models.User> Users { get; set; }

        public System.Data.Entity.DbSet<SUD.Models.RolPermission> RolPermissions { get; set; }

        public System.Data.Entity.DbSet<SUD.Models.Department> Departments { get; set; }

        public System.Data.Entity.DbSet<SUD.Models.Measure> Measures { get; set; }

        public System.Data.Entity.DbSet<SUD.Models.Cellar> Cellars { get; set; }

        public System.Data.Entity.DbSet<SUD.Models.Product> Products { get; set; }

        public System.Data.Entity.DbSet<SUD.Models.BarCode> BarCodes { get; set; }

        public System.Data.Entity.DbSet<SUD.Models.CellarProduct> CellarProducts { get; set; }

        public System.Data.Entity.DbSet<SUD.Models.Inventory> Inventories { get; set; }

        public System.Data.Entity.DbSet<SUD.Models.InventoryDetail> InventoryDetails { get; set; }
    }
}