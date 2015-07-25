using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GardenManager.Entities;
using GardenManager.Web.ViewModels;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using GardenManager.Web.Common;
using Serilog;
using GardenManager.DAL;
using GardenManager.DAL.Interfaces;
using GardenManager.DAL.Repositories;
using GardenManager.DAL.DataContexts;

namespace GardenManager.Web.Controllers
{
    public class GardenController : BaseController
    {
        private IBaseRepository<Garden> gardenRepository = null;

        public GardenController()
        {
            this.gardenRepository = new BaseRepository<Garden>();
        }

        public GardenController(IBaseRepository<Garden> repository)
        {
            this.gardenRepository = repository;
        }

        // GET: Garden
        public ActionResult Index()
        {
            var gardenViewModel = new GardenViewModel(gardenRepository.Get(includeProperties: "Beds"));

            return View(gardenViewModel);
        }

        // GET: Garden/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return ThrowJsonError(new EntryNotFoundException(String.Format("Parameter 'id' cannot be null", id)));
            }

            var gardenViewModel = new GardenViewModel(gardenRepository.Get(includeProperties: "Beds, Seasons, Harvests"));
            gardenViewModel.Garden =  gardenViewModel.Gardens.FirstOrDefault(g => g.Id == id);
            if (gardenViewModel.Garden == null)
            {
                return HttpNotFound();
            }

            return View(gardenViewModel);
        }

        // GET: Garden/Create
        public ActionResult Create()
        {
            var viewModel = new GardenViewModel(gardenRepository.Get())
            {
                Garden = new Garden(),
                Zones = GetZones()
            };
            return PartialView("_CreateGardenPartial", viewModel);
        }

        // POST: Garden/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IHtmlString Create(GardenViewModel viewModel)
        {
            Garden Model = viewModel.Garden;

            if (ModelState.IsValid)
            {
                Garden garden = new Garden();
                garden.Name = Model.Name;
                garden.Zone = gardenRepository.Fetch<PlantHardinessZone>().Where(z => z.Id == viewModel.ZoneId).Single();
                gardenRepository.Insert(garden);
                gardenRepository.Save();
                return new MvcHtmlString(Url.Action("Details", new { id = garden.Id }));
            }

            return new MvcHtmlString("");
        }

        // GET: Garden/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return ThrowJsonError(new EntryNotFoundException(String.Format("Parameter 'id' cannot be null", id)));
            }
            
            Garden garden = gardenRepository.GetByID(id);
            if (garden == null)
            {
                return HttpNotFound();
            }

            var viewModel = new GardenViewModel(gardenRepository.Get())
            {
                Garden = garden,
                ZoneId = garden.Zone.Id,
                Zones = GetZones()
            };

            return PartialView("_EditGardenPartial", viewModel);
        }

        // POST: Garden/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IHtmlString Edit(GardenViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Garden garden = viewModel.Garden;
                garden.ZoneId = gardenRepository.Fetch<PlantHardinessZone>().Where(z => z.Id == viewModel.ZoneId).Single().Id;
                gardenRepository.Update(garden);
                gardenRepository.Save();

                return new MvcHtmlString(Url.Action("Details", new { id = viewModel.Garden.Id }));

            }
            return new MvcHtmlString("");
        }

        // GET: Garden/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return ThrowJsonError(new EntryNotFoundException(String.Format("Parameter 'id' cannot be null", id)));
            }
            Garden garden = gardenRepository.GetByID(id);
            if (garden == null)
            {
                return HttpNotFound();
            }
            return PartialView("_DeleteGardenPartial", garden);
        }

        // POST: Garden/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IHtmlString DeleteConfirmed(int id)
        {
            Garden garden = gardenRepository.GetByID(id);
            gardenRepository.Delete(garden);
            gardenRepository.Save();
            return new MvcHtmlString(Url.Action("Index", "Home"));
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        // Private methods

        /// <summary>
        /// Returns an IEnumerable List of Zones
        /// </summary>
        private IEnumerable<PlantHardinessZone> GetZones()
        {
            return gardenRepository.Get<PlantHardinessZone>();
        }
    }
}
