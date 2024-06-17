using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SDP2241A2.Data;

namespace SDP2241A2.Models
{
    public class InvoiceBaseViewModel
    {
        [Key]
        public int InvoiceId { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:MMM d, yyyy}")]
        public DateTime InvoiceDate { get; set; }

        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        [Display(Name = "Billing Address")]
        public string BillingAddress { get; set; }

        [Display(Name = "City")]
        public string BillingCity { get; set; }

        [Display(Name = "State")]
        public string BillingState { get; set; }

        [StringLength(40)]
        [Display(Name = "Country")]
        public string BillingCountry { get; set; }

        [StringLength(10)]
        [Display(Name = "Postal/Zip")]
        public string BillingPostalCode { get; set; }

        [Column(TypeName = "numeric")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name = "Total")]
        public decimal Total { get; set; }
    }
}