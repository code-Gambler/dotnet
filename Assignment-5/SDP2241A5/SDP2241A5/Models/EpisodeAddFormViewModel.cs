﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDP2241A5.Models
{
    public class EpisodeAddFormViewModel : EpisodeAddViewModel
    {
        public SelectList GenresList { get; set; }

        public string GenresName { get; set; }
    }
}