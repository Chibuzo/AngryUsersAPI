using AngryUsers.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace AngryUsers.Services
{
    public class NotifyEmail
    {
        private AngryUsersContext db = new AngryUsersContext();

        public NotifyEmail()
        {

        }

        public void SendComplaintNotificationEmail(int complaintId)
        {
            string angryUserEmail;
            List<string> commentersEmails = new List<string>();

            Complaint complaint = db.Complaints
                .Include(u => u.User)
                .Include(c => c.Comments.Select(u => u.User))
                .FirstOrDefault(c => c.Id == complaintId);

            if (complaint.Notify == true)
            {
                angryUserEmail = complaint.User.Email;
            }

            foreach (Comment comment in complaint.Comments)
            {
                if (comment.Notify == true)
                {
                    commentersEmails.Add(comment.User.Email);
                }
            }
        }

        private string PopulateBody(string user, string complaint_title, string url)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplates/notify.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{user}", user);
            body = body.Replace("{complaint_title}", complaint_title);
            body = body.Replace("{url}", url);
            return body;
        }
    }
}