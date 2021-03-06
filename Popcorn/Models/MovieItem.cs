﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Popcorn.Models
{
    public class MovieItem
    {
        public int PopcornID { get; set; }
        public int TheMovieDbID { get; set; }
        public string TurkishTitle { get; set; }
        public string OriginalTitle { get; set; }
        public string ImageUrl { get; set; }
        public string Genre { get; set; }
        public string Year { get; set; }
        public string ReleaseDate { get; set; }
        public string Score { get; set; }
        public string Description { get; set; }
        public string ImdbScore { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - {1} - {2}", TurkishTitle, OriginalTitle, Year);
        }

    }

}