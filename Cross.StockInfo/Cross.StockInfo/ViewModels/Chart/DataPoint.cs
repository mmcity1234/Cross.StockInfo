using System;
using System.Collections.Generic;
using System.Text;

namespace Cross.StockInfo.ViewModels.Chart
{
    public class DataPoint
    {
        public DateTime Time { get; set; }
        public double Value { get; set; }

        /// </summary>
        public double ChangeRange { get; set; }

        /// <summary>
        /// 漲跌百分比
        /// </summary>
        public double ChangeRangePercentage { get; set; }

        public DataPoint(DateTime time, double value)
        {
            Time = time;
            Value = value;
        }

        public DataPoint(DateTime time, double value, double changeRange, double changeRangePercentage) : this(time, value)
        {
            ChangeRange = changeRange;
            ChangeRangePercentage = ChangeRangePercentage;
        }
    }
}
