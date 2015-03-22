using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenManager.Entities
{
    public class Season
    {
        public virtual int Id { get; set; }
        [Display(Name = "Year")]
        public virtual string SeasonYear { get; set; }
        public virtual int GardenId { get; set; }

        public virtual Harvest Harvest { get; set; }
        public virtual IQueryable<Seed> IndoorSeedStartSet { get; set; }
        public virtual IQueryable<Seed> DirectSeedSet { get; set; }
        public virtual IQueryable<Seed> TransplantSeedSet { get; set; }
    }
}
