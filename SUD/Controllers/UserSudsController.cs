using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SUD.Models;

namespace SUD.Controllers
{
    public class UserSudsController : Controller
    {
        private ApplicationDbContext db;

        public UserSudsController()
        {
            db = new ApplicationDbContext();
        }

        // GET: UserSuds
        public ActionResult Index()
        {
            var userSuds = db.UserSuds.Include(u => u.Rol);
            return View(userSuds.ToList());
        }

        // GET: UserSuds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSud userSud = db.UserSuds.Find(id);
            if (userSud == null)
            {
                return HttpNotFound();
            }
            return View(userSud);
        }

        // GET: UserSuds/Create
        public ActionResult Create()
        {
            ViewBag.RolId = new SelectList(db.Rols, "RolId", "Description");
            return View();
        }

        // POST: UserSuds/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserSudId,Name,LastName,Password,ModificationDatePassword,RolId,Email")] UserSud userSud)
        {
            if (ModelState.IsValid)
            {
                db.UserSuds.Add(userSud);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RolId = new SelectList(db.Rols, "RolId", "Description", userSud.RolId);
            return View(userSud);
        }

        // GET: UserSuds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSud userSud = db.UserSuds.Find(id);
            if (userSud == null)
            {
                return HttpNotFound();
            }
            ViewBag.RolId = new SelectList(db.Rols, "RolId", "Description", userSud.RolId);
            return View(userSud);
        }

        // POST: UserSuds/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserSudId,Name,LastName,Password,ModificationDatePassword,RolId,Email")] UserSud userSud)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userSud).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RolId = new SelectList(db.Rols, "RolId", "Description", userSud.RolId);
            return View(userSud);
        }

        // GET: UserSuds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSud userSud = db.UserSuds.Find(id);
            if (userSud == null)
            {
                return HttpNotFound();
            }
            return View(userSud);
        }

        // POST: UserSuds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserSud userSud = db.UserSuds.Find(id);
            db.UserSuds.Remove(userSud);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
