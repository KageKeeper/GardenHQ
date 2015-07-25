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

namespace GardenManager.Web.Controllers
{
    public class SeedController : Controller
    {
        private IBaseRepository<Seed> SeedRepository = null;

        public SeedController()
        {
            this.SeedRepository = new BaseRepository<Seed>();
        }

        public SeedController(IBaseRepository<Seed> repository)
        {
            this.SeedRepository = repository;
        }

        // GET: Seed
        public ActionResult Index()
        {
            return View(SeedRepository.Get());
        }

        // GET: Seed/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seed seed = SeedRepository.GetByID(id);
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
                SeedRepository.Insert(seed);
                SeedRepository.Save();
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
            Seed seed = SeedRepository.GetByID(id);
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
                SeedRepository.Update(seed);
                SeedRepository.Save();
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
            Seed seed = SeedRepository.GetByID(id);
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
            Seed seed = SeedRepository.GetByID(id);
            SeedRepository.Delete(seed);
            SeedRepository.Save();
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
