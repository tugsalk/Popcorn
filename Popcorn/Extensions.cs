using HtmlAgilityPack;
using Popcorn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ScrapySharp.Extensions;

namespace Popcorn
{
    public static class Extensions
    {
        public static MovieItem ToMovieItem(this HtmlNode node)
        {
            var head = node.CssSelect("div.vizyonImg").FirstOrDefault();
            var properties = node.CssSelect("ul.vizyonInfo > li").ToList();
            var content = node.CssSelect("div.vizyonText").FirstOrDefault();
            var m = new MovieItem();

            m.ImageUrl = head.ChildNodes["a"].ChildNodes["img"].Attributes["src"].Value;
            m.TurkishTitle = head.ChildNodes["a"].ChildNodes["span"].InnerText.Trim();
            m.OriginalTitle = head.ChildNodes[2].InnerText.Trim();
            m.Year = properties[0].ChildNodes[1].InnerText.Trim();
            m.ReleaseDate = properties[1].ChildNodes[1].InnerText.Trim();
            m.Score = properties[2].ChildNodes[1].InnerText.Trim();
            m.Genre = properties[3].ChildNodes[1].InnerText.Trim();
            m.Description = content.ChildNodes["p"].InnerText.Trim();

            return m;
        }
    }
}