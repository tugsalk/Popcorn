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
using Newtonsoft.Json;
using System.IO;

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
            var movie = client.SearchMovie(title).Results.ToList().Count > 0 ? client.SearchMovie(title).Results.FirstOrDefault() : null;
            
            if (movie != null)
                return client.GetMovie(movie.Id);
            else
                return null;
        }

        [HttpPost]
        public object GetNowPlaying() 
        {
            var browser = new ScrapingBrowser();
            browser.Encoding = System.Text.Encoding.UTF8;
            var html = browser.NavigateToPage(new Uri("http://sinema.mynet.com/vizyondaki-filmler   ")).Html;
            var parsedList = html.CssSelect("div.vizyonListe");
            var list = parsedList.Select(div => { return div.ToMovieItem(); }).ToList();

            foreach (MovieItem mi in list) 
            {
                var movie = client.SearchMovie(mi.OriginalTitle).Results.ToList().Count > 0 ? client.SearchMovie(mi.OriginalTitle).Results.FirstOrDefault() : null;

                if (movie != null)
                {
                    mi.ImdbScore = movie.VoteAverage.ToString();
                    mi.TheMovieDbID = movie.Id;
                }
            }

            System.IO.TextWriter tw = new StreamWriter(@"C:\Users\Kübra\Desktop\Text.txt");
            var data = JsonConvert.SerializeObject(list);
            tw.Write(data);
            tw.Flush();
            tw.Close();
            return list;
        }
    }
}
