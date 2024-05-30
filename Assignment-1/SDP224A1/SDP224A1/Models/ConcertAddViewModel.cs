using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SDP224A1.Models
{
    public class ConcertAddViewModel
    {
        public ConcertAddViewModel()
        {
            ConcertDate = DateTime.Now.AddDays(31);
        }

        [Required]
        [StringLength(128)]
        [Display(Name = "Concert Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(80)]
        [Display(Name = "Organizing Company")]
        public string Company { get; set; }

        [StringLength(70)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [StringLength(40)]
        [Display(Name = "City")]
        public string City { get; set; }

        [StringLength(40)]
        [Display(Name = "State")]
        public string State { get; set; }

        [StringLength(40)]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [StringLength(10)]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [StringLength(24)]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }

        [StringLength(100)]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [StringLength(100)]
        [Display(Name = "Website")]
        [DataType(DataType.Url)]
        public string Website { get; set; }

        [Display(Name = "Concert Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ConcertDate { get; set; }
    }
}