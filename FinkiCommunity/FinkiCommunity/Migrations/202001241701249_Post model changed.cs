namespace FinkiCommunity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Postmodelchanged : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Replies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        Created = c.DateTime(nullable: false),
                        NumberOfLikes = c.Int(nullable: false),
                        NumberOfReplies = c.Int(nullable: false),
                        ToPost_Id = c.Int(),
                        UserOwner_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.ToPost_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserOwner_Id)
                .Index(t => t.ToPost_Id)
                .Index(t => t.UserOwner_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Replies", "UserOwner_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Replies", "ToPost_Id", "dbo.Posts");
            DropIndex("dbo.Replies", new[] { "UserOwner_Id" });
            DropIndex("dbo.Replies", new[] { "ToPost_Id" });
            DropTable("dbo.Replies");
        }
    }
}
