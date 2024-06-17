using SDP2241A2.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SDP2241A2.Models
{
    public class InvoiceLineBaseViewModel
    {
        [Display(Name = "Line ID")]
        public int InvoiceLineId { get; set; }

        [Column(TypeName = "numeric")]
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Line Total")]
                [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal LinePrice
        {
            get
            {
                return Quantity * UnitPrice;
            }
        }
    }
}