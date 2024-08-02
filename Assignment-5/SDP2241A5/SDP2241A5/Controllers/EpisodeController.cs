using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDP2241A5.Controllers
{
    public class EpisodeController : Controller
    {
        // Reference to the manager object
        Manager m = new Manager();

        // GET: Episode
        public ActionResult Index()
        {
            var episodes = m.GetAllEpisodesWithShowNames();
            return View(episodes);
        }

        // GET: Episode/Details/5
        public ActionResult Details(int? id)
        {
            var episode = m.EpisodeGetByIdWithDetail(id.GetValueOrDefault());
            if (episode == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(episode);
            }
        }
    }
}
