using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AngryUsers.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        public string Body { get; set; }
        public DateTime DatePosted { get; set; }

        [Required]
        public int UserId { get; set; }
        [Required]
        public int ComplaintId { get; set; }
    }
}