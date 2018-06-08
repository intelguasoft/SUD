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
    [Authorize(Roles = "Administrador")]

    public class InventoryDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: InventoryDetails
        public ActionResult Index()
        {
            var inventoryDetails = db.InventoryDetails.Include(i => i.Inventory).Include(i => i.Product);
            return View(inventoryDetails.ToList());
        }

        // GET: InventoryDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventoryDetail inventoryDetail = db.InventoryDetails.Find(id);
            if (inventoryDetail == null)
            {
                return HttpNotFound();
            }
            return View(inventoryDetail);
        }

        // GET: InventoryDetails/Create
        public ActionResult Create()
        {
            ViewBag.InventoryId = new SelectList(db.Inventories, "InventoryId", "InventoryId");
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Description");
            return View();
        }

        // POST: InventoryDetails/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LineId,InventoryId,ProductId,Description,Stock,Count1,Count2,Count3,Adjustment,KardexId")] InventoryDetail inventoryDetail)
        {
            if (ModelState.IsValid)
            {
                db.InventoryDetails.Add(inventoryDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InventoryId = new SelectList(db.Inventories, "InventoryId", "InventoryId", inventoryDetail.InventoryId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Description", inventoryDetail.ProductId);
            return View(inventoryDetail);
        }

        // GET: InventoryDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventoryDetail inventoryDetail = db.InventoryDetails.Find(id);
            if (inventoryDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.InventoryId = new SelectList(db.Inventories, "InventoryId", "InventoryId", inventoryDetail.InventoryId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Description", inventoryDetail.ProductId);
            return View(inventoryDetail);
        }

        // POST: InventoryDetails/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LineId,InventoryId,ProductId,Description,Stock,Count1,Count2,Count3,Adjustment,KardexId")] InventoryDetail inventoryDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inventoryDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InventoryId = new SelectList(db.Inventories, "InventoryId", "InventoryId", inventoryDetail.InventoryId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Description", inventoryDetail.ProductId);
            return View(inventoryDetail);
        }

        // GET: InventoryDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventoryDetail inventoryDetail = db.InventoryDetails.Find(id);
            if (inventoryDetail == null)
            {
                return HttpNotFound();
            }
            return View(inventoryDetail);
        }

        // POST: InventoryDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InventoryDetail inventoryDetail = db.InventoryDetails.Find(id);
            db.InventoryDetails.Remove(inventoryDetail);
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
