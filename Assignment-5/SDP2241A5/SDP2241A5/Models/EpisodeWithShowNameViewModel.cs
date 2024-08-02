using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SDP2241A5.Models
{
    public class EpisodeWithShowNameViewModel : EpisodeBaseViewModel
    {
        [Display(Name = "Show Name")]
        public string ShowName { get; set; }
    }
}