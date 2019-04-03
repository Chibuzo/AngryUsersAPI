using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AngryUsers.Models
{
    public class ComplaintFlag
    {
        public int Id { get; set; }
        [Required]
        public string PostTitle { get; set; }
        [Required]
        public string PostType { get; set; }
        [Required]
        public int PostId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Comment { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}