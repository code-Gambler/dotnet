using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StevenDavidPillay_162218218_Test4.Controllers
{
    [Authorize(Roles = "Administrator")]
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