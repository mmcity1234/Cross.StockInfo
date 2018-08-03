using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Cross.StockInfo.Services.Product
{
    [StockIndex("Product.BpiIndex")]
    public class BpiIndex : FubonProduct
    {
        /// <summary>
        /// 散裝貨運巴拿馬指數
        /// </summary>
        private const string DayUrl = "https://fubon-ebrokerdj.fbs.com.tw/Z/ZN/ZNM/CZNM.djbcd?A=FM400008";
        private const string WeekUrl = "https://just2.entrust.com.tw/Z/ZH/ZHG/CZHG.djbcd?A=260020";

        protected override string GetUrl(AverageType averageType)
        {
            if (averageType == AverageType.Week)
                return WeekUrl;
            else
                return DayUrl;
        }
    }
}
