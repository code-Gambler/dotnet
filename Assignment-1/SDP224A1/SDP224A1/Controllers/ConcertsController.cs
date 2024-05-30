using SDP224A1.Models;
using SDP224A1.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace SDP224A1.Controllers
{
    public class ConcertsController : Controller
    {
        private Manager m = new Manager();

        // GET: Concerts
        public ActionResult Index()
        {
            var c = m.ConcertGetAll();

            return View(c);
        }

        // GET: Concerts/Details/5
        public ActionResult Details(int? id)
        {
            // Attempt to get the matching object
            var obj = m.ConcertGetById(id.GetValueOrDefault()); //The GetValueOrDefault function works if the value matches the type then returns value, otherwise returns 0.
            if (obj == null)
                return HttpNotFound();
            else
                return View(obj);
        }

        // GET: Concerts/Create
        public ActionResult Create()
        {
            var emp = new ConcertAddViewModel();

            return View(emp);
        }

        // POST: Concerts/Create
        [HttpPost]
        public ActionResult Create(ConcertAddViewModel newItem)
        {
            if (!ModelState.IsValid)
                return View(newItem);
            try
            {
                // Process the input
                var addedItem = m.ConcertAdd(newItem);
                // If the item was not added, return the user to the Create page
                // otherwise redirect them to the Details page.
                if (addedItem == null)
                    return View(newItem);
                else
                    return RedirectToAction("Details", new { id = addedItem.ConcertId });
            }
            catch
            {
                return View(newItem);
            }
        }

        // GET: Concerts/Edit/5
        public ActionResult Edit(int? id)
        {
            // Attempt to fetch the matching object
            var obj = m.ConcertGetById(id.GetValueOrDefault());
            if (obj == null)
                return HttpNotFound();
            else
            {
                // Create and configure an "edit form"
                // Notice that obj is a CustomerBaseViewModel object so
                // we must map it to a CustomerEditContactFormViewModel object
                // Notice that we can use AutoMapper anywhere,
                // and not just in the Manager class.
                var formObj = m.mapper.Map<ConcertBaseViewModel, ConcertEditFormViewModel>(obj);
                return View(formObj);
            }
        }

        // POST: Concerts/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, ConcertEditViewModel model)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                // Our "version 1" approach is to display the "edit form" again
                return RedirectToAction("Edit", new { id = model.ConcertId });
            }
            if (id.GetValueOrDefault() != model.ConcertId) //Retrieves the value of the current Nullable<T> object, or the default value of the underlying type.
            {
                // This appears to be data tampering, so redirect the user away
                return RedirectToAction("Index");
            }
            // Attempt to do the update
            var editedItem = m.ConcertEdit(model);

            if (editedItem == null)
            {
                // There was a problem updating the object
                // Our "version 1" approach is to display the "edit form" again
                return RedirectToAction("Edit", new { id = model.ConcertId });
            }
            else
            {
                // Show the details view, which will show the updated data
                return RedirectToAction("Details", new { id = model.ConcertId });
            }
        }

        // GET: Concerts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var obj = m.ConcertGetById(id.GetValueOrDefault());

            if (obj == null)
            {
                return HttpNotFound();
            }

            return View(obj);
        }
        // POST: Concerts/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var result = m.ConcertDelete(id);

            if (!result)
            {
                // There was a problem deleting the object
                return RedirectToAction("Delete", new { id = id });
            }
            else
            {
                // Redirect to the index view after successful deletion
                return RedirectToAction("Index");
            }
        }
    }
}