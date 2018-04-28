using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SUD.Models;

namespace SUD.Controllers.ApiControllers
{
    public class RolPermissionsController : ApiController
    {
        private ApplicationDbContext db;
        public RolPermissionsController()
        {
            db = new ApplicationDbContext();
        }

        // GET: api/RolPermissions
        public IQueryable<RolPermission> GetRolPermissions()
        {
            return db.RolPermissions;
        }

        // GET: api/RolPermissions/5
        [ResponseType(typeof(RolPermission))]
        public IHttpActionResult GetRolPermission(int id)
        {
            RolPermission rolPermission = db.RolPermissions.Find(id);
            if (rolPermission == null)
            {
                return NotFound();
            }

            return Ok(rolPermission);
        }

        // PUT: api/RolPermissions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRolPermission(int id, RolPermission rolPermission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rolPermission.IDPermission)
            {
                return BadRequest();
            }

            db.Entry(rolPermission).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RolPermissionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/RolPermissions
        [ResponseType(typeof(RolPermission))]
        public IHttpActionResult PostRolPermission(RolPermission rolPermission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RolPermissions.Add(rolPermission);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = rolPermission.IDPermission }, rolPermission);
        }

        // DELETE: api/RolPermissions/5
        [ResponseType(typeof(RolPermission))]
        public IHttpActionResult DeleteRolPermission(int id)
        {
            RolPermission rolPermission = db.RolPermissions.Find(id);
            if (rolPermission == null)
            {
                return NotFound();
            }

            db.RolPermissions.Remove(rolPermission);
            db.SaveChanges();

            return Ok(rolPermission);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RolPermissionExists(int id)
        {
            return db.RolPermissions.Count(e => e.IDPermission == id) > 0;
        }
    }
}