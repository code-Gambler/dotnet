using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SDP2241A3.Models
{
    public class AlbumBaseViewModel
    {
        [Key]
        [Display(Name = "Album ID")]
        public int AlbumId { get; set; }

        [Required]
        [StringLength(160)]
        public string Title { get; set; }
    }
}