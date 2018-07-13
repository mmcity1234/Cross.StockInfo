using System;
using System.Collections.Generic;
using System.Text;

namespace Cross.StockInfo.ViewModels.Control.Chart
{
    public class DataPoint
    {
        private string _changeValueLabel;
        private string _changeValuePercentageLabel;

        public DateTime Time { get; set; }
        public double Value { get; set; }

        /// <summary>
        /// 價格漲跌值
        /// </summary>
        public double ChangeValue { get; set; }

        /// <summary>
        /// 漲跌百分比
        /// </summary>
        public double ChangeValuePercentage { get; set; }

        /// <summary>
        /// 價格漲跌的顯示字串
        /// </summary>
        public string ChangeValueLabel
        {
            get
            {
                if(_changeValueLabel == null)
                    _changeValueLabel = prefixString(ChangeValue) + ChangeValue;
                return _changeValueLabel;
            }
        }
        public string ChangeValuePercentageLabel
        {
            get
            {
                if(_changeValuePercentageLabel == null)
                    _changeValuePercentageLabel = prefixString(ChangeValuePercentage) + ChangeValuePercentage + "%";
                return _changeValuePercentageLabel;
            }
        }

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

        private string prefixString(double value)
        {
            return value >= 0 ? "+" : string.Empty;
        }
    }
}
