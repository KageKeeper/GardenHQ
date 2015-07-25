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

namespace GardenManager.Web.Controllers
{
    /*
     * The MenuController is intended to be a repository for any Actions related to the management and upkeep of any menus.
     */
    public class MenuController : BaseController
    {
        private IBaseRepository<Garden> gardenRepository = null;

        public MenuController()
        {
            this.gardenRepository = new BaseRepository<Garden>();
        }

        public MenuController(IBaseRepository<Garden> repository)
        {
            this.gardenRepository = repository;
        }

        public ActionResult UpdateRootGardenMenu()
        {
            // Create the ViewModel for the menu
            //_NavigationViewModel viewModel = new _NavigationViewModel(db.Gardens.ToList());

            return PartialView("~/Views/Garden/Partials/_IndexGardenPartial.cshtml", gardenRepository.Get());
        }
    }
}