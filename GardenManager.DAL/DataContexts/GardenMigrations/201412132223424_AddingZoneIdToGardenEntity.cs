namespace GardenManager.DAL.DataContexts.GardenMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingZoneIdToGardenEntity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Gardens", "Zone_Id", "dbo.PlantHardinessZones");
            DropIndex("dbo.Gardens", new[] { "Zone_Id" });
            RenameColumn(table: "dbo.Gardens", name: "Zone_Id", newName: "ZoneId");
            AlterColumn("dbo.Gardens", "ZoneId", c => c.Int(nullable: false));
            CreateIndex("dbo.Gardens", "ZoneId");
            AddForeignKey("dbo.Gardens", "ZoneId", "dbo.PlantHardinessZones", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Gardens", "ZoneId", "dbo.PlantHardinessZones");
            DropIndex("dbo.Gardens", new[] { "ZoneId" });
            AlterColumn("dbo.Gardens", "ZoneId", c => c.Int());
            RenameColumn(table: "dbo.Gardens", name: "ZoneId", newName: "Zone_Id");
            CreateIndex("dbo.Gardens", "Zone_Id");
            AddForeignKey("dbo.Gardens", "Zone_Id", "dbo.PlantHardinessZones", "Id");
        }
    }
}
