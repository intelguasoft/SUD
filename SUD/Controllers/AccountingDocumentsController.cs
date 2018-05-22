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
    public class AccountingDocumentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AccountingDocuments
        public ActionResult Index()
        {
            return View(db.AccountingDocuments.ToList());
        }

        // GET: AccountingDocuments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountingDocument accountingDocument = db.AccountingDocuments.Find(id);
            if (accountingDocument == null)
            {
                return HttpNotFound();
            }
            return View(accountingDocument);
        }

        // GET: AccountingDocuments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountingDocuments/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccountingDocumentId,Document,Description,SerialNumber,InitialNumber,FinalNumber,ResolutionNumber,ExpirationDate")] AccountingDocument accountingDocument)
        {
            if (ModelState.IsValid)
            {
                db.AccountingDocuments.Add(accountingDocument);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(accountingDocument);
        }

        // GET: AccountingDocuments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountingDocument accountingDocument = db.AccountingDocuments.Find(id);
            if (accountingDocument == null)
            {
                return HttpNotFound();
            }
            return View(accountingDocument);
        }

        // POST: AccountingDocuments/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccountingDocumentId,Document,Description,SerialNumber,InitialNumber,FinalNumber,ResolutionNumber,ExpirationDate")] AccountingDocument accountingDocument)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accountingDocument).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accountingDocument);
        }

        // GET: AccountingDocuments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountingDocument accountingDocument = db.AccountingDocuments.Find(id);
            if (accountingDocument == null)
            {
                return HttpNotFound();
            }
            return View(accountingDocument);
        }

        // POST: AccountingDocuments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccountingDocument accountingDocument = db.AccountingDocuments.Find(id);
            db.AccountingDocuments.Remove(accountingDocument);
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
