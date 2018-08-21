using System;
using System.Collections.Generic;
using System.Text;

namespace Cross.StockInfo.Model.Stock
{
    public class StockRevenue : StockBase
    {
        private string _comment;
        /// <summary>
        /// 當月營收
        /// </summary>
        public string CurrentRevenue { get; set; }
        /// <summary>
        /// 上月營收
        /// </summary>
        public string LastMonthRevenue { get; set; }

        /// <summary>
        /// 去年同期營收
        /// </summary>
        public string LastYearRevenue { get; set; }

        /// <summary>
        /// 上月比較增減百分比
        /// </summary>
        public string MonthOverMonthPercentage { get; set; }

        /// <summary>
        /// 去年同期增減百分比
        /// </summary>
        public string YearOnYearPercentage { get; set; }


        /// <summary>
        /// 今年累計營收
        /// </summary>
        public string CurrentAccumulatedRevenue { get; set; }

        /// <summary>
        /// 去年同期累計營收
        /// </summary>
        public string LastYearAccumulatedRevenue { get; set; }

        /// <summary>
        /// 與去年同期累計營收增減百分比
        /// </summary>
        public string AccumulatedRevenueComparePercentage { get; set; }

        /// <summary>
        /// 備註
        /// </summary>
        public string Comment
        {
            get => _comment;
            set
            {
                if (value.StartsWith("-"))
                    _comment = value.Replace("-", string.Empty);
                else
                    _comment = value;
            }
        }
    }
}
