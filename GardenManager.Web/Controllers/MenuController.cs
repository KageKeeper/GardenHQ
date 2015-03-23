using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GardenManager.Web.DataContexts;
using GardenManager.Web.ViewModels;

namespace GardenManager.Web.Controllers
{
    /*
     * The MenuController is intended to be a repository for any Actions related to the management and upkeep of any menus.
     */
    public class MenuController : Controller
    {
        private GardenDb db = new GardenDb();

        public ActionResult UpdateMainMenu()
        {
            // Create the ViewModel for the menu
            _NavigationViewModel viewModel = new _NavigationViewModel(db.Gardens.ToList());

            return PartialView("_MainNavigationPartial", viewModel);
        }
    }
}