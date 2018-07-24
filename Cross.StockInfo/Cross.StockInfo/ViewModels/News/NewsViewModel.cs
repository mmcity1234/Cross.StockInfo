using Cross.StockInfo.Assets.Strings;
using Cross.StockInfo.Common.Helper;
using Cross.StockInfo.Model.News;
using Cross.StockInfo.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
namespace Cross.StockInfo.ViewModels.News
{
    public class NewsViewModel : BaseViewModel
    {
        private NewsTabCollection _tabItemSources;
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
        }

        public override async void OnPageLoading()
        {
            base.OnPageLoading();

            string allTypeName = EnumHelper.ParseToString(MoneyDJNewsType.All);
            var allNews = await NewsService.ListNewsTaskAsync(1, allTypeName);

            var tabItem = TabItemSources.FindTabItem(allTypeName);
            if (tabItem != null)
            {
                tabItem.AddNewsItems(allNews);
            }
        }
    }
}
