using System;
using System.Collections.Generic;
using System.Text;

namespace Cross.StockInfo.Services.Product
{
    /// <summary>
    /// 紐約輕原油(西德州輕級原油)
    /// </summary>
    [StockIndex("Product.NewYorkOilIndex")]
    public class NewYorkOilIndex : FubonProduct
    {
        private const string DayUrl = "https://fubon-ebrokerdj.fbs.com.tw/Z/ZN/ZNM/CZNM.djbcd?A=856";
        private const string WeekUrl = "https://just2.entrust.com.tw/Z/ZH/ZHG/CZHG.djbcd?A=130810";
        protected override string GetUrl(AverageType averageType)
        {
            if (averageType == AverageType.Week)
                return WeekUrl;
            else
                return DayUrl;
        }
    }
}
