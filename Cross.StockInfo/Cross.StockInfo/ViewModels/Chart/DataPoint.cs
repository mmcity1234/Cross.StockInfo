using System;
using System.Collections.Generic;
using System.Text;

namespace Cross.StockInfo.ViewModels.Chart
{
    public class DataPoint
    {
        public DateTime Time { get; set; }
        public double Value { get; set; }

        public DataPoint(DateTime time, double value)        {

            Time = time;
            Value = value;
        }
    }
}
