using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDP2241A5.Controllers
{
    public class GenresController : Controller
    {
        // Reference to the manager object
        Manager m = new Manager();

        // GET: Genres
        public ActionResult Index()
        {
            var genres = m.GenreGetAll();
            return View(genres);
        }
    }
}
