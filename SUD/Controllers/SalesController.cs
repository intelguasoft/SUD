using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SUD.Models;
using SUD.ViewModels; // New

namespace SUD.Controllers
{
    [Authorize(Roles = "Gerente de ventas, Administrador")]

    public class SalesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //New

        public ActionResult AddProduct()
        {
            ViewBag.ProductId = new SelectList(db.Products.OrderBy(p => p.Description), "ProductId", "Description");


            return View();
        }

        //[HttpPost]
        //public ActionResult AddNewProduct(int id, AddProductSaleView view)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var saleDetailBk = db.SaleDetailBkps.Where(odb => odb.User == User.Identity.Name && odb.ProductId == view.ProductId).FirstOrDefault();

        //        if (saleDetailBk == null)
        //        {
        //            var product = db.Products.Find(id);
        //            saleDetailBk = new SaleDetailBk
        //            {
        //                User = User.Identity.Name,
        //                Description = product.Description,
        //                Price = product.Price,
        //                ProductId = product.ProductId,
        //                Quantity = 1, //view.Quantity,
        //                IVAPercentage = 1,//view.IVAPercentage,
        //                DiscountRate = 1, //view.DiscountRate,
        //                KardexId = 100 //TODO quitar la variable estatica cuando se tenga kardex listo.

        //            };

        //            db.SaleDetailBkps.Add(saleDetailBk);

        //        }
        //        else
        //        {
        //            saleDetailBk.Quantity += view.Quantity;
        //            db.Entry(saleDetailBk).State = EntityState.Modified;
        //        }


        //        db.SaveChanges();
        //        return RedirectToAction("Create");

        //    }
        //    ViewBag.ProductId = new SelectList(db.Products.OrderBy(p => p.Description), "ProductId", "Description");
        //    return View();
        //}

        //EndNew

        [HttpPost]
        public JsonResult AddProduct(AddProductView view)
        {
            if (ModelState.IsValid)
            {
                var saleDetailBk = db.SaleDetailBkps.Where(odb => odb.User == User.Identity.Name && odb.ProductId == view.ProductId).FirstOrDefault();
                var producto = db.CellarProducts.Find(view.ProductId);

                if (saleDetailBk == null)
                {
                    var product = db.Products.Find(view.ProductId);
                    saleDetailBk = new SaleDetailBk
                    {
                        User = User.Identity.Name,
                        Description = product.Description,
                        Price = product.Price,
                        ProductId = product.ProductId,
                        Quantity = view.Quantity,
                        IVAPercentage = 1,//view.IVAPercentage,
                        DiscountRate = 1, //view.DiscountRate,
                        KardexId = 100 //TODO quitar la variable estatica cuando se tenga kardex listo.
                    };



                    if (producto.Stock > view.Quantity)
                    {
                        db.SaleDetailBkps.Add(saleDetailBk);
                    }

                }
                else
                {

                    if (producto.Stock > view.Quantity)
                    {
                        saleDetailBk.Quantity += view.Quantity;
                        db.Entry(saleDetailBk).State = EntityState.Modified;
                    }

                }


                db.SaveChanges();

            }
            return Json(view);
        }

        public ActionResult Detalle()
        {

            var view = new NewSaleView
            {
                Details = db.SaleDetailBkps.Where(pdb => pdb.User == User.Identity.Name).ToList()
            };

            return PartialView();
        }

        // GET: Sales
        public ActionResult Index()
        {
            var sales = db.Sales.Include(s => s.Cellar).Include(s => s.Client);
            return View(sales.ToList());
        }

        // GET: Sales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // GET: Sales/Create
        public ActionResult Create()
        {
            ViewBag.CellarId = new SelectList(db.Cellars, "CellarId", "Description");
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "ComertialName");
            ViewBag.AccountingDocumentId = new SelectList(db.AccountingDocuments, "AccountingDocumentId", "Description");
            ViewBag.PaymentMethodId = new SelectList(db.PaymentMethods, "PaymentMethodId", "Description");

            var view = new NewSaleView
            {
                Date = DateTime.Now,
                Details = db.SaleDetailBkps.Where(pdb => pdb.User == User.Identity.Name).ToList()
            };

