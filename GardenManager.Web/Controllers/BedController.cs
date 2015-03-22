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
using GardenManager.Web.ViewModels;

namespace GardenManager.Web.Controllers
{
    public class BedController : Controller
    {
        private GardenDb db = new GardenDb();

        // GET: Bed
        public ActionResult Index()
        {
            return View(db.Beds.ToList());
        }

        // GET: Bed/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bed bed = db.Beds.Find(id);
            if (bed == null)
            {
                return HttpNotFound();
            }
            return View(bed);
        }

        // GET: Bed/Create/5
        public ActionResult Create(int gardenId)
        {
            var viewModel = new BedViewModel
            {
                Bed = new Bed(),
                Gardens = GetGardens(),
                GardenId = gardenId
            };
            return View(viewModel);
        }

        // POST: Bed/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BedViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Bed bed = new Bed
                {
                    Alias = viewModel.Bed.Alias,
                    Width = viewModel.Bed.Width,
                    Length = viewModel.Bed.Length,
                    GardenId = viewModel.GardenId
                };
                db.Beds.Add(bed);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        // GET: Bed/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bed bed = db.Beds.Find(id);
            if (bed == null)
            {
                return HttpNotFound();
            }
            return View(bed);
        }

        // POST: Bed/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,GardenId,Alias,Width,Length,IsRaisedBed,UsingSFG")] Bed bed)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bed).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bed);
        }

        // GET: Bed/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bed bed = db.Beds.Find(id);
            if (bed == null)
            {
                return HttpNotFound();
            }
            return View(bed);
        }

        // POST: Bed/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bed bed = db.Beds.Find(id);
            db.Beds.Remove(bed);
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

        /// <summary>
        /// Returns an IEnumerable List of Garden
        /// </summary>
        private IEnumerable<Garden> GetGardens()
        {
            return db.Gardens.ToList();
        }
    }
}
