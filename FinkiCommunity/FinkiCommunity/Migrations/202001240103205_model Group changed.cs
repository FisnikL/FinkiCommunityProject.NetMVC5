namespace FinkiCommunity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modelGroupchanged : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Groups", "CoursePictureUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Groups", "CoursePictureUrl");
        }
    }
}
