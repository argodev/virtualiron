namespace virtualtri.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddistance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "TargetDistance", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "TargetDistance");
        }
    }
}
