﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GardenManager.Web.CustomViewEngines
{
    public class GardenHQViewEngine : RazorViewEngine
    {
        /// <summary>
        /// This allows the customization of the locations in which views will be automatically searched.
        /// </summary>
        public GardenHQViewEngine()
        {
            string[] locations = new string[] {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/{1}/Partials/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml"
            };

            this.ViewLocationFormats = locations;
            this.PartialViewLocationFormats = locations;
            this.MasterLocationFormats = locations;
        }
    }
}