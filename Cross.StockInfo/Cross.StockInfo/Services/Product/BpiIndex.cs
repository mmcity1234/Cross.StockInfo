using System;
using System.Collections.Generic;
using System.Text;

namespace Cross.StockInfo.Services.Product
{
    [StockIndex("Product.BpiIndex")]
    public class BpiIndex : FubonProduct
    {
        /// <summary>
        /// 散裝貨運巴拿馬指數
        /// </summary>
        private const string BpiIndexUrl = "https://fubon-ebrokerdj.fbs.com.tw/Z/ZN/ZNM/CZNM.djbcd?A=FM400008";

        protected override string GetUrl()
        {
            return BpiIndexUrl;
        }
    }
}
