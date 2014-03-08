using HtmlAgilityPack;
using Popcorn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TMDbLib.Client;
using TMDbLib.Objects.Movies;
using ScrapySharp.Network;
using ScrapySharp.Extensions;


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

        [HttpPost]
        public object GetInTheatres()
        {
            var browser = new ScrapingBrowser();
            var html = browser.NavigateToPage(new Uri("http://sinema.mynet.com/vizyondaki-filmler   ")).Html;
            var parsedList = html.CssSelect("div.vizyonListe");

            var list = parsedList.Select(div => { return div.ToMovieItem(); }).ToList();

            return list;
        }


    }
}
