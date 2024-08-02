using SDP2241A5.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDP2241A5.Controllers
{
    public class ShowController : Controller
    {
        // Reference to the manager object
        Manager m = new Manager();

        // GET: Show
        public ActionResult Index()
        {
            var shows = m.ShowGetAll();
            return View(shows);
        }

        // GET: Show/Details/5
        public ActionResult Details(int? id)
        {
            var show = m.ShowGetByIdWithDetail(id.GetValueOrDefault());

            if (show == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(show);
            }
        }

        [Authorize(Roles = "Clerk")]
        [Route("Show/{id}/AddEpisode")]
        [HttpGet]
        public ActionResult AddEpisode(int? id)
        {

            var show = m.ShowGetByIdWithDetail(id.GetValueOrDefault());

            if (show == null)
            {
                return HttpNotFound();
            }

            else
            {

                var episode = new EpisodeAddFormViewModel();
                episode.Clerk = User.Identity.Name;
                episode.ShowId = show.Id;
                episode.ShowName = show.Name;

                episode.GenresList = new SelectList(m.GenreGetAll(), "Name", "Name");

                return View(episode);
            }
        }

        [Authorize(Roles = "Clerk")]
        [HttpPost]
        [Route("Show/{id}/AddEpisode")]
        [ValidateInput(false)]
        public ActionResult AddEpisode(EpisodeAddViewModel newEpisode)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(newEpisode);
                }

                var addedEpisode = m.EpisodeAdd(newEpisode);

                if (addedEpisode == null)
                {
                    return View(newEpisode);
                }
                else
                {
                    return RedirectToAction("Details", "Episode", new { id = addedEpisode.Id });
                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                    }
                }
                return View();
            }
        }
    }
}
