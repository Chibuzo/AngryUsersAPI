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
    public class BlogCategoriesController : ApiController
    {
        private AngryUsersContext db = new AngryUsersContext();

        // GET: api/BlogCategories
        public IQueryable<BlogCategory> GetBlogCategories()
        {
            return db.BlogCategories;
        }

        // POST: api/BlogCategories
        [ResponseType(typeof(BlogCategory))]
        public async Task<IHttpActionResult> PostBlogCategory(BlogCategory category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BlogCategories.Add(category);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = category.Id }, category);
        }

        // GET: api/BlogCategories/getCategories
        [Route("api/BlogCategories/getCategories")]
        public IHttpActionResult GetCategories()
        {
            var blogCategory = db.BlogCategories.Select(p => new { id = p.Id, category = p.CategoryTitle, count = p.Posts.Count() });

            return Ok(blogCategory);
        }

        // GET: api/BlogCategories/getCategories
        [Route("api/BlogCategories/getPosts/{category}")]
        public IHttpActionResult GetCategoryPosts(string category)
        {
            var blogCategory = db.BlogCategories.Where(c => c.CategoryTitle == category).Select(p => 
                new {
                        category = p.CategoryTitle,
                        posts = p.Posts.Select(b => 
                            new {
                                    b.Title,
                                    b.Id,
                                    b.Article,
                                    b.CreatedAt
                            })
                });
            return Ok(blogCategory);
        }
    }
}
