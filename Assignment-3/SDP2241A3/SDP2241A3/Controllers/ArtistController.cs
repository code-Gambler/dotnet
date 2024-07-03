using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDP2241A3.Controllers
{
    public class ArtistController : Controller
    {
        private Manager m = new Manager();
        // GET: Artist
        public ActionResult Index()
        {
            var artists = m.ArtistGetAll();
            return View(artists);
        }
    }
}