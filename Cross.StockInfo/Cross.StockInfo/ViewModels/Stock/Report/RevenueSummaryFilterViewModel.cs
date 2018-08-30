using Cross.StockInfo.Assets.Strings;
using Cross.StockInfo.Common;
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
        private OperatorModel _selectedMonthOverMonthOperator;
        private OperatorModel _selectedYearOnYearOperator;
        private OperatorModel _selectedAccumulatedRevenueCompareOperator;

        /// <summary>
        /// 取得或設定運算子名稱清單
        /// </summary>
        public List<OperatorModel> ComboBoxStringList { get; set; }

        public event Action<RevenueSummaryFilterViewModel> FilterValueChangedFinish;

        public bool IsEnableMonthOverMonthFilter { get; set; }
        public bool IsEnableYearOnYearFilter { get; set; }
        public bool IsEnableAccumulatedRevenueFilter { get; set; }

        /// <summary>
        /// 取得或設定上月營收增減百分比運算子
        /// </summary>
        public OperatorModel SelectedMonthOverMonthOperator
        {
            get => _selectedMonthOverMonthOperator;
            set
            {
                _selectedMonthOverMonthOperator = value; OnPropertyChanged();
            }
        }

        /// <summary>
        /// 取得或設定去年同期營收增減百分比運算子
        /// </summary>
        public OperatorModel SelectedYearOnYearOperator
        {
            get => _selectedYearOnYearOperator;
            set
            {
                _selectedYearOnYearOperator = value; OnPropertyChanged();
            }
        }
        /// <summary>
        /// 取得或設定當年營收累計增減百分比運算子
        /// </summary>
        public OperatorModel SelectedAccumulatedRevenueCompareOperator
        {
            get => _selectedAccumulatedRevenueCompareOperator;
            set
            {
                _selectedAccumulatedRevenueCompareOperator = value; OnPropertyChanged();
            }
        }


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

        public DelegateCommand<EventArgs> ConfirmCommand { get; set; }

        public RevenueSummaryFilterViewModel()
        {
            OperatorModel moreThan = new OperatorModel { Name = AppResources.MoreThan, Value = OperatorType.MoreThan };
            ComboBoxStringList = new List<OperatorModel>
            {
                moreThan,
                new OperatorModel { Name = AppResources.LessThan, Value = OperatorType.LessThan }
            };
            ConfirmCommand = new DelegateCommand<EventArgs>(OkButton_EventHandler);

            SelectedMonthOverMonthOperator = moreThan;
            SelectedYearOnYearOperator = moreThan;
            SelectedAccumulatedRevenueCompareOperator = moreThan;
        }

        private void OkButton_EventHandler(EventArgs args)
        {
            FilterValueChangedFinish?.Invoke(this);
        }
    }
}
