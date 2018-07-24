using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Cross.StockInfo.Model.News
{
    public class NewsModel
    {
        private string _title = string.Empty;
        private string _description = string.Empty;
        private string _publish = string.Empty;

        public string Title
        {
            get => _title;
            set => _title = value;
        }

        public string Description
        {
            get => _description;
            set => _description = value;
        }               
        /// <summary>
        /// 新聞出版來源
        /// </summary>
        public string Publish
        {
            get => _publish;
            set => _publish = value;
        }
        public DateTime Time { get; set; }
        public string Url { get; set; }

        public string TimeLabel
        {
            get => Time.ToString(CultureInfo.InvariantCulture);
        }
    }
}
