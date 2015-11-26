namespace EF6Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequiredName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SportActivities", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SportActivities", "Name", c => c.String());
        }
    }
}
