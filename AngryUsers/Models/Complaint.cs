using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AngryUsers.Models
{
    public class Complaint : EntityBase
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Issue { get; set; }
        [Required]
        public DateTime IssueDate { get; set; }

        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public int UserId { get; set; } 
        public virtual User User { get; set; }
    }
}