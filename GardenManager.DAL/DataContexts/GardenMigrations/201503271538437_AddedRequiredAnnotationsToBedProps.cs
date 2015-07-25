namespace GardenManager.DAL.DataContexts.GardenMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRequiredAnnotationsToBedProps : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Beds", "Alias", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Beds", "Alias", c => c.String());
        }
    }
}
