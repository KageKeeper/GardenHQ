namespace GardenManager.DAL.DataContexts.GardenMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedWidthAndLengthOfBedTypeFromDecimalToInt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Beds", "Width", c => c.Int(nullable: false));
            AlterColumn("dbo.Beds", "Length", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Beds", "Length", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Beds", "Width", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
