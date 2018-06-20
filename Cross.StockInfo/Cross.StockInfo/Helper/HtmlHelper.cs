using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cross.StockInfo.Helper
{
    public static class HtmlHelper
    {
        public static string ReadDocumentValue(string html, string xpath, string attribute)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var value = doc.DocumentNode
                .SelectNodes(xpath);
              
            return value != null ? value.First().Attributes[attribute].Value : null;
        }

        public static string ReadDocumentValue(string html, string xpath)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var value = doc.DocumentNode.SelectNodes(xpath);

            return value != null ? value.First().InnerText : null;
        }

        public static List<T> Descendants<T>(string html, string name, Func<HtmlNode, T> selector)
        {           
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            // Descendants           
            var results = doc.DocumentNode.Descendants(name);

            return results == null
               ? new List<T>()
               : results.Select(selector).ToList();
        }

        public static List<T> DescendantsPath<T>(string html, string xpath, Func<HtmlNode, T> selector)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var results = doc.DocumentNode.SelectNodes(xpath);             
            
            return results == null 
                ? new List<T>() 
                : results.Select(selector).ToList();
        }
    }
}
