using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SDP2241A5.Models
{
    public class GenresBaseViewModel
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required, StringLength(30)]
        public string Name { get; set; }
    }
}