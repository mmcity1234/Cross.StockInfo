using Cross.StockInfo.Assets.Strings;
using Cross.StockInfo.Common.Helper;
using Cross.StockInfo.Model.News;
using Cross.StockInfo.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using Cross.StockInfo.Common;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace Cross.StockInfo.ViewModels.News
{
    public class NewsViewModel : BaseViewModel
    {
      
        private bool _isNewsItemLoading;
        private NewsTabCollection _tabItemSources;
        private NewsTabItem _selectedTabItem;

        #region Injection
        public INewsReaderService NewsService { get; set; }
        #endregion

        public NewsTabCollection TabItemSources
        {
            get => _tabItemSources;
            set
            {
                _tabItemSources = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 載入更多新聞資訊的讀取狀態
        /// </summary>
        public bool IsNewsItemLoading
        {
            get => _isNewsItemLoading; set
            {
                _isNewsItemLoading = value;
                OnPropertyChanged();
            }
        }

      
        public NewsTabItem SelectedTabItem
        {
            get => _selectedTabItem;
            set
            {
                _selectedTabItem = value;
                OnPropertyChanged();
            }
        }


        public DelegateCommand<SelectedItemChangedEventArgs> NewsItemSelectedCommand { get; set; }
        public DelegateCommand<ItemVisibilityEventArgs> NewsItemAppreaingCommand { get; set; }
        public DelegateCommand<EventArgs> NewsTabChangedCommand { get; set; }


        public NewsViewModel()
        {


            TabItemSources = new NewsTabCollection()
            {
                new NewsTabItem{ Title = AppResources.News_All_TabTile, NewsType = EnumHelper.ParseToString(MoneyDJNewsType.All), PageIndex = 1 },
                new NewsTabItem{ Title = AppResources.News_Topic_TabTile, NewsType = EnumHelper.ParseToString(MoneyDJNewsType.Topic), PageIndex = 1 },
                new NewsTabItem{ Title = AppResources.News_International_TabTile, NewsType = EnumHelper.ParseToString(MoneyDJNewsType.Global), PageIndex = 1 },
                new NewsTabItem{ Title = AppResources.News_TaiwanStock_TabTile, NewsType = EnumHelper.ParseToString(MoneyDJNewsType.TaiwanStock), PageIndex = 1 },
                new NewsTabItem{ Title = AppResources.News_AmericanStock_TabTile, NewsType = EnumHelper.ParseToString(MoneyDJNewsType.AmericanStock), PageIndex = 1 },
                new NewsTabItem{ Title = AppResources.News_Product_TabTile, NewsType = EnumHelper.ParseToString(MoneyDJNewsType.Product), PageIndex = 1 },
                new NewsTabItem{ Title = AppResources.News_Industry_TabTile, NewsType = EnumHelper.ParseToString(MoneyDJNewsType.Industry), PageIndex = 1 },
                new NewsTabItem{ Title = AppResources.News_Research_TabTile, NewsType = EnumHelper.ParseToString(MoneyDJNewsType.Research), PageIndex = 1 },
            };
            NewsItemAppreaingCommand = new DelegateCommand<ItemVisibilityEventArgs>(NewsItemAppearingEventHandler);
            NewsItemSelectedCommand = new DelegateCommand<SelectedItemChangedEventArgs>(NewsItemSelectedEventHandler);
            NewsTabChangedCommand = new DelegateCommand<EventArgs>(NewsTabChangedEventHandler);
            // set the selected tab item in the first page
            SelectedTabItem = TabItemSources[0];
        }

        private async void NewsTabChangedEventHandler(EventArgs args)
        {
            if (SelectedTabItem.NewsItemSources.Count == 0)
            {
                IsPageLoading = true;
                await LoadNewsData(1, SelectedTabItem.NewsType);
                IsPageLoading = false;
            }
        }

        /// <summary>
        /// ListView 項目因捲動呈現所觸發的事件
        /// </summary>
        /// <param name="e"></param>
        private async void NewsItemAppearingEventHandler(ItemVisibilityEventArgs e)
        {
            var appearingNewsModel = e.Item as NewsModel;
            if (appearingNewsModel == null || SelectedTabItem.FindLastItem() == null)
                return;
            try
            {
              
                if (!IsNewsItemLoading && SelectedTabItem.FindLastItem().Url == appearingNewsModel.Url)
                {
                    IsNewsItemLoading = true;
                    SelectedTabItem.PageIndex++;
                    await LoadNewsData(SelectedTabItem.PageIndex, SelectedTabItem.NewsType);
                    IsNewsItemLoading = false;

                }
            }
            catch (Exception exception)
            {
                IsPageLoading = false;
                //hit bottom! or data error
            }

        }

        /// <summary>
        /// 點選ListView項目所觸發的事件
        /// </summary>
        /// <param name="args"></param>
        private void NewsItemSelectedEventHandler(SelectedItemChangedEventArgs args)
        {
            try
            {
                var item = args.SelectedItem as NewsModel;
                if (item != null)
                {
                    WebViewModel webModel = new WebViewModel { Url = item.Url };
                    webModel.HtmlLoadingSourceEvent += HtmlSourceEvent;
                    Navigation.Navigate(typeof(Cross.StockInfo.Views.WebView), webModel);
                }
            }
            catch (Exception e)
            {
                /// exception
            }

        }

        protected override async void OnPageFirstLoad()
        {            
            IsPageLoading = true;
            base.OnPageFirstLoad();           
            await LoadNewsData(1, EnumHelper.ParseToString(MoneyDJNewsType.All));
            IsPageLoading = false;
        }

        private async Task<string> HtmlSourceEvent(string url)
        {
            return await NewsService.GetNewsHtmlContentTaskAsync(url);
        }

        private Task LoadNewsData(int pageIndex, string newsType)
        {
            return Task.Run(async() =>
            {
                var allNews = await NewsService.ListNewsTaskAsync(pageIndex, newsType).ConfigureAwait(false) ;
    
                var tabItem = TabItemSources.FindTabItem(newsType);
                if (tabItem != null)
                {
                    tabItem.AddNewsItems(allNews);
                }
            });
        }
    }
}
