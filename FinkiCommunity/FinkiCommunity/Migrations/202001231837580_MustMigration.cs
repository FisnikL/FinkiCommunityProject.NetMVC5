namespace FinkiCommunity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MustMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudyPrograms", "Group_Id", c => c.Int());
            CreateIndex("dbo.StudyPrograms", "Group_Id");
            AddForeignKey("dbo.StudyPrograms", "Group_Id", "dbo.Groups", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudyPrograms", "Group_Id", "dbo.Groups");
            DropIndex("dbo.StudyPrograms", new[] { "Group_Id" });
            DropColumn("dbo.StudyPrograms", "Group_Id");
        }
    }
}
