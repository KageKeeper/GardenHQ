using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GardenManager.Entities;

namespace GardenManager.Web.ViewModels
{
    public class BedViewModel : _LayoutViewModel
    {
        public BedViewModel()
        {
        }
        public BedViewModel(IEnumerable<Garden> gardens) : base (gardens)
        {
        }

        [Display(Name = "Assign To")]
        public int GardenId { get; set; }

        public int BedId { get; set; }

        [Display(Name = "Unassigned Beds")]
        public IEnumerable<Bed> UnassignedBeds { get; set; }

        public Bed Bed { get; set; }
    }
}