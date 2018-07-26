using Cross.StockInfo.Model.News;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cross.StockInfo.Services
{
    public interface INewsReaderService
    {
        Task<List<NewsModel>> ListNewsTaskAsync(int pageIndex);


        /// <summary>
        /// List the news topic with type
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="newsType">e.g. MoneyDJNewsType</param>
        /// <returns></returns>
        Task<List<NewsModel>> ListNewsTaskAsync(int pageIndex, string newsType);

        /// <summary>
        /// 取得新聞Html標記語言的文件格式
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<string> GetNewsHtmlContentTaskAsync(string url);

    }
}
