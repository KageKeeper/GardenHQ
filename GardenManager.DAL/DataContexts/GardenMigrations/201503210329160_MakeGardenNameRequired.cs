namespace GardenManager.DAL.DataContexts.GardenMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeGardenNameRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Gardens", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Gardens", "Name", c => c.String());
        }
    }
}
