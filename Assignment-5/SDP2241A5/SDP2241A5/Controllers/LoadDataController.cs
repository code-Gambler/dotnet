using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDP2241A5.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LoadDataController : Controller
    {

        // Reference to the manager object
        Manager m = new Manager();

        // GET: LoadData
        [AllowAnonymous()]
        public ActionResult Roles()
        {
            if (m.LoadRoles())
            {
                return Content("data has been loaded");
            }
            else
            {
                return Content("data exists already");
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Genres()
        {
            // Load genres data
            // Implement logic to load genres using the manager method
            if (m.LoadGenres())
            {
                return Content("Genres data has been loaded");
            }
            else
            {
                return Content("data exists already");
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Actors()
        {
            // Load actors data
            // Implement logic to load actors using the manager method
            if (m.LoadActors())
            {
                return Content("Actors data has been loaded");
            }
            else
            {
                return Content("data exists already");
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Shows()
        {
            // Load shows data
            // Implement logic to load shows using the manager method
            if (m.LoadShows())
            {
                return Content("Shows data has been loaded");
            }
            else
            {
                return Content("data exists already");
            }
        }

        [Authorize(Roles = "Admin")] // Require "Admin" role for this action
        public ActionResult Episodes()
        {
            // Load episodes data
            // Implement logic to load episodes using the manager method
            if (m.LoadEpisodes())
            {
                return Content("Episodes data has been loaded");
            }
            else
            {
                return Content("data exists already");
            }
        }

        public ActionResult Remove()
        {
            if (m.RemoveData())
            {
                return Content("data has been removed");
            }
            else
            {
                return Content("could not remove data");
            }
        }

        public ActionResult RemoveDatabase()
        {
            if (m.RemoveDatabase())
            {
                return Content("database has been removed");
            }
            else
            {
                return Content("could not remove database");
            }
        }

    }
}