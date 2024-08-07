﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SDP2241A3.Models
{
    public class TrackAddViewModel : TrackBaseViewModel
    {

        [Range(1, int.MaxValue, ErrorMessage = "Please select valid Album")]
        public int AlbumId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please select valid Media types")]
        public int MediaTypeId { get; set; }

    }
}