using SDP2241A5.Models;
using SDP2241A5.Views.Actor;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDP2241A5.Controllers
{
    public class ActorController : Controller
    {
        // Reference to the manager object
        Manager m = new Manager();

        // GET: Actor
        public ActionResult Index()
        {
            var actors = m.ActorGetAll();
            return View(actors);
        }

        // GET: Actor/Details/5
        public ActionResult Details(int? id)
        {
            var actor = m.ActorGetByIdWithDetail(id.GetValueOrDefault());
            if (actor == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(actor);
            }
        }

        // GET: Actor/Create
        [Authorize(Roles = "Executive")]
        public ActionResult Create()
        {
            var actor = new ActorAddViewModel();

            actor.Executive = User.Identity.Name;
            return View(actor);
        }

        // POST: Actor/Create
        [HttpPost]
        [Authorize(Roles = "Executive")]
        public ActionResult Create(ActorAddViewModel newItem)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(newItem);
                }

                var addedItem = m.ActorAdd(newItem);

                if (addedItem == null)
                {
                    return View(newItem);
                }

                else
                {
                    return RedirectToAction("Details", new { id = addedItem.Id });
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

        [Authorize(Roles = "Coordinator")]
        [Route("Actor/{id}/AddShow")]
        [HttpGet]
        public ActionResult AddShow(int? id)
        {

            var a = m.ActorGetById(id.GetValueOrDefault());

            if (a == null)
            {
                return HttpNotFound();
            }

            else
            {

                var o = new ShowAddFormViewModel();
                o.Coordinator = User.Identity.Name;
                o.ActorId = a.Id;
                o.ActorsName = a.Name;

                o.GenresList = new SelectList(m.GenreGetAll(), "Name", "Name");

                var selectedActor = new List<int> { a.Id };
                o.ActorsList = new MultiSelectList(m.ActorGetAll(), "Id", "Name", selectedActor);

                return View(o);
            }
        }

        [Authorize(Roles = "Coordinator")]
        [HttpPost]
        [Route("Actor/{id}/AddShow")]
        [ValidateInput(false)]
        public ActionResult AddShow(ShowAddViewModel newItem)
        {

            if (!ModelState.IsValid)
            {
                return View(newItem);
            }

            var addedItem = m.ShowAdd(newItem);

            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {
                return RedirectToAction("Details", "Show", new { id = addedItem.Id });
            }
        }
    }
}
