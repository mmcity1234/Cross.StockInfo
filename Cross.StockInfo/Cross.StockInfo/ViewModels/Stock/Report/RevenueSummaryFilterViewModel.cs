using System;
using System.Collections.Generic;
using System.Text;

namespace Cross.StockInfo.ViewModels.Stock.Report
{
    /// <summary>
    /// 上市櫃公司營收過濾篩選設定參數
    /// </summary>
    public class RevenueSummaryFilterViewModel : BaseViewModel
    {     

        public bool IsEnableMonthOverMonthFilter { get; set; }
        public bool IsEnableYearOnYearFilter { get; set; }
        public bool IsEnableAccumulatedRevenueFilter { get; set; }

        /// <summary>
        /// 取得或設定上月營收增減百分比運算子
        /// </summary>
        public OperatorModel SelectedMonthOverMonthOperator { get; set; }
       
        /// <summary>
        /// 取得或設定去年同期營收增減百分比運算子
        /// </summary>
        public OperatorModel SelectedYearOnYearOperator { get; set; }
        /// <summary>
        /// 取得或設定當年營收累計增減百分比運算子
        /// </summary>
        public OperatorModel SelectedAccumulatedRevenueCompareOperator { get; set; }


        /// <summary>
        /// 取得或設定上月營收增減百分比過濾條件
        /// </summary>
        public int MonthOverMonthPercentageFilter { get; set; }
        /// <summary>
        /// 取得或設定去年同期營收增減百分比過濾條件
        /// </summary>
        public int YearOnYearPercentageFilter { get; set; }

        /// <summary>
        /// 取得或設定當年累計營收增減百分比過濾條件
        /// </summary>
        public int AccumulatedRevenueComparePercentageFilter { get; set; }

    }
}
