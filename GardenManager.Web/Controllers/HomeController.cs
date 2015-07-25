using GardenManager.DAL.DataContexts;
using GardenManager.DAL.Interfaces;
using GardenManager.DAL.Repositories;
using GardenManager.Entities;
using GardenManager.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GardenManager.Web.Controllers
{
    public class HomeController : Controller
    {
        private IBaseRepository<Garden> gardenRepository = null;

        public HomeController()
        {
            this.gardenRepository = new BaseRepository<Garden>();
        }

        public HomeController(IBaseRepository<Garden> repository)
        {
            this.gardenRepository = repository;
        }

        public ActionResult Index()
        {
            var viewModel = new _LayoutViewModel(gardenRepository.Get());
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