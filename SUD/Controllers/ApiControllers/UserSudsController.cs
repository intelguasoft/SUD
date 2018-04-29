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
    public class UserSudsController : ApiController
    {
        private ApplicationDbContext db;

        public UserSudsController()
        {
            db = new ApplicationDbContext();
        }

        // GET: api/UserSuds
        public IQueryable<UserSud> GetUserSuds()
        {
            return db.UserSuds;
        }

        // GET: api/UserSuds/5
        [ResponseType(typeof(UserSud))]
        public IHttpActionResult GetUserSud(int id)
        {
            UserSud userSud = db.UserSuds.Find(id);
            if (userSud == null)
            {
                return NotFound();
            }

            return Ok(userSud);
        }

        // PUT: api/UserSuds/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserSud(int id, UserSud userSud)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userSud.UserSudId)
            {
                return BadRequest();
            }

            db.Entry(userSud).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserSudExists(id))
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

        // POST: api/UserSuds
        [ResponseType(typeof(UserSud))]
        public IHttpActionResult PostUserSud(UserSud userSud)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserSuds.Add(userSud);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = userSud.UserSudId }, userSud);
        }

        // DELETE: api/UserSuds/5
        [ResponseType(typeof(UserSud))]
        public IHttpActionResult DeleteUserSud(int id)
        {
            UserSud userSud = db.UserSuds.Find(id);
            if (userSud == null)
            {
                return NotFound();
            }

            db.UserSuds.Remove(userSud);
            db.SaveChanges();

            return Ok(userSud);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserSudExists(int id)
        {
            return db.UserSuds.Count(e => e.UserSudId == id) > 0;
        }
    }
}