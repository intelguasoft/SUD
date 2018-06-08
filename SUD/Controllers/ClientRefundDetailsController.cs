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
    [Authorize(Roles = "Gerente de ventas, Administrador")]

    public class ClientRefundDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ClientRefundDetails
        public ActionResult Index()
        {
            return View(db.ClientRefundDetails.ToList());
        }

        // GET: ClientRefundDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientRefundDetail clientRefundDetail = db.ClientRefundDetails.Find(id);
            if (clientRefundDetail == null)
            {
                return HttpNotFound();
            }
            return View(clientRefundDetail);
        }

        // GET: ClientRefundDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClientRefundDetails/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClientRefundDetailId,Description,price,Cantidad,DiscountRate,ClientRefundId,ProductId,KardexId")] ClientRefundDetail clientRefundDetail)
        {
            if (ModelState.IsValid)
            {
                db.ClientRefundDetails.Add(clientRefundDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(clientRefundDetail);
        }

        // GET: ClientRefundDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientRefundDetail clientRefundDetail = db.ClientRefundDetails.Find(id);
            if (clientRefundDetail == null)
            {
                return HttpNotFound();
            }
            return View(clientRefundDetail);
        }

        // POST: ClientRefundDetails/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClientRefundDetailId,Description,price,Cantidad,DiscountRate,ClientRefundId,ProductId,KardexId")] ClientRefundDetail clientRefundDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clientRefundDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(clientRefundDetail);
        }

        // GET: ClientRefundDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientRefundDetail clientRefundDetail = db.ClientRefundDetails.Find(id);
            if (clientRefundDetail == null)
            {
                return HttpNotFound();
            }
            return View(clientRefundDetail);
        }

        // POST: ClientRefundDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClientRefundDetail clientRefundDetail = db.ClientRefundDetails.Find(id);
            db.ClientRefundDetails.Remove(clientRefundDetail);
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
