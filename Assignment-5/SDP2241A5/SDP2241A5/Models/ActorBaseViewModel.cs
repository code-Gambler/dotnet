using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SDP2241A5.Models
{
    public class ActorBaseViewModel : ActorAddViewModel
    {
        [Key]
        public int Id { get; set; }
    }
}