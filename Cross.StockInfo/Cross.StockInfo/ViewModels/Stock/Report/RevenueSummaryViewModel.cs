using Cross.StockInfo.Model.Stock;
using Cross.StockInfo.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cross.StockInfo.ViewModels.Stock.Report
{
    public class RevenueSummaryViewModel : BaseViewModel
    {
        private List<StockRevenue> _stockRevenueList;
        public IStockReportService StockReportService { get; set; }

        public List<StockRevenue> StockRevenueList
        {
            get => _stockRevenueList;
            set
            {
                _stockRevenueList = value;
                OnPropertyChanged();
            }
        }

        public override async void OnPageLoading()
        {
            IsPageLoading = true;
            base.OnPageLoading();
            StockRevenueList = await StockReportService.ListStockRevenueTaskAsync(107, 7);
            IsPageLoading = false;

        }
    }
}
