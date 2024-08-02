using SDP2241A5.Views.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDP2241A5.Models
{
    public class ShowAddFormViewModel : ShowAddViewModel
    {
        public MultiSelectList ActorsList { get; set; }

        public string ActorsName { get; set; }

        public SelectList GenresList { get; set; }

        public string GenresName { get; set; }
    }
}