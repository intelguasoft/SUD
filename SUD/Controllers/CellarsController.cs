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
    [Authorize(Roles = "Jefe de bodega, Administrador")]

    public class CellarsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cellars
        public ActionResult Index()
        {
            return View(db.Cellars.ToList());
        }

        // GET: Cellars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cellar cellar = db.Cellars.Find(id);
            if (cellar == null)
            {
                return HttpNotFound();
            }
            return View(cellar);
        }

        // GET: Cellars/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cellars/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CellarId,Description")] Cellar cellar)
        {
            if (ModelState.IsValid)
            {
                db.Cellars.Add(cellar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cellar);
        }

        // GET: Cellars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cellar cellar = db.Cellars.Find(id);
            if (cellar == null)
            {
                return HttpNotFound();
            }
            return View(cellar);
        }

        // POST: Cellars/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CellarId,Description")] Cellar cellar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cellar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cellar);
        }

        // GET: Cellars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cellar cellar = db.Cellars.Find(id);
            if (cellar == null)
            {
                return HttpNotFound();
            }
            return View(cellar);
        }

        // POST: Cellars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cellar cellar = db.Cellars.Find(id);
            db.Cellars.Remove(cellar);
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
