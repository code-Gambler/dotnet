using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDP2241A3.Controllers
{
    public class AlbumController : Controller
    {
        private Manager m = new Manager();
        // GET: Album
        public ActionResult Index()
        {
            var albums = m.AlbumGetAll();
            return View(albums);
        }
    }
}