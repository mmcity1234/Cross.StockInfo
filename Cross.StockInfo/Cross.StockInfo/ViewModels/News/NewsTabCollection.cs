using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
namespace Cross.StockInfo.ViewModels.News
{
    public class NewsTabCollection : ObservableCollection<NewsTabItem>
    {
        /// <summary>
        /// 找尋符合的新聞分類頁籤資料
        /// </summary>
        /// <param name="newsType"></param>
        /// <returns></returns>
        public NewsTabItem FindTabItem(string newsType)
        {
            return this.Where(x => x.NewsType == newsType).FirstOrDefault();
        }
    }
}
