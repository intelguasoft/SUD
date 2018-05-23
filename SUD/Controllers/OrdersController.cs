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
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Cellar).Include(o => o.Client).Include(o => o.Route);
            return View(orders.ToList());
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
            ViewBag.CellarId = new SelectList(db.Cellars, "CellarId", "Description");
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Document");
            ViewBag.RouteId = new SelectList(db.Routes, "RouteId", "Territory");
            return View();
        }

        // POST: Orders/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,OrderNumber,Date,ClientId,CellarId,RouteId,StateId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CellarId = new SelectList(db.Cellars, "CellarId", "Description", order.CellarId);
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Document", order.ClientId);
            ViewBag.RouteId = new SelectList(db.Routes, "RouteId", "Territory", order.RouteId);
            return View(order);
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
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Document", order.ClientId);
            ViewBag.RouteId = new SelectList(db.Routes, "RouteId", "Territory", order.RouteId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,OrderNumber,Date,ClientId,CellarId,RouteId,StateId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CellarId = new SelectList(db.Cellars, "CellarId", "Description", order.CellarId);
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Document", order.ClientId);
            ViewBag.RouteId = new SelectList(db.Routes, "RouteId", "Territory", order.RouteId);
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
