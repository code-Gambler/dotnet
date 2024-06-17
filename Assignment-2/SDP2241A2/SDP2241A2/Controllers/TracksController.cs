using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDP2241A2.Controllers
{
    public class TracksController : Controller
    {
        private Manager m = new Manager();

        // GET: Tracks
        public ActionResult Index()
        {
            var t = m.TrackGetAll();
            return View(t);
        }

        public ActionResult BluesJazz()
        {
            var bj = m.TrackGetBluesJazz();

            return View("Index", bj);
        }

        public ActionResult CantrellStaley()
        {
            var cs = m.TrackGetCantrellStaley();

            return View("Index", cs);
        }

        public ActionResult Top50Longest()
        {
            var big50 = m.TrackGetTop50Longest();

            return View("Index", big50);
        }

        public ActionResult Top50Smallest()
        {
            var small50 = m.TrackGetTop50Smallest();

            return View("Index", small50);
        }
    }
}
