using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Description;
using AngryUsers.Models;

namespace AngryUsers.Controllers
{
    public class ComplaintsController : ApiController
    {
        private AngryUsersContext db = new AngryUsersContext();

        // GET: api/Complaints
        public IQueryable<Complaint> GetComplaints()
        {
            return db.Complaints
                .Include(coy => coy.Company)
                .Include(com => com.Comments.Select(u => u.User))
                .Include(u => u.User)
                .Include(f => f.ComplaintFiles)
                .OrderByDescending(c => c.CreatedAt);
        }

        // GET: api/Complaints/5
        [ResponseType(typeof(Complaint))]
        public async Task<IHttpActionResult> GetComplaint(int id)
        {
            Complaint complaint = await db.Complaints
                .Include(coy => coy.Company)
                .Include(c => c.Comments.Select(u => u.User))
                .Include(f => f.ComplaintFiles)
                .Include(u => u.User)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (complaint == null || complaint == default(Complaint))
            {
                return NotFound();
            }

            // increment view count
            complaint.ViewCount++;
            db.Entry(complaint).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return Ok(complaint);
        }

        // PUT: api/Complaints/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutComplaint(int id, Complaint complaint)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != complaint.Id)
            {
                return BadRequest();
            }

            db.Entry(complaint).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComplaintExists(id))
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

        // POST: api/Complaints
        [ResponseType(typeof(Complaint))]
        public async Task<IHttpActionResult> PostComplaint(Complaint complaint)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Complaints.Add(complaint);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = complaint.Id }, complaint);
        }

        // POST: api/complaints/save
        [Route("api/Complaints/save")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveComplaint(CompanyComplaint complaint)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (complaint.CompanyId == 0)
            {
                // add new company
                Company company = new Company
                {
                    CompanyName = complaint.CompanyName
                };
                db.Companies.Add(company);
                await db.SaveChangesAsync();
                complaint.CompanyId = company.Id;
            }

            Complaint newComplaint = new Complaint()
            {
                Title = complaint.Title,
                Issue = complaint.Issue,
                IssueDate = complaint.IssueDate,
                CompanyId = complaint.CompanyId,
                FacebookShare = complaint.FacebookShare,
                TwitterShare = complaint.TwitterShare,
                Anonymous = complaint.Anonymous,
                Notify = complaint.Notify,
                UserId = complaint.UserId,
                CreatedAt = DateTime.Now
            };
            db.Complaints.Add(newComplaint);
            await db.SaveChangesAsync();

            return Json(new { Success = true, newComplaint.Id });
        }

        // DELETE: api/Complaints/5
        [ResponseType(typeof(Complaint))]
        public async Task<IHttpActionResult> DeleteComplaint(int id)
        {
            Complaint complaint = await db.Complaints.FindAsync(id);
            if (complaint == null)
            {
                return NotFound();
            }

            db.Complaints.Remove(complaint);
            await db.SaveChangesAsync();

            return Ok(complaint);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ComplaintExists(int id)
        {
            return db.Complaints.Count(e => e.Id == id) > 0;
        }
    }
}