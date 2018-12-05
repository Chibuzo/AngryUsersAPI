﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AngryUsers.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        public string Fullname { get; set; }

        public virtual ICollection<Complaint> Complaints { get; set; }
    }
}