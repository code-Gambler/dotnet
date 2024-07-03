using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SDP2241A3.Models
{
    public class PlaylistWithTracksViewModel: PlaylistBaseViewModel
    {
        public PlaylistWithTracksViewModel()
        {
            Tracks = new List<TrackBaseViewModel>();
        }
        [Display(Name = "Tracks on the Playlist")]
        public IEnumerable<TrackBaseViewModel> Tracks { get; set; }
    }
}