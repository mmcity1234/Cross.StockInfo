using System;
using System.Collections.Generic;
using System.Text;

namespace Cross.StockInfo.Services.Product
{
    /// <summary>
    /// 布蘭特原油期貨價
    /// </summary>
    [StockIndex("Product.BrentCrudeOilIndex")]
    public class BrentCrudeOilIndex : FubonProduct
    {
        private const string BrentCrudeOilUrl = "https://just2.entrust.com.tw/Z/ZH/ZHG/CZHG.djbcd?A=130820";
        protected override string GetUrl()
        {
            return BrentCrudeOilUrl;
        }
    }
}
