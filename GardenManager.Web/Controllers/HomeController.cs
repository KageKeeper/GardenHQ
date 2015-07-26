using GardenManager.DAL.DataContexts;
using GardenManager.DAL.Interfaces;
using GardenManager.DAL.Repositories;
using GardenManager.DAL.Services;
using GardenManager.Entities;
using GardenManager.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GardenManager.Web.Controllers
{
    public class HomeController : BaseController
    {
        private IGardenService _gardenService;

        public HomeController(IGardenService gardenService, 
            IBaseService baseService) : base(baseService)
        {
            this._gardenService = gardenService;
        }

        public ActionResult Index()
        {
            var viewModel = new _LayoutViewModel(_gardenService.GetGardens());
            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}