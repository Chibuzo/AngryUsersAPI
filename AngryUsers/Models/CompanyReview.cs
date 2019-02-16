using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AngryUsers.Models
{
    public class CompanyReview
    {
        public int Id { get; set; }
        public bool Legit { get; set; }
        public bool GoodCustomerService { get; set; }
        [Required]
        public int CompanyId { get; set; }
        [Required]
        public int ComplaintId { get; set; }
    }
}