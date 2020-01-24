namespace FinkiCommunity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Replymodelchanged : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Replies", "Title");
            DropColumn("dbo.Replies", "NumberOfReplies");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Replies", "NumberOfReplies", c => c.Int(nullable: false));
            AddColumn("dbo.Replies", "Title", c => c.String());
        }
    }
}
