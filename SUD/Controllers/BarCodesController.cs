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

    public class BarCodesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BarCodes
        public ActionResult Index()
        {
            var barCodes = db.BarCodes.Include(b => b.Product);
            return View(barCodes.ToList());
        }

        // GET: BarCodes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BarCode barCode = db.BarCodes.Find(id);
            if (barCode == null)
            {
                return HttpNotFound();
            }
            return View(barCode);
        }

        // GET: BarCodes/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Description");
            return View();
        }

        // POST: BarCodes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BarCodeId,ProductId,Bar")] BarCode barCode)
        {
            if (ModelState.IsValid)
            {
                db.BarCodes.Add(barCode);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Description", barCode.ProductId);
            return View(barCode);
        }

        // GET: BarCodes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BarCode barCode = db.BarCodes.Find(id);
            if (barCode == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Description", barCode.ProductId);
            return View(barCode);
        }

        // POST: BarCodes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BarCodeId,ProductId,Bar")] BarCode barCode)
        {
            if (ModelState.IsValid)
            {
                db.Entry(barCode).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Description", barCode.ProductId);
            return View(barCode);
        }

        // GET: BarCodes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BarCode barCode = db.BarCodes.Find(id);
            if (barCode == null)
            {
                return HttpNotFound();
            }
            return View(barCode);
        }

        // POST: BarCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BarCode barCode = db.BarCodes.Find(id);
            db.BarCodes.Remove(barCode);
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
