using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AngryUsers.Models;

namespace AngryUsers.Controllers
{
    public class BlogPhotosController : ApiController
    {
        private AngryUsersContext db = new AngryUsersContext();

        // POST: api/BlogPhotos/uploadPhotos
        [Route("api/BlogPhotos/uploadPhotos")]
        [HttpPost]
        public HttpResponseMessage UploadFiles()
        {
            var httpRequest = HttpContext.Current.Request;
            int postId = Int32.Parse(httpRequest.Params["PostId"]);
            if (httpRequest.Files.Count > 0)
            {
                foreach (string filename in httpRequest.Files.Keys)
                {
                    var file = httpRequest.Files[filename];
                    var file_name = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file.FileName);
                    var filePart = HttpContext.Current.Server.MapPath("~/BlogPhotos/" + file_name);
                    file.SaveAs(filePart);

                    // insert into database
                    if (!ModelState.IsValid)
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest);
                    }

                    db.BlogPhotos.Add(new BlogPhoto()
                    {
                        PhotoName = file_name,
                        BlogPostId = postId,
                        CreatedAt = DateTime.Now,
                    });
                    db.SaveChanges();
                }
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
