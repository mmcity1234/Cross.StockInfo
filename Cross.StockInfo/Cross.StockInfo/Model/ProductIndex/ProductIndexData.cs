using System;
using System.Collections.Generic;
using System.Text;

namespace Cross.StockInfo.Model.ProductIndex
{
    public class ProductIndexData
    {        
        public DateTime Time { get; set; }
        public double Value { get; set; }
        /// <summary>
        /// 漲跌
        /// </summary>
        public double ChangeRange { get; set; }

        /// <summary>
        /// 漲跌百分比
        /// </summary>
        public double ChangeRangePercentage { get; set; }

    }
}
