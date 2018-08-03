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
        private const string Url = "https://just2.entrust.com.tw/Z/ZH/ZHG/CZHG.djbcd?A=131690";

        protected override string GetUrl()
        {
            return Url;
        }
    }
}
