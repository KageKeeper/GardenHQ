namespace GardenManager.Web.DataContexts.GardenMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Gardens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Zone = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Harvests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        GardenId = c.Int(nullable: false),
                        SeasonId = c.Int(nullable: false),
                        SeedId = c.Int(nullable: false),
                        YieldInWeight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        YieldInProduce = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Seeds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BedId = c.Int(nullable: false),
                        Name = c.String(),
                        Rating = c.Int(nullable: false),
                        Comments = c.String(),
                        Family = c.Int(nullable: false),
                        SeedsPerPackage = c.Int(nullable: false),
                        SeedOrderLocation = c.String(),
                        HaveOnHand = c.Boolean(nullable: false),
                        GrowThisSeason = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Seeds");
            DropTable("dbo.Harvests");
            DropTable("dbo.Gardens");
        }
    }
}
