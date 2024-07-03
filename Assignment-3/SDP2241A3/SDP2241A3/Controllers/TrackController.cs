using SDP2241A3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDP2241A3.Controllers
{
    public class TrackController : Controller
    {
        // GET: Track
        private Manager m = new Manager();

        public ActionResult Index()
        {
            var tracks = m.TrackGetAllWithDetail();
            return View(tracks);
        }
        public ActionResult Details(int? id)
        {
            var requiredTrack = m.TrackGetById(id.GetValueOrDefault());
            return View(requiredTrack);
        }
        public ActionResult Create()
        {
            var newObj = new TrackAddFormViewModel();

            newObj.AlbumList = new SelectList(m.AlbumGetAll(), "AlbumId", "Title", 156);
            newObj.MediaTypeList = new SelectList(m.MediaTypeGetAll(), "MediaTypeId", "Name", 1);
            return View(newObj);
        }

        [HttpPost]
        public ActionResult Create(TrackAddViewModel newItem)
        {
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }

            var addedItem = m.TrackAdd(newItem);

            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {
                return RedirectToAction("details", new { id = addedItem.TrackId });
            }
        }
    }
}