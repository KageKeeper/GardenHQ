using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenManager.Entities
{
    public class Harvest
    {
        public virtual int Id { get; set; }
        public virtual int Year { get; set; }
        public virtual int GardenId { get; set; }
        public virtual int SeasonId { get; set; }
        public virtual int SeedId { get; set; }
        public virtual decimal YieldInWeight { get; set; }
        public virtual decimal YieldInProduce { get; set; } // the actual count of fruit/veggie produced
    }
}
