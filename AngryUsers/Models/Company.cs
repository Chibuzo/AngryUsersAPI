using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AngryUsers.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Complaint> Complaints { get; set; }
    }
}