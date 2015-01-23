namespace GardenManager.Web.DataContexts.GardenMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnumsToTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PlantHardinessZones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Zone = c.String(),
                        TempRange = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SeedFamilies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Example = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Gardens", "Zone_Id", c => c.Int());
            AddColumn("dbo.Seeds", "Family_Id", c => c.Int());
            CreateIndex("dbo.Gardens", "Zone_Id");
            CreateIndex("dbo.Seeds", "Family_Id");
            AddForeignKey("dbo.Gardens", "Zone_Id", "dbo.PlantHardinessZones", "Id");
            AddForeignKey("dbo.Seeds", "Family_Id", "dbo.SeedFamilies", "Id");
            DropColumn("dbo.Gardens", "Zone");
            DropColumn("dbo.Seeds", "Family");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Seeds", "Family", c => c.Int(nullable: false));
            AddColumn("dbo.Gardens", "Zone", c => c.Int(nullable: false));
            DropForeignKey("dbo.Seeds", "Family_Id", "dbo.SeedFamilies");
            DropForeignKey("dbo.Gardens", "Zone_Id", "dbo.PlantHardinessZones");
            DropIndex("dbo.Seeds", new[] { "Family_Id" });
            DropIndex("dbo.Gardens", new[] { "Zone_Id" });
            DropColumn("dbo.Seeds", "Family_Id");
            DropColumn("dbo.Gardens", "Zone_Id");
            DropTable("dbo.SeedFamilies");
            DropTable("dbo.PlantHardinessZones");
        }
    }
}
