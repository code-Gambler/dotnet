using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SDP2241A3.Models
{
    public class ArtistBaseViewModel
    {
        [Key]
        [Display(Name = "Artist ID")]
        public int ArtistId { get; set; }

        [StringLength(120)]
        public string Name { get; set; }
    }
}