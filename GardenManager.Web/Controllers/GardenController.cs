﻿using System;
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

namespace GardenManager.Web.Controllers
{
    public class GardenController : Controller
    {
        private GardenDb db = new GardenDb();

        // GET: Garden
        public ActionResult Index()
        {
            var gardenViewModel = new GardenViewModel(db.Gardens.ToList());
            //var model = db.Gardens.ToList();

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
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
            return View(viewModel);
        }

        // POST: Garden/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GardenViewModel viewModel)
        {
            Garden Model = viewModel.Garden;

            if (ModelState.IsValid)
            {
                Garden garden = new Garden();
                garden.Name = Model.Name;
                garden.Zone = db.Zones.Where(z => z.Id == viewModel.ZoneId).Single();
                db.Gardens.Add(garden);
                db.SaveChanges();
                return RedirectToAction("Index", "Garden");
            }

            return View(viewModel);
        }

        // GET: Garden/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
        public string Edit(GardenViewModel viewModel)
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

                return Url.Action("Details", new { id = viewModel.Garden.Id });

            }
            return "";
        }

        // GET: Garden/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Garden garden = db.Gardens.Find(id);
            if (garden == null)
            {
                return HttpNotFound();
            }
            return View(garden);
        }

        // POST: Garden/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Garden garden = db.Gardens.Find(id);
            db.Gardens.Remove(garden);
            db.SaveChanges();
            return RedirectToAction("Index");
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