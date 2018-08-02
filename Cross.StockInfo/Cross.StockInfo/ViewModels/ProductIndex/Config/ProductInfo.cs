using System;
using System.Collections.Generic;
using System.Text;

namespace Cross.StockInfo.ViewModels.ProductIndex.Config
{
    /// <summary>
    /// 定義商品資訊頁面需要替換的UI描述或資料內容
    /// </summary>
    public abstract class ProductInfo
    {
        /// <summary>
        /// 取得或設定商品指數的名稱與查詢ID對照清單
        /// </summary>
        public List<SeriesInfo> SeriesInfoCollection { get => GetSeriesInfoCollection(); }

        /// <summary>
        /// 取得或設定產品走勢圖表的名稱
        /// </summary>
        public string ChartTitle { get => GetChartTitle(); }

        public string DailyPriceTitle { get => GetDailyPriceTitle(); }

        protected abstract List<SeriesInfo> GetSeriesInfoCollection();
        protected abstract string GetChartTitle();
        protected abstract string GetDailyPriceTitle();
    }
}
