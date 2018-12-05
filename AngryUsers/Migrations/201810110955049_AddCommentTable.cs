namespace AngryUsers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCommentTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Body = c.String(nullable: false),
                        DatePosted = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        ComplaintId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Complaints", t => t.ComplaintId, cascadeDelete: true)
                .Index(t => t.ComplaintId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "ComplaintId", "dbo.Complaints");
            DropIndex("dbo.Comments", new[] { "ComplaintId" });
            DropTable("dbo.Comments");
        }
    }
}
