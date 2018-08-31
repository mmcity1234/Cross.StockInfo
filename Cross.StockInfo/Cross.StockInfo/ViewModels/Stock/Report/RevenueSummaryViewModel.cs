using Cross.StockInfo.Assets.Strings;
using Cross.StockInfo.Common;
using Cross.StockInfo.Common.IoC;
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
        /// <summary>
        /// 設定目前UI控制向的啟用狀態，確保頁面載入過程不可操作此UI控制向
        /// </summary>
        private bool _isControlEnable;
      
        private List<StockRevenue> _stockRevenueList;
        private RevenueSummaryFilterViewModel _filterModel;

        public event Action<RevenueSummaryFilterViewModel> FilterChanged;

        public IStockReportService StockReportService { get; set; }
        public Type ConfigParameter { get; set; }

        #region ViewModel

        /// <summary>
        /// 控制項目前的啟用狀況
        /// </summary>
        public bool IsControlEnable
        {
            get => _isControlEnable;
            set
            {
                _isControlEnable = value;
                OnPropertyChanged();
            }
        }

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
                for (int i = 0; i < 5; i++)
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

        public DelegateCommand<EventArgs> FilterImageButtonCommand { get; set; }

        #endregion


        public RevenueSummaryViewModel()
        {           
            FilterImageButtonCommand = new DelegateCommand<EventArgs>(FilterImageClick_EventHandler);
        }

        protected override async void OnPageFirstLoad()
        {  
            SetViewStatus(true);
            base.OnPageFirstLoad();
            if (ConfigParameter == typeof(OtcRevenueType))
                StockRevenueList = await StockReportService.ListOtcRevenueTaskAsync(107, 7);
            else if (ConfigParameter == typeof(CompanyRevenueType))
                StockRevenueList = await StockReportService.ListCompaynRevenueTaskAsync(107, 7);
            SetViewStatus(false);          
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

            if (SearchText != null && !stockInfo.DisplayLabel.Contains(SearchText))
                return false;
            if (_filterModel != null)
            {
                if (_filterModel.IsEnableMonthOverMonthFilter)
                    isMonValid = CheckFilterValue(stockInfo.MonthOverMonthPercentage, _filterModel.MonthOverMonthPercentageFilter, _filterModel.SelectedMonthOverMonthOperator.Value);
                if (_filterModel.IsEnableYearOnYearFilter)
                    isYearValid = CheckFilterValue(stockInfo.YearOnYearPercentage, _filterModel.YearOnYearPercentageFilter, _filterModel.SelectedYearOnYearOperator.Value);
                if (_filterModel.IsEnableAccumulatedRevenueFilter)
                    isAccumulatedValid = CheckFilterValue(stockInfo.AccumulatedRevenueComparePercentage, _filterModel.AccumulatedRevenueComparePercentageFilter, _filterModel.SelectedAccumulatedRevenueCompareOperator.Value);
            }
            return isMonValid & isYearValid & isAccumulatedValid;
        }

        private bool CheckFilterValue(string value, int filterValue, OperatorType operatorType)
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

        private void FilterImageClick_EventHandler(EventArgs args)
        {
            if (_filterModel == null)
            {
                _filterModel = IocProvider.Instance.Container.Resolve<RevenueSummaryFilterViewModel>();               
                _filterModel.FilterValueChangedFinish += FilterViewModel_FilterValueChangedFinish;
            }

            Navigation.Navigate(typeof(Views.Stock.Report.RevenueSummaryFilterView), _filterModel);
        }

        private void FilterViewModel_FilterValueChangedFinish(RevenueSummaryFilterViewModel model)
        {
            _filterModel = model;

            // triggle the data grid filter 
            FilterChanged?.Invoke(model);          
        }

        private void SetViewStatus(bool isPageLoading)
        {
            IsPageLoading = isPageLoading;
            // Disable the control when page is loading
            IsControlEnable = !isPageLoading;
        }

    }
}
