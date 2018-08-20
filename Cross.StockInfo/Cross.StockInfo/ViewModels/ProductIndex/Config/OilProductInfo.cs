using Cross.StockInfo.Assets.Strings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cross.StockInfo.ViewModels.ProductIndex.Config
{
    public class OilProductInfo : ProductInfo
    {
        protected override string GetChartTitle()
        {
            return AppResources.OilIndex_ChartTitle;
        }

        protected override string GetDailyPriceTitle()
        {
            return AppResources.BrentCrudeOilIndex_Label;
        }

        protected override List<SeriesInfo> GetSeriesInfoCollection()
        {
            return new List<SeriesInfo>
            {
                new SeriesInfo { QueryKey = "Product.BrentCrudeOilIndex", Name = AppResources.BrentCrudeOilIndex_Label, Visible = true, IsPrimary = true },
                new SeriesInfo { QueryKey = "Product.NewYorkOilIndex", Name = AppResources.NewYorkOilIndex_Label, Visible = false },
                new SeriesInfo { QueryKey = "Product.DubaiOilIndex", Name = AppResources.DubaiOilIndex_Label, Visible = false }
            };
        }
    }
}
