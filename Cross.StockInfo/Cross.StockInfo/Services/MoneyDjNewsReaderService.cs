using Core.Utility.Network;
using Cross.StockInfo.Common.Helper;
using Cross.StockInfo.Model.News;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace Cross.StockInfo.Services
{
    public class MoneyDjNewsReaderService : INewsReaderService
    {
        
        private const string DomainUrl = "https://www.moneydj.com";
        private const string MobileDomainUrl = "https://m.moneydj.com";
        /// <summary>
        /// 新聞網址
        /// </summary>
        private const string NewsPathUrl = "/KMDJ/News/NewsRealList.aspx?index1={0}&a={1}";
        private const string NewsViewerPath = "/KMDJ/News/NewsViewer.aspx";
        private const string MobileNewsViewerPath = "/f1a.aspx";


        public async Task<List<NewsModel>> ListNewsTaskAsync(int pageIndex)
        {
            return await ListNewsTaskAsync(pageIndex, EnumHelper.ParseToString(MoneyDJNewsType.Topic));
        }

        public async Task<List<NewsModel>> ListNewsTaskAsync(int pageIndex, string newsType)
        {

            string url = DomainUrl + string.Format(NewsPathUrl, pageIndex, newsType);
            string results = await RestApi.GetContentTaskAsync(url);
            // fetch the news data list
            var resultList = HtmlHelper.DescendantsPath(results, "//table/tr", node =>
            {
                bool? result = node.Attributes["style"]?.Value?.StartsWith("background-color:");
                return result.GetValueOrDefault(false);
            }
            , node =>
            { 
                string time = HtmlHelper.ReadDocumentValue(node.InnerHtml, "//td[1]").Replace("\r\n", string.Empty).Trim();
                string title = HtmlHelper.ReadDocumentValue(node.InnerHtml, "//td[2]/a", "title");
                string mobileNewsPath = HtmlHelper.ReadDocumentValue(node.InnerHtml, "//td[2]/a", "href").Replace(NewsViewerPath, MobileNewsViewerPath);
                // e.g. https://m.moneydj.com/f1a.aspx?a={uuid}&c=MB010000
                string newsUrl = MobileDomainUrl + mobileNewsPath;
                NewsModel newsModel = new NewsModel
                {
                    Title = title,
                    Time = DateTime.ParseExact(time, "MM/dd HH:mm", CultureInfo.InvariantCulture),
                    Url = newsUrl
                };
                return newsModel;
            });
            return resultList;
        }
    }
}
