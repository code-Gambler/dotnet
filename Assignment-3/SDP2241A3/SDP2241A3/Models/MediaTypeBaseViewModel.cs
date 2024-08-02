using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SDP2241A3.Models
{
    public class MediaTypeBaseViewModel
    {
        [Key]
        [Display(Name = "Media Type ID")]static 
        public int MediaTypeId { get; set; }

        [StringLength(120)]
        public string Name { get; set; }
    }
}