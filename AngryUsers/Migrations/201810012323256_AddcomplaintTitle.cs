namespace AngryUsers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddcomplaintTitle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Complaints", "Title", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Complaints", "Title");
        }
    }
}
