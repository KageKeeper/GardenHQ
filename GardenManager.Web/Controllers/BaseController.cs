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
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// Adds the ability to return an easy to manage JSON error message to the UI.
        /// </summary>
        public JsonResult ThrowJsonError(Exception e)
        {
            Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
            Response.StatusDescription = e.Message;

            return Json(new { Message = e.Message }, JsonRequestBehavior.AllowGet);
        }
    }
}