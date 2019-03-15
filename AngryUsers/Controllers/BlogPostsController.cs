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
    public class BlogPostsController : ApiController
    {
        private AngryUsersContext db = new AngryUsersContext();

        // GET: api/BlogPosts
        public IQueryable<BlogPost> GetBlogPosts()
        {
            return db.BlogPosts.Include(c => c.Category).Include(p => p.Photos).OrderByDescending(d => d.CreatedAt);
        }

        // GET: api/BlogPosts/FetchRecent
        [Route("api/BlogPosts/FetchRecent")]
        public IHttpActionResult GetRecentBlogPosts()
        {
            var recentPosts = db.BlogPosts.Select(p => new { id = p.Id, title = p.Title, createdAt = p.CreatedAt }).OrderByDescending(p => p.createdAt).Take(8);
            return Json(new { recentPosts });
        }

        // GET: api/BlogPosts/5
        [ResponseType(typeof(BlogPost))]
        public async Task<IHttpActionResult> GetBlogPost(int id)
        {
            BlogPost BlogPost = await db.BlogPosts.Include(c => c.Category).Include(p => p.Photos).FirstOrDefaultAsync(i => i.Id == id);
            if (BlogPost == null)
            {
                return NotFound();
            }

            return Ok(BlogPost);
        }

        // PUT: api/BlogPosts/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBlogPost(int id, BlogPost BlogPost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != BlogPost.Id)
            {
                return BadRequest();
            }

            db.Entry(BlogPost).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogPostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Json(new { Success = true, BlogPost.Id, BlogPost.Title });
        }

        // POST: api/BlogPosts
        [ResponseType(typeof(BlogPost))]
        public async Task<IHttpActionResult> PostBlogPost(BlogPost blogPost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            blogPost.CreatedAt = DateTime.Now;
            db.BlogPosts.Add(blogPost);
            await db.SaveChangesAsync();

            return Json(new { Success = true, blogPost.Id, blogPost.Title });
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

        private bool BlogPostExists(int id)
        {
            return db.BlogPosts.Count(e => e.Id == id) > 0;
        }
    }
}
