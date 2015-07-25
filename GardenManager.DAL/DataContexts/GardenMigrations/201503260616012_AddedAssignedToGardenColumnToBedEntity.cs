namespace GardenManager.DAL.DataContexts.GardenMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAssignedToGardenColumnToBedEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Beds", "AssignedToGarden", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Beds", "AssignedToGarden");
        }
    }
}
