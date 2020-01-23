namespace FinkiCommunity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Somechangesinmodels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudyPrograms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Groups", "Semester", c => c.Int(nullable: false));
            AddColumn("dbo.Groups", "StudyYear", c => c.Int(nullable: false));
            AddColumn("dbo.Groups", "CourseType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Groups", "CourseType");
            DropColumn("dbo.Groups", "StudyYear");
            DropColumn("dbo.Groups", "Semester");
            DropTable("dbo.StudyPrograms");
        }
    }
}
