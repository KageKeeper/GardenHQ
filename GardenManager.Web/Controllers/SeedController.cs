using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GardenManager.Entities;
using GardenManager.DAL.DataContexts;
using GardenManager.DAL.Interfaces;
using GardenManager.DAL.Repositories;
using GardenManager.DAL.Services;

namespace GardenManager.Web.Controllers
{
    public class SeedController : BaseController
    {
        private ISeedService _seedService;

        public SeedController(ISeedService seedService,
            IBaseService baseService) : base(baseService)
        {
            this._seedService = seedService;
        }

        // GET: Seed
        public ActionResult Index()
        {
            return View(_seedService.GetSeeds());
        }

        // GET: Seed/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seed seed = _seedService.GetSeedByID(id);
            if (seed == null)
            {
                return HttpNotFound();
            }
            return View(seed);
        }

        // GET: Seed/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Seed/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BedId,Name,Rating,Comments,SeedsPerPackage,SeedOrderLocation,HaveOnHand,GrowThisSeason")] Seed seed)
        {
            if (ModelState.IsValid)
            {
                _seedService.AddSeed(seed);
                return RedirectToAction("Index");
            }

            return View(seed);
        }

        // GET: Seed/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seed seed = _seedService.GetSeedByID(id);
            if (seed == null)
            {
                return HttpNotFound();
            }
            return View(seed);
        }

        // POST: Seed/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BedId,Name,Rating,Comments,SeedsPerPackage,SeedOrderLocation,HaveOnHand,GrowThisSeason")] Seed seed)
        {
            if (ModelState.IsValid)
            {
                _seedService.UpdateSeed(seed);
                return RedirectToAction("Index");
            }
            return View(seed);
        }

        // GET: Seed/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seed seed = _seedService.GetSeedByID(id);
            if (seed == null)
            {
                return HttpNotFound();
            }
            return View(seed);
        }

        // POST: Seed/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Seed seed = _seedService.GetSeedByID(id);
            _seedService.DeleteSeed(seed);
            return RedirectToAction("Index");
        }
    }
}
