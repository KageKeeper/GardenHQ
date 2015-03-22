using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GardenManager.Entities;

namespace GardenManager.Web.ViewModels
{
    public class GardenViewModel : _LayoutViewModel
    {
        public GardenViewModel()
        {
        }
        public GardenViewModel(IEnumerable<Garden> gardens) : base (gardens)
        {
        }

        [Display(Name = "Zone")]
        public int ZoneId { get; set; }
        public IEnumerable<PlantHardinessZone> Zones { get; set; }

        public Garden Garden { get; set; }
        public Bed Bed { get; set; }
        public Season Season { get; set; }
        public Harvest Harvest { get; set; }
    }
}