using SDP2241A2.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SDP2241A2.Models
{
    public class TrackBaseViewModel
    {
        [Key]
        public int TrackId { get; set; }

        [Required]
        [Display(Name = "Track Name")]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(220)]
        [Display(Name = "Composer Names(s)")]
        public string Composer { get; set; }

        [Display(Name = "Length(ms)")]
        public int Milliseconds { get; set; }

        [Display(Name = "Size(bytes)")]
        public int? Bytes { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [Column(TypeName = "numeric")]
        [Display(Name = "Price")]
        public decimal UnitPrice { get; set; }
    }
}