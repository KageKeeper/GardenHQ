using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenManager.Entities
{
    // A Bed HAS to be assigned to a Garden. 
    public class Bed
    {
        public virtual int Id { get; set; }
        public virtual int GardenId { get; set; }

        [Display(Name = "Bed Name")]
        public virtual string Alias { get; set; } // Perhaps each bed has a name, e.g. "Herb Bed"
        public virtual decimal Width { get; set; }
        public virtual decimal Length { get; set; }

        [Display(Name = "Raised Bed")]
        public virtual bool IsRaisedBed { get; set; }

        [Display(Name = "Uses SFG Method")]
        public virtual bool UsingSFG { get; set; } // a check to see if the Square Foot Gardening method is being used

        public virtual IQueryable<Seed> SeedsInBed { get; set; } 
    }
}
