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

namespace GardenManager.Web.Controllers
{
    public class GardenController : Controller
    {
        private GardenDb db = new GardenDb();

        // GET: Garden
        public ActionResult Index()
        {
            var model = db.Gardens.ToList();
            return View(model);
        }

        // GET: Garden/Details/5
        public ActionResult Details(int? id)
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

        // GET: Garden/Create
        public ActionResult Create()
        {
            var viewModel = new GardenViewModel
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
                return RedirectToAction("Index");
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

            var viewModel = new GardenViewModel
            {
                Garden = garden,
                ZoneId = garden.Zone.Id,
                Zones = GetZones()
            };

            return View(viewModel);
        }

        // POST: Garden/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GardenViewModel viewModel)
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
                
                return RedirectToAction("Index");
            }
            return View(viewModel);
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
