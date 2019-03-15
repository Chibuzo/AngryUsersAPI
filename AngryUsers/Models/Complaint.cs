using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AngryUsers.Models
{
    public class Complaint
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Issue { get; set; }
        [Required]
        public DateTime IssueDate { get; set; }
        public int ViewCount { get; set; }
        public Boolean FacebookShare { get; set; }
        public Boolean TwitterShare { get; set; }
        public Boolean Anonymous { get; set; }
        public DateTime CreatedAt { get; set; }

        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<ComplaintFile> ComplaintFiles { get; set; }

        public int UserId { get; set; } 
        public virtual User User { get; set; }
    }
}