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
    public class CompanyReviewsController : ApiController
    {
        private AngryUsersContext db = new AngryUsersContext();

        // POST: api/CompanyReviews
        [ResponseType(typeof(CompanyReview))]
        public async Task<IHttpActionResult> PostCompanyReview(CompanyReview Review)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CompanyReviews.Add(Review);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = Review.Id }, Review);
        }
    }
}
