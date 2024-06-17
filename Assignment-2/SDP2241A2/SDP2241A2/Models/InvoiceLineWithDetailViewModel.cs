using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SDP2241A2.Models
{
    public class InvoiceLineWithDetailViewModel : InvoiceLineBaseViewModel
    {
        [Required]
        [StringLength(200)]
        public string TrackName { get; set; }

        [StringLength(220)]
        public string TrackComposer { get; set; }

        [Required]
        [StringLength(160)]
        public string TrackAlbumTitle { get; set; }

        [StringLength(120)]
        public string TrackAlbumArtistName { get; set; }

        [StringLength(120)]
        public string TrackGenreName { get; set; }

        [StringLength(120)]
        public string TrackMediaTypeName { get; set; }
    }
}