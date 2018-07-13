using System;
using System.Collections.Generic;
using System.Text;

namespace Cross.StockInfo.Model.News
{
    public class NewsModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }
        /// <summary>
        /// 新聞出版來源
        /// </summary>
        public string Publish { get; set; }
        public DateTime Time { get; set; }
    }
}
