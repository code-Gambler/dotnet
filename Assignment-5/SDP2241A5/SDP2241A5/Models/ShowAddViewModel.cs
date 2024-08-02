using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SDP2241A5.Views.Actor
{
    public class ShowAddViewModel
    {
        public ShowAddViewModel()
        {
            ReleaseDate = DateTime.Now;
        }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Genre")]
        public string Genre { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        [Display(Name = "Coordinator")]
        public string Coordinator { get; set; }

        public int ActorId { get; set; }

        public int GenreId { get; set; }
    }
}