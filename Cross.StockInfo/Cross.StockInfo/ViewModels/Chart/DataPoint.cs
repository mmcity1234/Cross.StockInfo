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
        public double ChangeValue { get; set; }

        /// <summary>
        /// 漲跌百分比
        /// </summary>
        public double ChangeValuePercentage { get; set; }

        public DataPoint() { }

        public DataPoint(DateTime time, double value)
        {
            Time = time;
            Value = value;
        }

        public DataPoint(DateTime time, double value, double changeValue, double changeValuePercentage) : this(time, value)
        {
            ChangeValue = changeValue;
            ChangeValuePercentage = changeValuePercentage;
        }
    }
}
