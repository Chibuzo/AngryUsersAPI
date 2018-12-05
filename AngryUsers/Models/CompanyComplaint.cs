using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngryUsers.Models
{
    public class CompanyComplaint
    {
        public string Title { get; set; }
        
        public string Issue { get; set; }
        
        public DateTime IssueDate { get; set; }

        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public int UserId { get; set; }

    }
}