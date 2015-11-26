namespace EF6Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SportActivities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SportComplex_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SportComplexes", t => t.SportComplex_Id)
                .Index(t => t.SportComplex_Id);
            
            CreateTable(
                "dbo.SportComplexes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SportActivities", "SportComplex_Id", "dbo.SportComplexes");
            DropIndex("dbo.SportActivities", new[] { "SportComplex_Id" });
            DropTable("dbo.SportComplexes");
            DropTable("dbo.SportActivities");
        }
    }
}
