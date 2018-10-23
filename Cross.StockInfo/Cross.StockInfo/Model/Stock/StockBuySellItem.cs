using System;
using System.Collections.Generic;
using System.Text;

namespace Cross.StockInfo.Model.Stock
{
    public class StockBuySellItem : StockBase
    {
        public int Rank { get; set; }
        /// <summary>
        /// 買賣超數量
        /// </summary>
        public int Value
        {
            get => BuyVaule - SellValue;
        }

        /// <summary>
        /// 買進數量
        /// </summary>
        public int BuyVaule { get; set; }

        /// <summary>
        /// 賣出數量
        /// </summary>
        public int SellValue { get; set; }
    }
}
