namespace DawProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Role_TableV2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Roles", "employee_id", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Roles", "employee_id");
        }
    }
}
