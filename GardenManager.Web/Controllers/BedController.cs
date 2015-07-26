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
using GardenManager.Web.ViewModels;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using GardenManager.Web.Common;
using GardenManager.DAL.Interfaces;
using GardenManager.DAL.Repositories;
using GardenManager.DAL.Services;

namespace GardenManager.Web.Controllers
{
    public class BedController : BaseController
    {
        private IBedService _bedService;

        public BedController(IBedService bedService, 
            IBaseService baseService) : base(baseService)
        {
            this._bedService = bedService;
        }

        // GET: Bed
        public ActionResult Index()
        {
            return View(_bedService.GetBeds());
        }

        public PartialViewResult GetAssignPartial(int id)
        {
            var viewModel = new BedViewModel(_bedService.GetGardens())
            {
                Bed = new Bed(),
                UnassignedBeds = _bedService.GetUnassignedBeds(),
                GardenId = id
            };

            return PartialView("_AssignBedPartial", viewModel);
        }

        public PartialViewResult GetCreatePartial(int id)
        {
            var viewModel = new BedViewModel(_bedService.GetGardens())
            {
                Bed = new Bed(),
                UnassignedBeds = _bedService.GetUnassignedBeds(),
                GardenId = id
            };

            return PartialView("_CreateBedPartial", viewModel);
        }

        // GET: Bed/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return ThrowJsonError(new EntryNotFoundException(String.Format("Parameter 'id' cannot be null", id)));
            }
            Bed bed = _bedService.GetBedByID(id);
            if (bed == null)
            {
                return HttpNotFound();
            }
            return View(bed);
        }

        // GET: Bed/CreateOrAssign
        public ActionResult CreateOrAssign(int id)
        {
            var viewModel = new BedViewModel(_bedService.GetGardens())
            {
                Bed = new Bed(),
                UnassignedBeds = _bedService.GetUnassignedBeds(),
                GardenId = id
            };

            return PartialView("_CreateOrAssignBedPartial", viewModel);
        }

        // GET: Bed/Create/5
        public ActionResult Create(int gardenId)
        {
            var viewModel = new BedViewModel(_bedService.GetGardens())
            {
                Bed = new Bed(),
                GardenId = gardenId
            };
            return View(viewModel);
        }

        // POST: Bed/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IHtmlString Create(BedViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Bed bed = new Bed
                {
                    Alias = viewModel.Bed.Alias,
                    Width = viewModel.Bed.Width,
                    Length = viewModel.Bed.Length,
                    GardenId = viewModel.GardenId,
                    AssignedToGarden = true
                };
                _bedService.AddBed(bed);

                return new MvcHtmlString(Url.Action("Details", "Garden", new { id = viewModel.GardenId }));
            }

            return new MvcHtmlString("");
        }

        // POST: Bed/Assign
        // This Action will assign the selected Bed to the selected Garden.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IHtmlString Assign(BedViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Bed bed = _bedService.GetBedByID(viewModel.BedId);
                bed.GardenId = viewModel.GardenId;
                bed.AssignedToGarden = true;

                _bedService.UpdateBed(bed);
                return new MvcHtmlString(Url.Action("Details", "Garden", new { id = viewModel.GardenId }));
            }

            return new MvcHtmlString("");
        }
        // GET: Bed/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return ThrowJsonError(new EntryNotFoundException(String.Format("Parameter 'id' cannot be null", id)));
            }

            var viewModel = new BedViewModel(_bedService.GetGardens());
            viewModel.Bed = _bedService.GetBedByID(id);
            if (viewModel.Bed == null)
            {
                return HttpNotFound();
            }

            viewModel.GardenId = viewModel.Bed.GardenId;

            return PartialView("_EditBedPartial", viewModel);
        }

        // POST: Bed/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IHtmlString Edit(BedViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Bed bed = _bedService.GetBedByID(viewModel.Bed.Id);
                bed.Alias = viewModel.Bed.Alias;
                bed.Width = viewModel.Bed.Width;
                bed.Length = viewModel.Bed.Length;

                _bedService.UpdateBed(bed);

                return new MvcHtmlString(Url.Action("Details", "Garden", new { id = viewModel.GardenId }));
            }
            return new MvcHtmlString("");
        }

        // Unassign removes the association between the selected Bed and current Garden.
        // Unassigning does NOT delete the Bed.
        // GET: Bed/Unassign/4
        public ActionResult Unassign(int? id)
        {
            if (id == null)
            {
                return ThrowJsonError(new EntryNotFoundException(String.Format("Parameter 'id' cannot be null", id)));
            }

            BedViewModel viewModel = new BedViewModel(_bedService.GetGardens());
            viewModel.Bed = _bedService.GetBedByID(id);

            if (viewModel.Bed == null)
            {
                return HttpNotFound();
            }

            viewModel.GardenId = viewModel.Bed.GardenId;

            return PartialView("_UnassignBedPartial", viewModel);
        }

        // POST: Bed/Unassign/4
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IHtmlString Unassign(BedViewModel viewModel)
        {
            Bed bed = _bedService.GetBedByID(viewModel.Bed.Id);
            bed.GardenId = 0;
            bed.AssignedToGarden = false;

            _bedService.UpdateBed(bed);

            return new MvcHtmlString(Url.Action("Details", "Garden", new { id = viewModel.GardenId }));
        }

        // GET: Bed/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return ThrowJsonError(new EntryNotFoundException(String.Format("Parameter 'id' cannot be null", id)));
            }
            Bed bed = _bedService.GetBedByID(id);
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
            Bed bed = _bedService.GetBedByID(id);
            _bedService.DeleteBed(bed);
            return RedirectToAction("Index");
        }
    }
}
