using System;
using System.ComponentModel.DataAnnotations;

namespace SDP224A1.Models
{
    public class ConcertEditViewModel
    {
        public int ConcertId { get; set; }

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

        [StringLength(6, MinimumLength = 6, ErrorMessage = "Promo Code must be exactly 6 characters.")]
        [RegularExpression(@"^[A-Z]{3}[0-9]{3}$", ErrorMessage = "Promo Code must be in the format 'LLLNNN'.")]
        [Display(Name = "Promo Code")]
        public string PromoCode { get; set; }

        [Range(1, 150000, ErrorMessage = "Capacity must be between 1 and 150,000.")]
        [Display(Name = "Capacity")]
        public int? Capacity { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Ticket Sale Password")]
        public string TicketSalePassword { get; set; }
    }
}