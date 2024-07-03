using SDP2241A3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDP2241A3.Controllers
{
    public class PlaylistController : Controller
    {
        // GET: Playlist
        private Manager m = new Manager();
        public ActionResult Index()
        {
            var playlists = m.PlaylistGetAll();
            return View(playlists);
        }
        public ActionResult Details(int? id)
        {
            var requiredPlaylist = m.PlaylistGetById(id.GetValueOrDefault());
            if (requiredPlaylist == null)
            {
                return HttpNotFound();
            }
            else
            {
                var tracks = requiredPlaylist.Tracks.OrderBy(t => t.Name);
                requiredPlaylist.Tracks = tracks;

                return View(requiredPlaylist);
            }
        }
        public ActionResult Edit(int? id)
        {
            var currentPlaylist = m.PlaylistGetById(id.GetValueOrDefault());
            if (currentPlaylist == null)
            {
                return HttpNotFound();
            }
            else
            {
                var updatedObj = m.mapper.Map<PlaylistWithTracksViewModel, PlaylistEditTracksFormViewModel>(currentPlaylist);
                var selectedValues = currentPlaylist.Tracks.Select(t => t.TrackId);
                updatedObj.Tracks = currentPlaylist.Tracks.OrderBy(t => t.Name);
                updatedObj.TrackList = new MultiSelectList
                    (items: m.TrackGetAllWithDetail(),
                    dataValueField: "TrackId",
                    dataTextField: "NameFull",
                    selectedValues: selectedValues);
                return View(updatedObj);
            }
        }
        [HttpPost]
        public ActionResult Edit(int? id, PlaylistEditTracksViewModel updatedItem)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Edit", new { id = updatedItem.PlaylistId });
            }
            if (id.GetValueOrDefault() != updatedItem.PlaylistId)
            {
                return RedirectToAction("index");
            }
            try
            {
                var editedItem = m.PlaylistEditTracks(updatedItem);
                return RedirectToAction("details", new { id = updatedItem.PlaylistId });
            }
            catch
            {
                return RedirectToAction("Edit", new { id = updatedItem.PlaylistId });

            }

        }
    }
}