namespace FinkiCommunity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changegroupmodel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StudyPrograms", "Group_Id", "dbo.Groups");
            DropIndex("dbo.StudyPrograms", new[] { "Group_Id" });
            DropColumn("dbo.StudyPrograms", "Group_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StudyPrograms", "Group_Id", c => c.Int());
            CreateIndex("dbo.StudyPrograms", "Group_Id");
            AddForeignKey("dbo.StudyPrograms", "Group_Id", "dbo.Groups", "Id");
        }
    }
}
