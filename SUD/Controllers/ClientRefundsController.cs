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
    public class ClientRefundsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ClientRefunds
        public ActionResult Index()
        {
            var clientRefunds = db.ClientRefunds.Include(c => c.Sale);
            return View(clientRefunds.ToList());
        }

        // GET: ClientRefunds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientRefund clientRefund = db.ClientRefunds.Find(id);
            if (clientRefund == null)
            {
                return HttpNotFound();
            }
            return View(clientRefund);
        }

        // GET: ClientRefunds/Create
        public ActionResult Create()
        {
            ViewBag.SaleId = new SelectList(db.Sales, "SaleId", "SaleId");
            return View();
        }

        // POST: ClientRefunds/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClientRefundId,Date,SaleId")] ClientRefund clientRefund)
        {
            if (ModelState.IsValid)
            {
                db.ClientRefunds.Add(clientRefund);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SaleId = new SelectList(db.Sales, "SaleId", "SaleId", clientRefund.SaleId);
            return View(clientRefund);
        }

        // GET: ClientRefunds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientRefund clientRefund = db.ClientRefunds.Find(id);
            if (clientRefund == null)
            {
                return HttpNotFound();
            }
            ViewBag.SaleId = new SelectList(db.Sales, "SaleId", "SaleId", clientRefund.SaleId);
            return View(clientRefund);
        }

        // POST: ClientRefunds/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClientRefundId,Date,SaleId")] ClientRefund clientRefund)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clientRefund).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SaleId = new SelectList(db.Sales, "SaleId", "SaleId", clientRefund.SaleId);
            return View(clientRefund);
        }

        // GET: ClientRefunds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientRefund clientRefund = db.ClientRefunds.Find(id);
            if (clientRefund == null)
            {
                return HttpNotFound();
            }
            return View(clientRefund);
        }

        // POST: ClientRefunds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClientRefund clientRefund = db.ClientRefunds.Find(id);
            db.ClientRefunds.Remove(clientRefund);
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
