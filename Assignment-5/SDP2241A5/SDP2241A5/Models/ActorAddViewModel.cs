using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SDP2241A5.Models
{
    public class ActorAddViewModel
    {
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Altername Name")]
        public string AlternateName { get; set; }

        [Display(Name = "Birth date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Height (m)")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal Height { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        [Display(Name = "Executive")]
        public string Executive { get; set; }
    }
}