using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GardenManager.Entities;
using GardenManager.Web.DataContexts;
using GardenManager.Web.ViewModels;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using GardenManager.Web.Common;
using Serilog;

namespace GardenManager.Web.Controllers
{
    public class GardenController : BaseController
    {
        private GardenDb db = new GardenDb();

        // GET: Garden
        public ActionResult Index()
        {
            var gardenViewModel = new GardenViewModel(db.Gardens.ToList());

            foreach (Garden garden in gardenViewModel.Gardens)
            {
                var beds = db.Beds.Where(b => b.GardenId == garden.Id).ToList();
                garden.Beds = beds.AsQueryable();
            }

            return View(gardenViewModel);
        }

        // GET: Garden/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return ThrowJsonError(new EntryNotFoundException(String.Format("Parameter 'id' cannot be null", id)));
            }

            var gardenViewModel = new GardenViewModel(db.Gardens.ToList());
            gardenViewModel.Garden = db.Gardens.Find(id);
            if (gardenViewModel.Garden == null)
            {
                return HttpNotFound();
            }

            // find any Beds assigned to this Garden
            var beds = db.Beds.Where(b => b.GardenId == gardenViewModel.Garden.Id).ToList();
            if (beds != null)
            {
                gardenViewModel.Garden.Beds = beds.AsQueryable();
            }
            else
            {
                gardenViewModel.Garden.Beds = null;
            }

            // find any Seasons assigned to this Garden
            var seasons = db.Seasons.Where(b => b.GardenId == gardenViewModel.Garden.Id).ToList();
            if (seasons != null)
            {
                gardenViewModel.Garden.Seasons = seasons.AsQueryable();
            }
            else
            {
                gardenViewModel.Garden.Seasons = null;
            }
            
            // find any Harvests associated with this Garden
            var harvests = db.Harvests.Where(b => b.GardenId == gardenViewModel.Garden.Id).ToList();
            if (harvests != null)
            {
                gardenViewModel.Garden.Harvests = harvests.AsQueryable();
            }
            else
            {
                gardenViewModel.Garden.Harvests = null;
            }

            return View(gardenViewModel);
        }

        // GET: Garden/Create
        public ActionResult Create()
        {
            var viewModel = new GardenViewModel(db.Gardens.ToList())
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
                garden.Zone = db.Zones.Where(z => z.Id == viewModel.ZoneId).Single();
                db.Gardens.Add(garden);
                db.SaveChanges();
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
            
            Garden garden = db.Gardens.Find(id);
            if (garden == null)
            {
                return HttpNotFound();
            }

            var viewModel = new GardenViewModel(db.Gardens.ToList())
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
                db.Gardens.Attach(garden);
                garden.ZoneId = db.Zones.Where(z => z.Id == viewModel.ZoneId).Single().Id;
                db.Entry(garden).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch (OptimisticConcurrencyException)
                {
                    var ctx = ((IObjectContextAdapter)db).ObjectContext;
                    ctx.Refresh(RefreshMode.ClientWins, db.Gardens);
                    ctx.SaveChanges();
                }

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
            Garden garden = db.Gardens.Find(id);
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
            Garden garden = db.Gardens.Find(id);
            db.Gardens.Remove(garden);
            db.SaveChanges();
            return new MvcHtmlString(Url.Action("Index", "Home"));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // Private methods

        /// <summary>
        /// Returns an IEnumerable List of Zones
        /// </summary>
        private IEnumerable<PlantHardinessZone> GetZones()
        {
            return db.Zones.ToList();
        }
    }
}
