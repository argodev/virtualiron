namespace virtualtri.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MajorChanges01 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Activities", "Participant_Id", "dbo.Participants");
            DropIndex("dbo.Activities", new[] { "Participant_Id" });
            AddColumn("dbo.Activities", "ActivityDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Activities", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "DateStarted", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "DateCompleted", c => c.DateTime());
            CreateIndex("dbo.Activities", "ApplicationUser_Id");
            AddForeignKey("dbo.Activities", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Activities", "ClickDateTime");
            DropColumn("dbo.Activities", "Participant_Id");
            DropTable("dbo.Participants");
        }
        
        public override void Down()
        {
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
            
            AddColumn("dbo.Activities", "Participant_Id", c => c.Int());
            AddColumn("dbo.Activities", "ClickDateTime", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.Activities", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Activities", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.AspNetUsers", "DateCompleted");
            DropColumn("dbo.AspNetUsers", "DateStarted");
            DropColumn("dbo.Activities", "ApplicationUser_Id");
            DropColumn("dbo.Activities", "ActivityDateTime");
            CreateIndex("dbo.Activities", "Participant_Id");
            AddForeignKey("dbo.Activities", "Participant_Id", "dbo.Participants", "Id");
        }
    }
}
