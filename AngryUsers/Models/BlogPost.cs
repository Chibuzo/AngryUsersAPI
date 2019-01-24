using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace AngryUsers.Models
{
    public class BlogPost
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Article { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public virtual BlogCategory Category { get; set; }

        public virtual ICollection<BlogPhoto> Photos { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}