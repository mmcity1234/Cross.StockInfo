using Cross.StockInfo.Common;
using Cross.StockInfo.Model.News;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace Cross.StockInfo.ViewModels.News
{
    public class NewsTabItem : BaseViewModel
    {
        private ObservableCollection<NewsModel> _newsItemSources = new ObservableCollection<NewsModel>();

        /// <summary>
        /// 取得或設定目前新聞讀取的頁面數
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 取得或設定目前的新聞分類
        /// </summary>
        public string NewsType { get; set; }

        #region ViewModel

        public string Title { get; set; }
        public ObservableCollection<NewsModel> NewsItemSources
        {
            get => _newsItemSources;
            set
            {
                _newsItemSources = value;
                OnPropertyChanged();
            }
        }
        #endregion
     
        public NewsTabItem()
        {
        }

       

        /// <summary>
        /// 批次新增新聞訊息資料
        /// </summary>
        /// <param name="newsCollection"></param>
        public void AddNewsItems(List<NewsModel> newsCollection)
        {
            foreach (var newItem in newsCollection)
            {
                NewsItemSources.Add(newItem);
            }
            
        }
    }
}
