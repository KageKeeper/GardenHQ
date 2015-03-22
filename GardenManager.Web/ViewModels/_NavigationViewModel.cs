using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GardenManager.Entities;

namespace GardenManager.Web.ViewModels
{
    public class _NavigationViewModel 
    {
        public IEnumerable<Garden> Gardens { get; set; }

        public _NavigationViewModel(IEnumerable<Garden> gardens)
        {
            Gardens = gardens;
        }
    }
}