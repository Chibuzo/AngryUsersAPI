using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AngryUsers.Models
{
    public class BlogCategory
    {
        public int Id { get; set; }

        [Required]
        public string CategoryTitle { get; set; }

        //public virtual ICollection<BlogPost> Posts { get; set; } 

        public DateTime CreatedAt { get; set; }
    }
}