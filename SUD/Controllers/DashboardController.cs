using SUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SUD.Controllers
{
    public class DashboardController : Controller
    {
        private ApplicationDbContext db;

        public DashboardController()
        {
            db = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var user = db.UserSuds.Where(u => u.Email == User.Identity.Name).FirstOrDefault();
            //string[] myUser = { user.Email, user.LastName, user.Name, user.Password, user.Rol.Description, user.Status.ToString() };

            //System.Web.HttpContext.Current.Session["UserInfo"] = myUser;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}