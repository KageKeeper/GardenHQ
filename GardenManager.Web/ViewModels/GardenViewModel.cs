using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GardenManager.Entities;

namespace GardenManager.Web.ViewModels
{
    public class GardenViewModel
    {
        [Display(Name = "Zone")]
        public int ZoneId { get; set; }
        public IEnumerable<PlantHardinessZone> Zones { get; set; }

        public Garden Garden { get; set; }
    }
}