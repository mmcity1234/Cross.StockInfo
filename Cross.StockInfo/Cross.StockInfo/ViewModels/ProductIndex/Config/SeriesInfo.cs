using System;
using System.Collections.Generic;
using System.Text;

namespace Cross.StockInfo.ViewModels.ProductIndex.Config
{
    public class SeriesInfo
    {
        public string QueryKey { get; set; }
        public string Name { get; set; }

        public bool Visible { get; set; }
        /// <summary>
        /// 是否為主要顯示的線圖
        /// </summary>
        public bool IsPrimary { get; set; }
    }
}
