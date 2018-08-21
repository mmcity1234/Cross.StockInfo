using Cross.StockInfo.Assets.Strings;
using Cross.StockInfo.Common;
using Cross.StockInfo.Model.Stock;
using Cross.StockInfo.Services;
using Cross.StockInfo.ViewModels.Stock.Report.Config;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Cross.StockInfo.ViewModels.Stock.Report
{
    public class OperatorModel
    {
        public string Name { get; set; }
        public OperatorType Value { get; set; }
    }

    public class RevenueSummaryViewModel : BaseViewModel, IMenuItemData
    {
        private string _searchText;
        private string _selectedMonth;
        private string _selectedYear;
        private List<StockRevenue> _stockRevenueList;


        public IStockReportService StockReportService { get; set; }
        public Type ConfigParameter { get; set; }

        #region ViewModel



        public RevenueSummaryFilterData FilterData { get; set; }
        /// <summary>
        /// 取得或設定運算子名稱清單
        /// </summary>
        public List<OperatorModel> ComboBoxStringList { get; set; }

        /// <summary>
        /// 選擇查詢營收的月份
        /// </summary>
        public string SelectedMonth
        {
            get => _selectedMonth;
            set
            {
                _selectedMonth = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// 選擇查詢營收的年份
        /// </summary>
        public string SelectedYear
        {
            get => _selectedYear;
            set
            {
                _selectedYear = value;
                OnPropertyChanged();
            }
        }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged();
            }
        }

        public List<string> MonthCollection
        {
            get
            {
                return new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
            }
        }

        public List<string> YearCollection
        {
            get
            {
                List<string> yearCollection = new List<string>();
                int currentYear = DateTime.Now.Year;
                for(int i= 0; i < 5; i++)
                {
                    yearCollection.Add(Convert.ToString(currentYear--));
                }
                return yearCollection;
            }
        }

        /// <summary>
        /// 所有上市櫃公司營收清單
        /// </summary>
        public List<StockRevenue> StockRevenueList
        {
            get => _stockRevenueList;
            set
            {
                _stockRevenueList = value;
                OnPropertyChanged();
            }
        }

        #endregion


        public RevenueSummaryViewModel()
        {
            ComboBoxStringList = new List<OperatorModel>
            {
                new OperatorModel { Name = AppResources.MoreThan, Value = OperatorType.MoreThan },
                new OperatorModel { Name = AppResources.LessThan, Value = OperatorType.LessThan }
            };
            FilterData = new RevenueSummaryFilterData();
        }

        public override async void OnPageLoading()
        {
            IsPageLoading = true;
            base.OnPageLoading();
            if (ConfigParameter == typeof(OtcRevenueType))
                StockRevenueList = await StockReportService.ListOtcRevenueTaskAsync(107, 7);
            else if (ConfigParameter == typeof(CompanyRevenueType))
                StockRevenueList = await StockReportService.ListCompaynRevenueTaskAsync(107, 7);
            IsPageLoading = false;

        }


        /// <summary>
        /// Handler the datagrid predicate condiction
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public bool FilterRecordCondition(object obj)
        {
            bool isMonValid = true, isYearValid = true, isAccumulatedValid = true;
            var stockInfo = obj as StockRevenue;
            if (stockInfo == null)
                return false;

            if (!stockInfo.DisplayLabel.Contains(SearchText))
                return false;
            if (FilterData.IsEnableMonthOverMonthFilter)
                isMonValid = ChechFilterValue(stockInfo.MonthOverMonthPercentage, FilterData.MonthOverMonthPercentageFilter, FilterData.SelectedMonthOverMonthOperator.Value);
            if (FilterData.IsEnableYearOnYearFilter)
                isYearValid = ChechFilterValue(stockInfo.YearOnYearPercentage, FilterData.YearOnYearPercentageFilter, FilterData.SelectedYearOnYearOperator.Value);
            if (FilterData.IsEnableAccumulatedRevenueFilter)
                isAccumulatedValid = ChechFilterValue(stockInfo.AccumulatedRevenueComparePercentage, FilterData.AccumulatedRevenueComparePercentageFilter, FilterData.SelectedAccumulatedRevenueCompareOperator.Value);

            return isMonValid & isYearValid & isAccumulatedValid;
        }

        private bool ChechFilterValue(string value, int filterValue, OperatorType operatorType)
        {
            double checkedValue;
            bool isSuccess = double.TryParse(value, out checkedValue);

            if (isSuccess == false)
                return false;
            if (operatorType == OperatorType.MoreThan)
                return checkedValue > filterValue;
            else if (operatorType == OperatorType.LessThan)
                return checkedValue < filterValue;
            else
                return false;
        }
    }
}
