namespace DawProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newRegister_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RegisterEs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        ConfirmPassword = c.String(nullable: false),
                        BornDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RegisterEs");
        }
    }
}
