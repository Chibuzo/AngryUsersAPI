using AngryUsers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AngryUsers.Controllers
{
    public class ComplaintTagsController : ApiController
    {
        private AngryUsersContext db = new AngryUsersContext();

        // GET : api/ComplaintTags
        public IQueryable<ComplaintTag> getComplaintTags()
        {
            return db.ComplaintTags;
        }
    }
}
