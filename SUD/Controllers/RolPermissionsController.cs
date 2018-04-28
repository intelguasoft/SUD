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
    public class RolPermissionsController : Controller
    {
        private ApplicationDbContext db;

        public RolPermissionsController()
        {
            db = new ApplicationDbContext();
        }

        // GET: RolPermissions
        public ActionResult Index()
        {
            var rolPermissions = db.RolPermissions.Include(r => r.Rol);
            return View(rolPermissions.ToList());
        }

        // GET: RolPermissions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RolPermission rolPermission = db.RolPermissions.Find(id);
            if (rolPermission == null)
            {
                return HttpNotFound();
            }
            return View(rolPermission);
        }

        // GET: RolPermissions/Create
        public ActionResult Create()
        {
            ViewBag.IDRol = new SelectList(db.Rols, "IDRol", "Description");
            return View();
        }

        // POST: RolPermissions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDPermission,IDRol,CanSee,CanModify,CanErase")] RolPermission rolPermission)
        {
            if (ModelState.IsValid)
            {
                db.RolPermissions.Add(rolPermission);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDRol = new SelectList(db.Rols, "IDRol", "Description", rolPermission.IDRol);
            return View(rolPermission);
        }

        // GET: RolPermissions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RolPermission rolPermission = db.RolPermissions.Find(id);
            if (rolPermission == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDRol = new SelectList(db.Rols, "IDRol", "Description", rolPermission.IDRol);
            return View(rolPermission);
        }

        // POST: RolPermissions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDPermission,IDRol,CanSee,CanModify,CanErase")] RolPermission rolPermission)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rolPermission).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDRol = new SelectList(db.Rols, "IDRol", "Description", rolPermission.IDRol);
            return View(rolPermission);
        }

        // GET: RolPermissions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RolPermission rolPermission = db.RolPermissions.Find(id);
            if (rolPermission == null)
            {
                return HttpNotFound();
            }
            return View(rolPermission);
        }

        // POST: RolPermissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RolPermission rolPermission = db.RolPermissions.Find(id);
            db.RolPermissions.Remove(rolPermission);
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
