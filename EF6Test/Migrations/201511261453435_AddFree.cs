namespace EF6Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFree : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SportActivities", "Free", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SportActivities", "Free");
        }
    }
}
