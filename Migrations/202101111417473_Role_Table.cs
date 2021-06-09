namespace DawProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Role_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        e_id = c.Int(nullable: false, identity: true),
                        role_name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.e_id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Roles");
        }
    }
}
