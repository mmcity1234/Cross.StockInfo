using Cross.StockInfo.Assets.Strings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cross.StockInfo.ViewModels.ProductIndex.Config
{
    /// <summary>
    /// 農產品相關
    /// </summary>
    public class AgriculturalProductInfo : ProductInfo
    {
        protected override string GetChartTitle()
        {
            return AppResources.AgriculturalProducts_ChartTitle;
        }

        protected override string GetDailyPriceTitle()
        {
            return AppResources.SoyIndex_Label;
        }

        protected override List<SeriesInfo> GetSeriesInfoCollection()
        {
            return new List<SeriesInfo>
            {
                new SeriesInfo { QueryKey = "Product.SoyIndex", Name = AppResources.SoyIndex_Label, Visible = true },
                new SeriesInfo { QueryKey = "Product.WheatIndex", Name = AppResources.WheatIndex_Label, Visible = false },
          
            };
        }
    }
}
