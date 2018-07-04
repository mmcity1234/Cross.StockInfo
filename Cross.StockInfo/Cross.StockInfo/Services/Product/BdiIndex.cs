using System;
using System.Collections.Generic;
using System.Text;

namespace Cross.StockInfo.Services.Product
{
    [StockIndex("Product.BdiIndex")]
    public class BdiIndex : FubonProduct
    {  /// <summary>
       /// 散裝貨運BDI指數
       /// </summary>
        private const string BdiIndexUrl = "https://fubon-ebrokerdj.fbs.com.tw/Z/ZN/ZNM/CZNM.djbcd?A=FM400007";
        protected override string GetUrl()
        {
            return BdiIndexUrl;
        }
    }
}
