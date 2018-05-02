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
    public class CellarProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CellarProducts
        public ActionResult Index()
        {
            var cellarProducts = db.CellarProducts.Include(c => c.Cellar).Include(c => c.Product);
            return View(cellarProducts.ToList());
        }

        // GET: CellarProducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CellarProduct cellarProduct = db.CellarProducts.Find(id);
            if (cellarProduct == null)
            {
                return HttpNotFound();
            }
            return View(cellarProduct);
        }

        // GET: CellarProducts/Create
        public ActionResult Create()
        {
            ViewBag.CellarId = new SelectList(db.Cellars, "CellarId", "Description");
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Description");
            return View();
        }

        // POST: CellarProducts/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CellarProductId,CellarId,ProductId,Stock,Minimum,Maximum,ReplacementDays,MinimumAmount")] CellarProduct cellarProduct)
        {
            if (ModelState.IsValid)
            {
                db.CellarProducts.Add(cellarProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CellarId = new SelectList(db.Cellars, "CellarId", "Description", cellarProduct.CellarId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Description", cellarProduct.ProductId);
            return View(cellarProduct);
        }

        // GET: CellarProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CellarProduct cellarProduct = db.CellarProducts.Find(id);
            if (cellarProduct == null)
            {
                return HttpNotFound();
            }
            ViewBag.CellarId = new SelectList(db.Cellars, "CellarId", "Description", cellarProduct.CellarId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Description", cellarProduct.ProductId);
            return View(cellarProduct);
        }

        // POST: CellarProducts/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CellarProductId,CellarId,ProductId,Stock,Minimum,Maximum,ReplacementDays,MinimumAmount")] CellarProduct cellarProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cellarProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CellarId = new SelectList(db.Cellars, "CellarId", "Description", cellarProduct.CellarId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Description", cellarProduct.ProductId);
            return View(cellarProduct);
        }

        // GET: CellarProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CellarProduct cellarProduct = db.CellarProducts.Find(id);
            if (cellarProduct == null)
            {
                return HttpNotFound();
            }
            return View(cellarProduct);
        }

        // POST: CellarProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CellarProduct cellarProduct = db.CellarProducts.Find(id);
            db.CellarProducts.Remove(cellarProduct);
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
