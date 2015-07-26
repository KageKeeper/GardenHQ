using GardenManager.DAL.Interfaces;
using GardenManager.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GardenManager.Web.Controllers
{
    /// <summary>
    /// This will include various behaviours commonly used in all Controllers.
    /// </summary>
    public class BaseController : Controller
    {
        protected IBaseService _baseService;

        public BaseController(IBaseService baseService)
        {
            this._baseService = baseService;
        }

        /// <summary>
        /// Adds the ability to return an easy to manage JSON error message to the UI.
        /// </summary>
        public JsonResult ThrowJsonError(Exception e)
        {
            Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
            Response.StatusDescription = e.Message;

            return Json(new { Message = e.Message }, JsonRequestBehavior.AllowGet);
        }


        // After every action that is not a child action, the Unit Of Work Save() method will be
        // called to commit changes to the db.
        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (!filterContext.IsChildAction)
            {
                int save = _baseService.Save();
            }
        }
    }
}