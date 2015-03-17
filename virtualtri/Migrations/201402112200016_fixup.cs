namespace virtualtri.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActivityType = c.Int(nullable: false),
                        Distance = c.Single(nullable: false),
                        ClickDateTime = c.DateTime(nullable: false),
                        Participant_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Participants", t => t.Participant_Id)
                .Index(t => t.Participant_Id);
            
            CreateTable(
                "dbo.Participants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmailAddress = c.String(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 255),
                        LastName = c.String(nullable: false, maxLength: 255),
                        SignedUp = c.DateTime(nullable: false),
                        DateCompleted = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "EmailAddress", c => c.String());
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String(maxLength: 255));
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Activities", "Participant_Id", "dbo.Participants");
            DropIndex("dbo.Activities", new[] { "Participant_Id" });
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
            DropColumn("dbo.AspNetUsers", "EmailAddress");
            DropTable("dbo.Participants");
            DropTable("dbo.Activities");
        }
    }
}
