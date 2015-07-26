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
using GardenManager.DAL.Services;

namespace GardenManager.Web.Controllers
{
    public class GardenController : BaseController
    {
        private IGardenService _gardenService;

        public GardenController(IGardenService gardenService,
            IBaseService baseService) : base(baseService)
        {
            this._gardenService = gardenService;
        }

        // GET: Garden
        public ActionResult Index()
        {
            var gardenViewModel = new GardenViewModel(_gardenService.GetGardens(includeProperties: "Beds"));

            return View(gardenViewModel);
        }

        // GET: Garden/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return ThrowJsonError(new EntryNotFoundException(String.Format("Parameter 'id' cannot be null", id)));
            }

            var gardenViewModel = new GardenViewModel(_gardenService.GetGardens(includeProperties: "Beds, Seasons, Harvests"));
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
            var viewModel = new GardenViewModel(_gardenService.GetGardens())
            {
                Garden = new Garden(),
                Zones = _gardenService.GetZones()
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
                garden.Zone = _gardenService.GetZoneByID(viewModel.ZoneId);
                _gardenService.AddGarden(garden);
                // Need to explicitly call Save() since the newly created
                // Garden.Id is needed for the Details call, and the auto-save
                // does not happen until the Action is complete.
                _gardenService.Save();
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
            
            Garden garden = _gardenService.GetGardenByID(id);
            if (garden == null)
            {
                return HttpNotFound();
            }

            var viewModel = new GardenViewModel(_gardenService.GetGardens())
            {
                Garden = garden,
                ZoneId = garden.Zone.Id,
                Zones = _gardenService.GetZones()
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
                garden.ZoneId = _gardenService.GetZoneByID(viewModel.ZoneId).Id;
                _gardenService.UpdateGarden(garden);

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
            Garden garden = _gardenService.GetGardenByID(id);
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
            Garden garden = _gardenService.GetGardenByID(id);
            _gardenService.DeleteGarden(garden);
            return new MvcHtmlString(Url.Action("Index", "Home"));
        }
    }
}
