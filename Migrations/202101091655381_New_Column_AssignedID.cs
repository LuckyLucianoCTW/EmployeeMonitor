namespace DawProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New_Column_AssignedID : DbMigration
    {
        public override void Up()
        { 
            DropPrimaryKey("dbo.ProjectsAssigneds", new[] { "ProjectId" });
            DropColumn("dbo.ProjectsAssigneds", "ProjectId");
            AddColumn("dbo.ProjectsAssigneds", "AssignID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.ProjectsAssigneds", "ProjectId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.ProjectsAssigneds", "AssignID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ProjectsAssigneds");
            AlterColumn("dbo.ProjectsAssigneds", "ProjectId", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.ProjectsAssigneds", "AssignID");
            AddPrimaryKey("dbo.ProjectsAssigneds", "ProjectId");
        }
    }
}
