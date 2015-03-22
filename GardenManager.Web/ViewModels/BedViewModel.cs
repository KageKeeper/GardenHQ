using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GardenManager.Entities;

namespace GardenManager.Web.ViewModels
{
    public class BedViewModel
    {
        [Display(Name = "Garden Assigned To")]
        public int GardenId { get; set; }
        public IEnumerable<Garden> Gardens { get; set; }

        public Bed Bed { get; set; }
    }
}