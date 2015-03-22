using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GardenManager.Entities;

namespace GardenManager.Web.ViewModels
{
    public class _LayoutViewModel
    {
        public IEnumerable<Garden> Gardens { get; private set; }

        public _LayoutViewModel()
        {

        }
        public _LayoutViewModel(IEnumerable<Garden> gardens)
        {
            Gardens = gardens;
        }
    }
}