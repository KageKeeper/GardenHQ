using GardenManager.Web.DataContexts;
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
        private GardenDb db = new GardenDb();

        public ActionResult Index()
        {
            var viewModel = new _LayoutViewModel(db.Gardens.ToList());
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