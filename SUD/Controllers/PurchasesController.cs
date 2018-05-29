using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SUD.Models;
using SUD.ViewModels;

namespace SUD.Controllers
{
    public class PurchasesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Lanza la vista que permite agregar el producto
        /// </summary>
        /// <returns></returns>
        public ActionResult AddProduct()
        {
            ViewBag.ProductId = new SelectList(db.Products.OrderBy(p => p.Description), "ProductId", "Description");
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(AddProductView view)
        {
            if (ModelState.IsValid)
            {
                var purchaseDetailBk = db.PurchaseDetailBkps.Where(pdb => pdb.User == User.Identity.Name && pdb.ProductId == view.ProductId).FirstOrDefault();
                if (purchaseDetailBk == null)
                {
                    var product = db.Products.Find(view.ProductId);
                    purchaseDetailBk = new PurchaseDetailBk
                    {
                        Description = product.Description,
                        User = User.Identity.Name,
                        Cost = product.Price,
                        ProductId = product.ProductId,
                        Quantity = view.Quantity,
                        ManufacturingLot = view.ManufacturingLot,
                        DueDate = view.DueDate

                    };

                    db.PurchaseDetailBkps.Add(purchaseDetailBk);

                }
                else
                {
                    purchaseDetailBk.Quantity += view.Quantity;
                    db.Entry(purchaseDetailBk).State = EntityState.Modified;
                }

                db.SaveChanges();
                return RedirectToAction("Create");
            }
            ViewBag.ProductId = new SelectList(db.Products.OrderBy(p => p.Description), "ProductId", "Description");
            return View();
        }

        public ActionResult DeleteProduct(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var purchaseDetailBkps = db.PurchaseDetailBkps.Where(pdb => pdb.User == User.Identity.Name && pdb.ProductId == id).FirstOrDefault();
            if (purchaseDetailBkps == null)
            {
                return HttpNotFound();
            }

            db.PurchaseDetailBkps.Remove(purchaseDetailBkps);
            db.SaveChanges();

            return RedirectToAction("Create");
        }

        public ActionResult DeleteAllProduct()
        {

            var purchaseDetailBkps = db.PurchaseDetailBkps.Where(pdb => pdb.User == User.Identity.Name).ToList();
            if (purchaseDetailBkps == null)
            {
                return HttpNotFound();
            }
            foreach (var detail in purchaseDetailBkps)
            {
                db.PurchaseDetailBkps.Remove(detail);
            }
            db.SaveChanges();

            return RedirectToAction("Create");
        }
        // GET: Purchases
        public ActionResult Index()
        {
            var purchases = db.Purchases.Include(p => p.Cellar).Include(p => p.States).Include(p => p.Supplier);
            return View(purchases.ToList());
        }

        // GET: Purchases/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            return View(purchase);
        }

        // GET: Purchases/Create
        public ActionResult Create()
        {
            ViewBag.CellarId = new SelectList(db.Cellars, "CellarId", "Description");
            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierId", "Tradename");

            var view = new NewPurchaseView
            {
                Date = DateTime.Now,
                Details = db.PurchaseDetailBkps.Where(pdb => pdb.User == User.Identity.Name).ToList()
            };

            return View(view);
        }

        // POST: Purchases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NewPurchaseView view)
        {
            if (ModelState.IsValid)
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var purchase = new Purchase
                        {
                            Date = view.Date,
                            SupplierId = view.SupplierId,
                            CellarId = view.CellarId
                        };

                        db.Purchases.Add(purchase);
                        db.SaveChanges();

                        var details = db.PurchaseDetailBkps.Where(pdb => pdb.User == User.Identity.Name).ToList();
                        foreach (var detail in details)
                        {
                            var purchaseDetail = new PurchaseDetail
                            {
                                PurchaseId = purchase.PurchaseId,
                                ProductId = detail.ProductId,
                                Description = detail.Description,
                                Cost = detail.Cost,
                                Quantity = detail.Quantity,
                                ManufacturingLot = detail.ManufacturingLot,
                                DueDate = detail.DueDate
                            };

                            db.PurchaseDetails.Add(purchaseDetail);
                            db.PurchaseDetailBkps.Remove(detail);
                        }

                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                }

                return RedirectToAction("Index");
            }

            ViewBag.CellarId = new SelectList(db.Cellars, "CellarId", "Description", view.CellarId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierId", "Tradename", view.SupplierId);
            view.Details = db.PurchaseDetailBkps.Where(pdb => pdb.User == User.Identity.Name).ToList();
            return View(view);
        }

        // GET: Purchases/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            ViewBag.CellarId = new SelectList(db.Cellars, "CellarId", "Description", purchase.CellarId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierId", "Tradename", purchase.SupplierId);
            return View(purchase);
        }

        // POST: Purchases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PurchaseId,Date,SupplierId,CellarId,StateId")] Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CellarId = new SelectList(db.Cellars, "CellarId", "Description", purchase.CellarId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierId", "Tradename", purchase.SupplierId);
            return View(purchase);
        }

        // GET: Purchases/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            return View(purchase);
        }

        // POST: Purchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Purchase purchase = db.Purchases.Find(id);
            db.Purchases.Remove(purchase);
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
