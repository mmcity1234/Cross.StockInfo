using System;
using System.Collections.Generic;
using System.Text;

namespace Cross.StockInfo.Model.Stock.Category
{
    /// <summary>
    /// 股市產業分類項目
    /// </summary>
    public class StockCategory
    {
        public string Name { get; set; }
        public string Url { get; set; }

        /// <summary>
        /// 產業別細項
        /// </summary>
        public List<StockCategory> DetailItems { get; set; } = new List<StockCategory>();

        public void AddDetailRange(List<StockCategory> list)
        {           
            DetailItems.AddRange(list);   
        }
    }
}
