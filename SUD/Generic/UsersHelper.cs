using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace SUD.Generic
{
    public class UsersHelper:IDisposable
    {
        private static ApplicationDbContext userSudContext = new ApplicationDbContext();

        public static void CheckRole(string roleName)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(userSudContext));

            // Comprueba que el rol exista, si no existe lo crea.
            if (!roleManager.RoleExists(roleName))
            {
                roleManager.Create(new IdentityRole(roleName));
            }
        }

        public static void CheckSuperUser()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userSudContext));
            var email = WebConfigurationManager.AppSettings["AdminUser"];
            var password = WebConfigurationManager.AppSettings["AdminPassword"];
            var userASP = userManager.FindByName(email);
            if (userASP == null)
            {
                CreateUserASP(email, "Administrador", password);
                return;
            }

            userManager.AddToRole(userASP.Id, "Administrador");
        }
        public static void CreateUserASP(string email, string roleName)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userSudContext));

            var userASP = new ApplicationUser
            {
                Email = email,
                UserName = email,
            };

            userManager.Create(userASP, email);
            userManager.AddToRole(userASP.Id, roleName);
        }

        public static void CreateUserASP(string email, string roleName, string password)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userSudContext));

            var userASP = new ApplicationUser
            {
                Email = email,
                UserName = email,
            };

            userManager.Create(userASP, password);
            userManager.AddToRole(userASP.Id, roleName);
        }

        public static bool DeleteUserSud(string username)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userSudContext));
            var userSudASP = userManager.FindByEmail(username);
            if (userSudASP == null)
            {
                return false;
            }

            var response = userManager.Delete(userSudASP);
            return response.Succeeded;

        }

        public static bool UpdateUsername(string currentUsername, string newUsername)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userSudContext));
            var userSudASP = userManager.FindByEmail(currentUsername);
            if (userSudASP == null)
            {
                return false;
            }
            userSudASP.UserName = newUsername;
            userSudASP.Email = newUsername;
            var response = userManager.Update(userSudASP);
            return response.Succeeded;

        }

        public static async Task PasswordRecovery(string email)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userSudContext));
            var userASP = userManager.FindByEmail(email);
            if (userASP == null)
            {
                return;
            }

            var user = userSudContext.UserSuds.Where(u => u.Email == email).FirstOrDefault();
            if (user == null)
            {
                return;
            }

            var random = new Random();
            var newPassword = string.Format("{0}{1}{2:04}*",
                user.Name.Trim().ToUpper().Substring(0, 1),
                user.LastName.Trim().ToLower(),
                random.Next(10000));

            userManager.RemovePassword(userASP.Id);
            userManager.AddPassword(userASP.Id, newPassword);

            var subject = "Recuperacion de contraseña del sistema";
            var body = string.Format(@"
                <h1>Recuperacion de contraseña del sistema</h1>
                <p>Su nueva contraseña es: <strong>{0}</strong></p>
                <p>Por favor se le recomienda cambiar esta contraseña por uno mas fácil de recordar.",
                newPassword);

            await EmailHelper.SendMail(email, subject, body);
        }

        public void Dispose()
        {
            userSudContext.Dispose();
        }

    }
}