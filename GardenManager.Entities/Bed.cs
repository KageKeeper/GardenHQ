using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenManager.Entities
{
    // A Bed can be assigned to a Garden, or not assigned at all.

    /// <summary>
    /// Represents a single Bed in which to plant. A Bed can be assigned to a Garden, or not assigned at all.
    /// </summary>
    public class Bed
    {
        public virtual int Id { get; set; }
        public virtual int GardenId { get; set; }

        public virtual bool AssignedToGarden { get; set; } // if the bed is not assigned to a Garden, the GardenId will be 0.

        [Required]
        [Display(Name = "Bed Name")]
        public virtual string Alias { get; set; } // A name for the Bed, e.g. "Herb Bed"
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#}")]
        public virtual int Width { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#}")]
        public virtual int Length { get; set; }

        /************************************************
         * Right now this is placed here for future     *
         * functionality                                *
         ***********************************************/
        [Display(Name = "Raised Bed")]
        public virtual bool IsRaisedBed { get; set; }

        /************************************************
        * Right now this is placed here for future     *
        * functionality                                *
        ***********************************************/
        [Display(Name = "Uses SFG Method")]
        public virtual bool UsingSFG { get; set; } // a check to see if the Square Foot Gardening method is being used

        public virtual IQueryable<Seed> SeedsInBed { get; set; } 
    }
}
