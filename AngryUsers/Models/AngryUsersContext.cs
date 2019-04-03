using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AngryUsers.Models
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class AngryUsersContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public AngryUsersContext() : base("name=AngryUsersContext")
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<AngryUsersContext>());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AngryUsersContext, AngryUsers.Migrations.Configuration>());
            Configuration.LazyLoadingEnabled = false;
        }

        public System.Data.Entity.DbSet<AngryUsers.Models.Complaint> Complaints { get; set; }

        public System.Data.Entity.DbSet<AngryUsers.Models.User> Users { get; set; }

        public System.Data.Entity.DbSet<AngryUsers.Models.Company> Companies { get; set; }

        public System.Data.Entity.DbSet<AngryUsers.Models.Comment> Comments { get; set; }

        public System.Data.Entity.DbSet<AngryUsers.Models.ComplaintFile> ComplaintFiles { get; set; }

        public System.Data.Entity.DbSet<AngryUsers.Models.BlogCategory> BlogCategories { get; set; }

        public System.Data.Entity.DbSet<AngryUsers.Models.BlogPost> BlogPosts { get; set; }

        public System.Data.Entity.DbSet<AngryUsers.Models.BlogPhoto> BlogPhotos { get; set; }

        public System.Data.Entity.DbSet<AngryUsers.Models.CompanyReview> CompanyReviews { get; set; }

<<<<<<< HEAD
        public System.Data.Entity.DbSet<AngryUsers.Models.ComplaintFlag> ComplaintFlags { get; set; }
=======
        public System.Data.Entity.DbSet<AngryUsers.Models.CompanyRequest> CompanyRequests { get; set; }
>>>>>>> company-side
    }
}
