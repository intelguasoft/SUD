using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SUD.Generic;
using SUD.Models;

namespace SUD.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class UserSudsController : Controller
    {
        private ApplicationDbContext db;

        public UserSudsController()
        {
            db = new ApplicationDbContext();
        }

        // GET: UserSuds
        public ActionResult Index()
        {
            var userSuds = db.UserSuds.Include(u => u.Rol);
            return View(userSuds.ToList());
        }

        // GET: UserSuds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSud userSud = db.UserSuds.Find(id);
            if (userSud == null)
            {
                return HttpNotFound();
            }
            return View(userSud);
        }

        // GET: UserSuds/Create
        public ActionResult Create()
        {
            ViewBag.RolId = new SelectList(db.Rols, "RolId", "Description");
            return View();
        }

        // POST: UserSuds/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserSudId,Name,LastName,Password,ModificationDatePassword,RolId,Email, Status, Image, FotografiaFile")] UserSud userSud)
        {
            if (ModelState.IsValid)
            {
                db.UserSuds.Add(userSud);
                db.SaveChanges();

                if (userSud != null)
                {
                    var rol = db.Rols.Find(userSud.RolId);
                    UsersHelper.CreateUserASP(userSud.Email, rol.Description, userSud.Password);
                }

                if (userSud.FotografiaFile != null)
                {
                    var folder = "~/Uploads/Usuarios";
                    var response = FilesHelper.UploadPhoto(userSud.FotografiaFile, folder, string.Format("{0}.jpg", userSud.UserSudId));
                    if (response)
                    {
                        var pic = string.Format("{0}/{1}.jpg", folder, userSud.UserSudId);
                        userSud.Image = pic;

                        db.Entry(userSud).State = EntityState.Modified;
                        db.SaveChanges();


                    }
                }
                return RedirectToAction("Index");

            }

            ViewBag.RolId = new SelectList(db.Rols, "RolId", "Description", userSud.RolId);
            return View(userSud);
        }

        // GET: UserSuds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSud userSud = db.UserSuds.Find(id);
            if (userSud == null)
            {
                return HttpNotFound();
            }
            ViewBag.RolId = new SelectList(db.Rols, "RolId", "Description", userSud.RolId);
            return View(userSud);
        }

        // POST: UserSuds/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserSudId,Name,LastName,Password,ModificationDatePassword,RolId,Email, Status, Image, FotografiaFile")] UserSud userSud)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userSud).State = EntityState.Modified;
                db.SaveChanges();

                var db2 = new ApplicationDbContext();
                var currentUser = db2.UserSuds.Find(userSud.UserSudId);
                if (currentUser.Email != userSud.Email)
                {
                    UsersHelper.UpdateUsername(currentUser.Email, userSud.Email);
                }

                if (userSud.FotografiaFile != null)
                {
                    var folder = "~/Uploads/Usuarios";
                    var response = FilesHelper.UploadPhoto(userSud.FotografiaFile, folder, string.Format("{0}.jpg", userSud.UserSudId));
                    if (response)
                    {
                        var pic = string.Format("{0}/{1}.jpg", folder, userSud.UserSudId);
                        userSud.Image = pic;

                        db.Entry(userSud).State = EntityState.Modified;
                        db.SaveChanges();


                    }
                }
                db2.Dispose();
                return RedirectToAction("Index");
            }

            ViewBag.RolId = new SelectList(db.Rols, "RolId", "Description", userSud.RolId);
            return View(userSud);
        }

        // GET: UserSuds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSud userSud = db.UserSuds.Find(id);
            if (userSud == null)
            {
                return HttpNotFound();
            }
            return View(userSud);
        }

        // POST: UserSuds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserSud userSud = db.UserSuds.Find(id);
            db.UserSuds.Remove(userSud);
            db.SaveChanges();
            UsersHelper.DeleteUserSud(userSud.Email);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult getUserSuds()
        {
            // TODO Falta que hacer el filtrado del lado del servidor.
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            //var filter = Request.Form.GetValues("filter").FirstOrDefault();
            //Find Order Column
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();


            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            using (ApplicationDbContext _context = new ApplicationDbContext())
            {
                _context.Configuration.ProxyCreationEnabled = false; // esto es necesario si nuestra tabla esta relacionado y por cosiguiente tiene claves foraneas

                var v = (from a in _context.UserSuds.Include("Rol") select a);

                //SORT
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    v = v.OrderBy(sortColumn + " " + sortColumnDir);
                }

                recordsTotal = v.Count();
                var data = v.Skip(skip).Take(pageSize).ToList();
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
