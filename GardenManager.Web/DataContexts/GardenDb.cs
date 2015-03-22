using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using GardenManager.Entities;

namespace GardenManager.Web.DataContexts
{
    public class GardenDb : DbContext
    {
        public GardenDb()
            : base("DefaultConnection")
        {
        }

        public DbSet<Garden> Gardens { get; set; }
        public DbSet<Seed> Seeds { get; set; }
        public DbSet<Harvest> Harvests { get; set; }
        public DbSet<Bed> Beds { get; set; }
        public DbSet<Season> Seasons { get; set; }
        
        // Lookup table functionality
        public DbSet<SeedFamily> SeedFamilies { get; set; }
        public DbSet<PlantHardinessZone> Zones { get; set; }
    }
}