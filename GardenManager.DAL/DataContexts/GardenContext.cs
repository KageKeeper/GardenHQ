using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using GardenManager.Entities;
using GardenManager.DAL.Interfaces;

namespace GardenManager.DAL.DataContexts
{
    public class GardenContext : DbContext
    {
        public GardenContext()
            : base("DefaultConnection") { }

        public GardenContext(string connectioNString)
            : base(connectioNString) { }

        public DbSet<Garden> Gardens { get; set; }
        public DbSet<Seed> Seeds { get; set; }
        public DbSet<Harvest> Harvests { get; set; }
        public DbSet<Bed> Beds { get; set; }
        public DbSet<Season> Seasons { get; set; }

        public DbSet<SeedFamily> SeedFamilies { get; set; }
        public DbSet<PlantHardinessZone> Zones { get; set; }
    }
}