using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TMDbLib.Client;
using TMDbLib.Objects.Movies;

namespace Popcorn.Controllers
{
    public class MobileController : ApiController
    {
        private TMDbClient client;

        public MobileController()
        {
            client = new TMDbClient("3358925f2e2c11ceb356e574a787583d");
            
        }

        [HttpPost]
        public object SearchMovie(string title) 
        {
            var movie = client.SearchMovie(title).Results.OrderByDescending(p => p.ReleaseDate.Value).ToList().Count > 0 ? client.SearchMovie(title).Results.OrderByDescending(p => p.ReleaseDate.Value).FirstOrDefault() : null;

            if (movie != null)
                return client.GetMovie(movie.Id);
            else
                return null;
        }

        [HttpPost]
        public object GetMovieTrailer(int id)
        {
            var trailer = client.GetMovieTrailers(id);
            return trailer;
        }
    }
}
