using AngryUsers.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace AngryUsers.Controllers
{
    public class ComplaintFlagsController : ApiController
    {
        private AngryUsersContext db = new AngryUsersContext();

        // GET: api/ComplaintFlags
        public IQueryable<ComplaintFlag> GetComplaintFlags()
        {
            return db.ComplaintFlags;
        }

        // POST: api/ComplaintFlags
        [ResponseType(typeof(ComplaintFlag))]
        public async Task<IHttpActionResult> PostComplaintFlag(ComplaintFlag flag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            flag.Status = "Pending";
            flag.CreatedAt = DateTime.Now;
            db.ComplaintFlags.Add(flag);
            await db.SaveChangesAsync();

            return Ok();
        }

        // PUT: api/ComplaintFlags/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutComplaintFlag(int id, ComplaintFlag flag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != flag.Id)
            {
                return BadRequest();
            }

            db.Entry(flag).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComplaintFlagExists(id))
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

        // DELETE: api/ComplaintFlags/5
        [ResponseType(typeof(ComplaintFlag))]
        public async Task<IHttpActionResult> DeleteComplaintFlag(int id)
        {
            ComplaintFlag flag = await db.ComplaintFlags.FindAsync(id);
            if (flag == null)
            {
                return NotFound();
            }

            db.ComplaintFlags.Remove(flag);
            await db.SaveChangesAsync();

            return Ok(flag);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ComplaintFlagExists(int id)
        {
            return db.ComplaintFlags.Count(e => e.Id == id) > 0;
        }
    }
}
