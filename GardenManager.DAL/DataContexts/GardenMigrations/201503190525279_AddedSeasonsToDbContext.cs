namespace GardenManager.DAL.DataContexts.GardenMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSeasonsToDbContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Seasons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SeasonYear = c.String(),
                        GardenId = c.Int(nullable: false),
                        Harvest_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Harvests", t => t.Harvest_Id)
                .Index(t => t.Harvest_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Seasons", "Harvest_Id", "dbo.Harvests");
            DropIndex("dbo.Seasons", new[] { "Harvest_Id" });
            DropTable("dbo.Seasons");
        }
    }
}
