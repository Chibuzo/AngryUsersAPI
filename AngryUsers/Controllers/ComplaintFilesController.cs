using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using AngryUsers.Models;
using AngryUsers.Services;

namespace AngryUsers.Controllers
{
    public class ComplaintFilesController : ApiController
    {
        private AngryUsersContext db = new AngryUsersContext();

        // POST: api/ComplaintFiles/uploadFiles
        [Route("api/ComplaintFiles/uploadFiles")]
        [HttpPost]
        public HttpResponseMessage UploadFiles()
        {
            var httpRequest = HttpContext.Current.Request;
            int complaintId = Int32.Parse(httpRequest.Params["ComplaintId"]);
            if (httpRequest.Files.Count > 0)
            {
                foreach (string filename in httpRequest.Files.Keys)
                {
                    var file = httpRequest.Files[filename];
                    var file_name = "cp_" + Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file.FileName);
                    var filePart = HttpContext.Current.Server.MapPath("~/ComplaintFiles/" + file_name);
                    file.SaveAs(filePart);

                    // insert into database
                    if (!ModelState.IsValid)
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest);
                    }

                    db.ComplaintFiles.Add(new ComplaintFile()
                    {
                        Filename = file_name,
                        ComplaintId = complaintId,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now                       
                    });
                    db.SaveChanges();
                }
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }


        [Route("api/ComplaintFiles/SaveUploadedFiles")]
        [HttpPost]
        public void SaveUploadedFiles(IEnumerable<ComplaintFile> files)
        {
            foreach (ComplaintFile file in files)
            {
                db.ComplaintFiles.Add(new ComplaintFile()
                {
                    Filename = file.Filename,
                    ComplaintId = file.ComplaintId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                });
            }
            db.SaveChanges();
        }

        [Route("api/ComplaintFiles/SignRequests")]
        [HttpPost]
        public IEnumerable<S3FileObject> SignRequests(IEnumerable<S3FileObject> FileObjs)
        {
            AWSServices aws = new AWSServices();
            IEnumerable<S3FileObject> SignedUrls = aws.SignUrl(FileObjs);
            return SignedUrls;
        }

        // GET: api/ComplaintFiles/5
        [ResponseType(typeof(ComplaintFile))]
        public async Task<IHttpActionResult> GetComplaintFile(int id)
        {
            ComplaintFile complaintFile = await db.ComplaintFiles.FindAsync(id);
            if (complaintFile == null)
            {
                return NotFound();
            }

            return Ok(complaintFile);
        }

        // PUT: api/ComplaintFiles/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutComplaintFile(int id, ComplaintFile complaintFile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != complaintFile.Id)
            {
                return BadRequest();
            }

            db.Entry(complaintFile).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComplaintFileExists(id))
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

        // POST: api/ComplaintFiles
        [ResponseType(typeof(ComplaintFile))]
        public async Task<IHttpActionResult> PostComplaintFile(ComplaintFile complaintFile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ComplaintFiles.Add(complaintFile);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = complaintFile.Id }, complaintFile);
        }

        // DELETE: api/ComplaintFiles/5
        [ResponseType(typeof(ComplaintFile))]
        public async Task<IHttpActionResult> DeleteComplaintFile(int id)
        {
            ComplaintFile complaintFile = await db.ComplaintFiles.FindAsync(id);
            if (complaintFile == null)
            {
                return NotFound();
            }

            db.ComplaintFiles.Remove(complaintFile);
            await db.SaveChangesAsync();

            return Ok(complaintFile);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ComplaintFileExists(int id)
        {
            return db.ComplaintFiles.Count(e => e.Id == id) > 0;
        }
    }
}