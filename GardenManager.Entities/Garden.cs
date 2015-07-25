using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GardenManager.Entities
{
    public class Garden
    {
        public virtual int Id { get; set; }
        
        [Required]
        public virtual string Name { get; set; }
        public virtual int ZoneId { get; set; }
        public virtual PlantHardinessZone Zone { get; set; }

        public virtual ICollection<Season> Seasons { get; set; }
        public virtual ICollection<Bed> Beds { get; set; }
        public virtual ICollection<Harvest> Harvests { get; set; }
    }
}
