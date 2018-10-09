using System;
using System.Collections.Generic;
using System.Text;

namespace Cross.StockInfo.Model.Stock
{
    /// <summary>
    /// 法人買賣超金額
    /// </summary>
    public class BuySellPriceItem
    {
        public string Date { get; set; }

        /// <summary>
        /// 外資買賣超
        /// </summary>
        public string ForeignBuySell { get; set; }

        /// <summary>
        /// 投信買賣超
        /// </summary>
        public string InvestmentBuySell { get; set; }

        /// <summary>
        /// 自營商買賣超
        /// </summary>
        public string DealerBuySell { get; set; }
    }
}
