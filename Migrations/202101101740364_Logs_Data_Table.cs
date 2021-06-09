namespace DawProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Logs_Data_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        text_log = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Logs");
        }
    }
}
