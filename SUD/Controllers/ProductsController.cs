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

    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category).Include(p => p.Measure);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Description");
            ViewBag.MeasureId = new SelectList(db.Measures, "MeasureId", "Description");
            return View();
        }

        // POST: Products/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,CategoryId,MeasureId,Description,Price,Note,Image,Medida,FotografiaFile")] Product product)
        {


            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();

                if (product.FotografiaFile != null)
                {
                    var folder = "~/Uploads/Products";
                    var response = FilesHelper.UploadPhoto(product.FotografiaFile, folder, string.Format("{0}.jpg", product.ProductId));
                    if (response)
                    {
                        var pic = string.Format("{0}/{1}.jpg", folder, product.ProductId);
                        product.Image = pic;

                        db.Entry(product).State = EntityState.Modified;
                        db.SaveChanges();


                    }
                }
                return RedirectToAction("Index");



            }

            ViewBag.DepartmentId = new SelectList(db.Categories, "CategoryId", "Description", product.CategoryId);
            ViewBag.MeasureId = new SelectList(db.Measures, "MeasureId", "Description", product.MeasureId);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Categories, "CategoryId", "Description", product.CategoryId);
            ViewBag.MeasureId = new SelectList(db.Measures, "MeasureId", "Description", product.MeasureId);
            return View(product);
        }

        // POST: Products/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,CategoryId,MeasureId,Description,Price,Note,Image,Medida,FotografiaFile")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();

                if (product.FotografiaFile != null)
                {
                    var folder = "~/Uploads/Products";
                    var response = FilesHelper.UploadPhoto(product.FotografiaFile, folder, string.Format("{0}.jpg", product.ProductId));
                    if (response)
                    {
                        var pic = string.Format("{0}/{1}.jpg", folder, product.ProductId);
                        product.Image = pic;

                        db.Entry(product).State = EntityState.Modified;
                        db.SaveChanges();


                    }
                }

                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Categories, "CategoryId", "Description", product.CategoryId);
            ViewBag.MeasureId = new SelectList(db.Measures, "MeasureId", "Description", product.MeasureId);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
