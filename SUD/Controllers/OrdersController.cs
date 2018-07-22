using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SUD.Generic;
using SUD.Models;
using SUD.ViewModels;

namespace SUD.Controllers
{
    [Authorize(Roles = "Jefe de bodega, Administrador")]

    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult AddProduct()
        {
            ViewBag.ProductId = new SelectList(db.CellarProducts.Include(cp => cp.Product).OrderBy(cp => cp.Product.Description), "ProductId", "Product.Description");
            return View();
        }
        
        [HttpPost]
        public JsonResult AddProduct(AddProductView view)
        {
            if (ModelState.IsValid)
            {
                var orderDetailBk = db.OrderDetailBkps.Where(odb => odb.User == User.Identity.Name && odb.ProductId == view.ProductId).FirstOrDefault();
                var bodega = db.CellarProducts.Where(cp => cp.ProductId == view.ProductId).FirstOrDefault();

                if (orderDetailBk == null)
                {
                    var product = db.Products.Find(view.ProductId);
                    orderDetailBk = new OrderDetailBk
                    {
                        User = User.Identity.Name,
                        Description = product.Description,
                        Price = product.Price,
                        ProductId = product.ProductId,
                        Quantity = view.Quantity
                    };



                    if (bodega.Stock > view.Quantity)
                    {
                        db.OrderDetailBkps.Add(orderDetailBk);
                    }

                }
                else
                {

                    if (bodega.Stock > view.Quantity)
                    {
                        orderDetailBk.Quantity += view.Quantity;
                        db.Entry(orderDetailBk).State = EntityState.Modified;
                    }

                }


                db.SaveChanges();

            }
            return Json(view);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddClient(Client model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Clients.Add(model);
                    db.SaveChanges();
                }


                return Json(model);
            }
            catch (Exception ex)
            {

                return Json(ex);
            }
        }


        public ActionResult Detalle()
        {

            var view = new DetailBkView
            {
                Details = db.OrderDetailBkps.Where(pdb => pdb.User == User.Identity.Name).ToList()
            };

            return PartialView("Detalle", view);
        }

        public ActionResult DetailOrders(int? id)
        {

            var view = new DetailOrderView
            {
                Details = db.OrderDetails.Where(pdb => pdb.OrderId == id).ToList()
            };

            return PartialView("DetailOrders", view);
        }



        public ActionResult DeleteProduct(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var orderDetailBk = db.OrderDetailBkps.Where(pdb => pdb.User == User.Identity.Name && pdb.ProductId == id).FirstOrDefault();
            if (orderDetailBk == null)
            {
                return HttpNotFound();
            }

            db.OrderDetailBkps.Remove(orderDetailBk);
            db.SaveChanges();

            return RedirectToAction("AddProduct");
        }

        public ActionResult DeleteAllProduct()
        {

            var orderDetailBk = db.OrderDetailBkps.Where(pdb => pdb.User == User.Identity.Name).ToList();
            if (orderDetailBk == null)
            {
                return HttpNotFound();
            }
            foreach (var detail in orderDetailBk)
            {
                db.OrderDetailBkps.Remove(detail);
            }
            db.SaveChanges();

            return RedirectToAction("AddProduct");
        }

        [HttpPost]
        public ActionResult Shipping(int? id)
        {
            if (ModelState.IsValid)
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        Order ord = db.Orders.Find(id);
                        
                        ord.StateId = 2;
                        db.Entry(ord).State = EntityState.Modified;

                        var document = db.AccountingDocuments.Where(acd => acd.AccountingDocumentId == ord.Route.AccountingDocumentId).FirstOrDefault();

                        var ship = new Shipping
                        {
                            OrderId = ord.OrderId,
                            Date = DateTime.Now.AddDays(1),
                            InvoiceNumber = document.CurrentNumber,
                            StateId = 2
                        };

                        db.Shippings.Add(ship);

                        document.CurrentNumber = document.CurrentNumber + 1;
                       
                        db.Entry(document).State = EntityState.Modified;

                        db.SaveChanges();
                        transaction.Commit(); 
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                }

                return RedirectToAction("Index", "Shipments");
            }

            return RedirectToAction("Index");
        }


        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Cellar).Include(o => o.Client).Include(o => o.Route).Include(o => o.State);
            return View(orders.Where(o => o.StateId == 1).ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.CellarId = new SelectList(db.Cellars.OrderBy(b => b.Description), "CellarId", "Description");
            ViewBag.ClientId = new SelectList(db.Clients.OrderBy(cl => cl.ComertialName), "ClientId", "ComertialName");
            ViewBag.RouteId = new SelectList(db.Routes.OrderBy(rt => rt.RouteNumber), "RouteId", "RouteNumber");

            var view = new NewOrderView
            {
                Date = DateTime.Now,
                Details = db.OrderDetailBkps.Where(pdb => pdb.User == User.Identity.Name).ToList()
            };

            return View(view);
        }

        // POST: Orders/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NewOrderView view)
        {
            if (ModelState.IsValid)
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var order = new Order
                        {
                            Date = view.Date,
                            ClientId = view.ClientId,
                            CellarId = view.CellarId,
                            RouteId = view.RouteId,
                            StateId = 1
                        };

                        db.Orders.Add(order);
                        db.SaveChanges();

                        var detailbks = db.OrderDetailBkps.Where(pdb => pdb.User == User.Identity.Name).ToList();

                        foreach (var detail in detailbks)
                        {
                            var orderDetail = new OrderDetail
                            {
                                OrderId = order.OrderId,
                                ProductId = detail.ProductId,
                                Description = detail.Description,
                                Price = detail.Price,
                                Quantity = detail.Quantity
                            };

                            var bodega = db.CellarProducts.Where(cp => cp.ProductId == detail.ProductId).FirstOrDefault();

                            bodega.Stock -= detail.Quantity;
                            db.Entry(bodega).State = EntityState.Modified;

                            db.OrderDetails.Add(orderDetail);
                            db.OrderDetailBkps.Remove(detail);


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
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "ComertialName", view.ClientId);
            ViewBag.RouteId = new SelectList(db.Routes, "RouteId", "RouteNumber", view.RouteId);
            view.Details = db.OrderDetailBkps.Where(pdb => pdb.User == User.Identity.Name).ToList();
            return View(view);
        }


        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CellarId = new SelectList(db.Cellars, "CellarId", "Description", order.CellarId);
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "ComertialName", order.ClientId);
            ViewBag.RouteId = new SelectList(db.Routes, "RouteId", "RouteNumber", order.RouteId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,Date,ClientId,CellarId,RouteId,StateId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CellarId = new SelectList(db.Cellars, "CellarId", "Description", order.CellarId);
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "ComertialName", order.ClientId);
            ViewBag.RouteId = new SelectList(db.Routes, "RouteId", "RouteNumber", order.RouteId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
