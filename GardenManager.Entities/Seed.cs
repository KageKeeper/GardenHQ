using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenManager.Entities
{
    public class Seed
    {
        public virtual int Id { get; set; }
        public virtual int BedId { get; set; } // the id of the bed this seed will be planted in
        public virtual string Name { get; set; }
        public virtual int Rating { get; set; }
        public virtual string Comments { get; set; }
        public virtual SeedFamily Family { get; set; }
        public virtual int SeedsPerPackage { get; set; }
        public virtual string SeedOrderLocation { get; set; } // url of rareseeds.com, for example
        public virtual bool HaveOnHand { get; set; } // do I have some of these aleady?
        public virtual bool GrowThisSeason { get; set; } // am I planning on growing this seed in the upcoming growing season

        public virtual IQueryable<Harvest> Harvests { get; set; }
    }
}
