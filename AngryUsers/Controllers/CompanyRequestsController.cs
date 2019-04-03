using AngryUsers.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace AngryUsers.Controllers
{
    public class CompanyRequestsController : ApiController
    {
        private AngryUsersContext db = new AngryUsersContext();

        // GET: api/CompanyRequests
        public IQueryable<CompanyRequest> GetCompanyRequests()
        {
            return db.CompanyRequests.Include(c => c.Company).OrderByDescending(d => d.CreatedAt);
        }

        // POST: api/CompanyRequests
        [ResponseType(typeof(CompanyRequest))]
        public async Task<IHttpActionResult> PostCompanyRequest(CompanyRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (request.CompanyId == 0)
            {
                // add new company
                Company company = new Company
                {
                    CompanyName = request.CompanyName
                };
                db.Companies.Add(company);
                await db.SaveChangesAsync();
                request.CompanyId = company.Id;
            }

            request.CreatedAt = DateTime.Now;
            request.Status = "Pending";
            db.CompanyRequests.Add(request);
            await db.SaveChangesAsync();

            return Json(new { Success = true });
        }

        // PUT: api/companyRequests/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCompanyRequest(int id, CompanyRequest Request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Request.Id)
            {
                return BadRequest();
            }

            db.Entry(Request).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyRequestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Json(new { Success = true });
        }

        // DELETE: api/BlogPosts/5
        [ResponseType(typeof(BlogPost))]
        public async Task<IHttpActionResult> DeleteBlogPost(int id)
        {
            BlogPost BlogPost = await db.BlogPosts.FindAsync(id);
            if (BlogPost == null)
            {
                return NotFound();
            }

            db.BlogPosts.Remove(BlogPost);
            await db.SaveChangesAsync();

            return Ok(BlogPost);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CompanyRequestExists(int id)
        {
            return db.CompanyRequests.Count(e => e.Id == id) > 0;
        }
    }
}
