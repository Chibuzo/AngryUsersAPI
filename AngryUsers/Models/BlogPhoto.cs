using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AngryUsers.Models
{
    public class BlogPhoto
    {
        public int Id { get; set; }
        [Required]
        public string PhotoName { get; set; }
        public int BlogPostId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}