            return View(view);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NewSaleView view)
        {
            if (ModelState.IsValid)
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {

                        AccountingDocument dn = (from x in db.AccountingDocuments orderby x.InitialNumber descending select x).FirstOrDefault();
                        var docnumber = dn.InitialNumber + 1;

                        var sale = new Sale
                        {
                            Datetime = view.Date,
                            CellarId = view.CellarId,
                            ClientId = view.ClientId,
                            AccountingDocumentId = view.AccountingDocumentId,
                            PaymentMethodId = view.PaymentMethodId,
                            DocumentNumber = docnumber,





                        };

                        db.Sales.Add(sale);
                        db.SaveChanges();

                        var details = db.SaleDetailBkps.Where(pdb => pdb.User == User.Identity.Name).ToList();
                        foreach (var detail in details)
                        {
                            var saleDetail = new SaleDetail
                            {
                                SaleId = sale.SaleId,
                                ProductId = detail.ProductId,
                                Description = detail.Description,
                                Price = detail.Price,
                                Quantity = detail.Quantity,
                                IVAPercentage = detail.IVAPercentage,
                                DiscountRate = detail.DiscountRate


                            };


                            db.SaleDetails.Add(saleDetail);
                            db.SaleDetailBkps.Remove(detail);
                            var status = true;


                            if (status == true)
                            {
                                CellarProduct c = (from x in db.CellarProducts
                                                   where x.ProductId == detail.ProductId
                                                   select x).First();
                                c.Stock = c.Stock - detail.Quantity;
                                db.SaveChanges();

                            }

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
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Cliente", view.ClientId);
            ViewBag.AccountingDocumentId = new SelectList(db.AccountingDocuments, "AccountingDocumentId", "Tipo de Documento", view.AccountingDocumentId);
            ViewBag.PaymentMethodId = new SelectList(db.PaymentMethods, "PaymentMethodId", "Metodo de Pago", view.PaymentMethodId);
            view.Details = db.SaleDetailBkps.Where(pdb => pdb.User == User.Identity.Name).ToList();
            return View(view);

        }

        public ActionResult DeleteProduct(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var saleDetailBk = db.SaleDetailBkps.Where(pdb => pdb.User == User.Identity.Name && pdb.ProductId == id).FirstOrDefault();
            if (saleDetailBk == null)
            {
                return HttpNotFound();
            }

            db.SaleDetailBkps.Remove(saleDetailBk);
            db.SaveChanges();

            return RedirectToAction("Create");
        }

        public ActionResult DeleteAllProduct()
        {

            var saleDetailBk = db.SaleDetailBkps.Where(pdb => pdb.User == User.Identity.Name).ToList();
            if (saleDetailBk == null)
            {
                return HttpNotFound();
            }
            foreach (var detail in saleDetailBk)
            {
                db.SaleDetailBkps.Remove(detail);
            }
            db.SaveChanges();

            return RedirectToAction("Create");
        }

        // POST: Sales/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "SaleId,Datetime,ClientId,CellarId")] Sale sale)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Sales.Add(sale);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.CellarId = new SelectList(db.Cellars, "CellarId", "Description", sale.CellarId);
        //    ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Document", sale.ClientId);
        //    return View(sale);
        //}

        // GET: Sales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            ViewBag.CellarId = new SelectList(db.Cellars, "CellarId", "Description", sale.CellarId);
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Document", sale.ClientId);
            return View(sale);
        }

        // POST: Sales/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SaleId,Datetime,ClientId,CellarId")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sale).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CellarId = new SelectList(db.Cellars, "CellarId", "Description", sale.CellarId);
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Document", sale.ClientId);
            return View(sale);
        }

        // GET: Sales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sale sale = db.Sales.Find(id);
            db.Sales.Remove(sale);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult getSales()
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();

            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();


            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            using (ApplicationDbContext _context = new ApplicationDbContext())
            {
                _context.Configuration.LazyLoadingEnabled = false; // esto es necesario si nuestra tabla esta relacionado y por cosiguiente tiene claves foraneas

                var w = (from a in _context.Sales.Include("Client") select a);
                var x = (from b in _context.Sales.Include("Cellar") select b);
                var y = (from c in _context.Sales.Include("AccountingDocument") select c);
                var z = (from d in _context.Sales.Include("PaymentMethod") select d);

                //SORT
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    w = w.OrderBy(sortColumn + " " + sortColumnDir);

                }

                recordsTotal = w.Count();
                var data = w.Skip(skip).Take(pageSize).ToList();
                data.Union(x).ToList();
                data.Union(y).ToList();
                data.Union(z).ToList();




                return Json(new { draw, recordsFiltered = recordsTotal, recordsTotal, data }, JsonRequestBehavior.AllowGet);
            }

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
