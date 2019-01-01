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

namespace AngryUsers.Controllers
{
    public class ComplaintFilesController : ApiController
    {
        private AngryUsersContext db = new AngryUsersContext();

        // GET: api/ComplaintFiles
        public IQueryable<ComplaintFile> GetComplaintFiles()
        {
            return db.ComplaintFiles;
        }

        // POST: api/ComplaintFiles/uploadFiles
        [Route("api/ComplaintFiles/uploadFiles")]
        [HttpPost]
        public HttpResponseMessage UploadFiles()
        {
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                foreach (string filename in httpRequest.Files.Keys)
                {
                    var file = httpRequest.Files[filename];
                    var file_name = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file.FileName);
                    var filePart = HttpContext.Current.Server.MapPath("~/ComplaintFiles/cp_" + file_name);
                    file.SaveAs(filePart);
                }
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
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