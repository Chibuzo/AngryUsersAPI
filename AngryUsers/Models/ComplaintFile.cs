using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace AngryUsers.Models
{
    public class ComplaintFile
    {
        public int Id { get; set; }
        [Required]
        public string Filename { get; set; }
        [Required]
        public int ComplaintId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}