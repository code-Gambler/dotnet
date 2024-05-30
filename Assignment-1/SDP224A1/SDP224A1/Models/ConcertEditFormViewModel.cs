using SDP224A1.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace SDP224A1.Models
{
    public class ConcertEditFormViewModel : ConcertEditViewModel
    {
        [Required]
        [StringLength(128)]
        [Display(Name = "Concert Name")]
        public string Name { get; set; }
    }
}