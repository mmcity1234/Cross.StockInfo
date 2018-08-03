using System;
using System.Collections.Generic;
using System.Text;

namespace Cross.StockInfo.Services.Product
{
    /// <summary>
    /// 杜拜輕原油
    /// </summary>
    [StockIndex("Product.DubaiOilIndex")]
    public class DubaiOilIndex : FubonProduct
    {
        private const string DayUrl = "https://fubon-ebrokerdj.fbs.com.tw/Z/ZN/ZNM/CZNM.djbcd?A=FM200049";
        private const string WeekUrl = "https://just2.entrust.com.tw/Z/ZH/ZHG/CZHG.djbcd?A=131690";
 

        protected override string GetUrl(AverageType averageType)
        {
            if (averageType == AverageType.Week)
                return WeekUrl;
            else
                return DayUrl;
        }
    }
}
