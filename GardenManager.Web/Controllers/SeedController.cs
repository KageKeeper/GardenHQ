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
using GardenManager.Web.DataContexts;

namespace GardenManager.Web.Controllers
{
    public class SeedController : Controller
    {
        private GardenDb db = new GardenDb();

        // GET: Seed
        public async Task<ActionResult> Index()
        {
            return View(await db.Seeds.ToListAsync());
        }

        // GET: Seed/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seed seed = await db.Seeds.FindAsync(id);
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
        public async Task<ActionResult> Create([Bind(Include = "Id,BedId,Name,Rating,Comments,SeedsPerPackage,SeedOrderLocation,HaveOnHand,GrowThisSeason")] Seed seed)
        {
            if (ModelState.IsValid)
            {
                db.Seeds.Add(seed);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(seed);
        }

        // GET: Seed/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seed seed = await db.Seeds.FindAsync(id);
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
        public async Task<ActionResult> Edit([Bind(Include = "Id,BedId,Name,Rating,Comments,SeedsPerPackage,SeedOrderLocation,HaveOnHand,GrowThisSeason")] Seed seed)
        {
            if (ModelState.IsValid)
            {
                db.Entry(seed).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(seed);
        }

        // GET: Seed/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seed seed = await db.Seeds.FindAsync(id);
            if (seed == null)
            {
                return HttpNotFound();
            }
            return View(seed);
        }

        // POST: Seed/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Seed seed = await db.Seeds.FindAsync(id);
            db.Seeds.Remove(seed);
            await db.SaveChangesAsync();
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
    }
}
