using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngryUsers.Models
{
    public class CompanyComplaint
    {
        public int Id { get; set; }

        public string Title { get; set; }
        
        public string Issue { get; set; }
        
        public DateTime IssueDate { get; set; }

        public Boolean FacebookShare { get; set; }
        public Boolean TwitterShare { get; set; }
        public Boolean Anonymous { get; set; }
        public Boolean Notify { get; set; }
        public DateTime CreatedAt { get; set; }

        public int CompanyId { get; set; }

        public string CompanyName { get; set; }
        public string Tags { get; set; }

        public int UserId { get; set; }

    }
}