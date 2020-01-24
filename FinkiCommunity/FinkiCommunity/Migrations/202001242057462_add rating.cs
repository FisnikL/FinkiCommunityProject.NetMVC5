namespace FinkiCommunity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addrating : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Rating", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Rating");
        }
    }
}
