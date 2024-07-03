using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDP2241A3.Controllers
{
    public class MediaTypeController : Controller
    {
        private Manager m = new Manager();
        // GET: MediaType
        public ActionResult Index()
        {
            var mediaTypes = m.MediaTypeGetAll();
            return View(mediaTypes);
        }
    }
}