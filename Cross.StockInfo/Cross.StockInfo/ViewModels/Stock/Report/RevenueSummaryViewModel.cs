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
    public class RevenueSummaryViewModel : BaseViewModel, IMenuItemData
    {
        private string _searchText;


        private List<StockRevenue> _stockRevenueList;
        public IStockReportService StockReportService { get; set; }
     

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;                
                OnPropertyChanged();
            }
        }
        

        public List<StockRevenue> StockRevenueList
        {
            get => _stockRevenueList;
            set
            {
                _stockRevenueList = value;
                OnPropertyChanged();
            }
        }

        public Type ConfigParameter { get; set; }

        public RevenueSummaryViewModel()
        {
         
        }

        public override async void OnPageLoading()
        {
            IsPageLoading = true;
            base.OnPageLoading();
            if(ConfigParameter == typeof(OtcRevenueType))
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
            var stockInfo = obj as StockRevenue;
            if (stockInfo == null)
                return false;

            return stockInfo.DisplayLabel.Contains(SearchText);
        }
    }
}
