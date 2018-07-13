using Core.Utility.Network;
using Cross.StockInfo.Common.Helper;
using Cross.StockInfo.Model.News;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace Cross.StockInfo.Services
{
    public class MoneyDjNewsReaderService : INewsReaderService
    {

        private const string DomainUrl = "https://www.moneydj.com";
        /// <summary>
        /// 新聞網址
        /// </summary>
        private const string NewsPathUrl = "/KMDJ/News/NewsRealList.aspx?index1={0}&a={0}";

      
        public async Task<List<NewsModel>> ListNewsTaskAsync(int pageIndex)
        {
            return await ListNewsTaskAsync(pageIndex, EnumHelper.ParseToString(MoneyDJNewsType.Topic));
        }

        public async Task<List<NewsModel>> ListNewsTaskAsync(int pageIndex, string newsType)
        {
            string url = DomainUrl + string.Format(NewsPathUrl, pageIndex, newsType);
            string results = await RestApi.GetContentTaskAsync(url);

        }
    }
}
