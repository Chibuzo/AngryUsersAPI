using System;
using System.ComponentModel.DataAnnotations;

namespace AngryUsers.Models
{
    public class CompanyRequest
    {
        public int Id { get; set; }
        [Required]
        public string ContactPerson { get; set; }
        [Required]
        public string ContactPhone { get; set; }
        [Required]
        public string ContactEmail { get; set; }
        public string CompanyName { get; set; }
        [Required]
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}