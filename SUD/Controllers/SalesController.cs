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
    public class SalesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //New

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
                var saleDetailBk = db.SaleDetailBkps.Where(odb => odb.User == User.Identity.Name && odb.ProductId == view.ProductId).FirstOrDefault();

                if (saleDetailBk == null)
                {
                    var product = db.Products.Find(view.ProductId);
                    saleDetailBk = new SaleDetailBk
                    {
                        User = User.Identity.Name,
                        Description = product.Description,
                        Price = product.Price,
                        ProductId = product.ProductId,
                        Quantity = view.Quantity
                    };

                    db.SaleDetailBkps.Add(saleDetailBk);

                }
                else
                {
                    saleDetailBk.Quantity += view.Quantity;
                    db.Entry(saleDetailBk).State = EntityState.Modified;
                }


                db.SaveChanges();
                return RedirectToAction("Create");

            }
            ViewBag.ProductId = new SelectList(db.Products.OrderBy(p => p.Description), "ProductId", "Description");
            return View();
        }

        //EndNew



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
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Document");

            var view = new NewSaleView
            {
                Date = DateTime.Now,
                Details = db.SaleDetailBkps.Where(pdb => pdb.User == User.Identity.Name).ToList()
            };

            return View(view);
        }

        // POST: Sales/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SaleId,Datetime,ClientId,CellarId")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                db.Sales.Add(sale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CellarId = new SelectList(db.Cellars, "CellarId", "Description", sale.CellarId);
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Document", sale.ClientId);
            return View(sale);
        }

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
                var x = (from a in _context.Sales.Include("Client") select a);
                var y = (from a in _context.Sales.Include("AccountingDocument") select a);
                var z = (from a in _context.Sales.Include("PaymentMethod") select a);

                //SORT
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    w = w.OrderBy(sortColumn + " " + sortColumnDir);
                    x = x.OrderBy(sortColumn + " " + sortColumnDir);
                    y = y.OrderBy(sortColumn + " " + sortColumnDir);
                    z = z.OrderBy(sortColumn + " " + sortColumnDir);
                }

                recordsTotal = w.Count();
                var data = w.Skip(skip).Take(pageSize).ToList();
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
