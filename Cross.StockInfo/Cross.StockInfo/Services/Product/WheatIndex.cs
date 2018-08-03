using System;
using System.Collections.Generic;
using System.Text;

namespace Cross.StockInfo.Services.Product
{
    /// <summary>
    /// 小麥近期
    /// </summary>
    [StockIndex("Product.WheatIndex")]
    public class WheatIndex : FubonProduct
    {
        private const string DayUrl = "https://fubon-ebrokerdj.fbs.com.tw/Z/ZN/ZNM/CZNM.djbcd?A=413";
        private const string WeekUrl = "https://just2.entrust.com.tw/Z/ZH/ZHG/CZHG.djbcd?A=120140";
        protected override string GetUrl(AverageType averageType)
        {
            if (averageType == AverageType.Week)
                return WeekUrl;
            else
                return DayUrl;
        }
    }
}
