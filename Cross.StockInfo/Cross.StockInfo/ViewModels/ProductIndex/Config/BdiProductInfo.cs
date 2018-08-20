using Cross.StockInfo.Assets.Strings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cross.StockInfo.ViewModels.ProductIndex.Config
{
    public class BdiProductInfo : ProductInfo
    {

        protected override string GetChartTitle()
        {
            return AppResources.BDIIndex_ChartTitle;
        }

        protected override string GetDailyPriceTitle()
        {
            return AppResources.BDIIndex_Label;
        }

        protected override List<SeriesInfo> GetSeriesInfoCollection()
        {
            return new List<SeriesInfo>
            {
                new SeriesInfo { QueryKey = "Product.BdiIndex", Name = AppResources.BDIIndex_Label, Visible = true, IsPrimary = true },
                new SeriesInfo { QueryKey = "Product.BpiIndex", Name = AppResources.BPIIndex_Label, Visible = false },
            };
        }
    }
}
