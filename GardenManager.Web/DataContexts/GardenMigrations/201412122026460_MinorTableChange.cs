namespace GardenManager.Web.DataContexts.GardenMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MinorTableChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SeedFamilies", "MoreInfo", c => c.String());
            AddColumn("dbo.SeedFamilies", "Examples", c => c.String());
            DropColumn("dbo.SeedFamilies", "Example");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SeedFamilies", "Example", c => c.String());
            DropColumn("dbo.SeedFamilies", "Examples");
            DropColumn("dbo.SeedFamilies", "MoreInfo");
        }
    }
}
