namespace virtualtri.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixuserlink : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Activities", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Activities", new[] { "ApplicationUser_Id" });
            AlterColumn("dbo.Activities", "ApplicationUser_Id", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Activities", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Activities", "ApplicationUser_Id");
            AddForeignKey("dbo.Activities", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
