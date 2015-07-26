using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GardenManager.DAL.DataContexts;
using GardenManager.Web.ViewModels;
using GardenManager.DAL.Interfaces;
using GardenManager.Entities;
using GardenManager.DAL.Repositories;
using GardenManager.DAL.Services;

namespace GardenManager.Web.Controllers
{
    /*
     * The MenuController is intended to be a repository for any Actions related to the management and upkeep of any menus.
     */
    public class MenuController : BaseController
    {
        private IGardenService _gardenService;

        public MenuController(IGardenService gardenService,
            IBaseService baseService) : base(baseService)
        {
            this._gardenService = gardenService;
        }

        public ActionResult UpdateRootGardenMenu()
        {
            // Create the ViewModel for the menu
            //_NavigationViewModel viewModel = new _NavigationViewModel(db.Gardens.ToList());

            return PartialView("~/Views/Garden/Partials/_IndexGardenPartial.cshtml", _gardenService.GetGardens());
        }
    }
}