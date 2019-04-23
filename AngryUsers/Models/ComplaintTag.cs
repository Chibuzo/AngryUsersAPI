using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace AngryUsers.Models
{
    public class ComplaintTag
    {
        public int Id { get; set; }
        [StringLength(60)]
        [Index(IsUnique = true)]
        public string TagTitle { get; set; }
        public int Count { get; set; }
    }
}