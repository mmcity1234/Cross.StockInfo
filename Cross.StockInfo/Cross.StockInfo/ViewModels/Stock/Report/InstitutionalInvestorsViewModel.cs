using Cross.StockInfo.Model.Stock;
using Cross.StockInfo.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Cross.StockInfo.ViewModels.Stock.Report
{
    public class InstitutionalInvestorsViewModel : BaseViewModel
    {
        private List<BuySellPriceItem> _buySellInfoCollection;
        public IStockReportService StockReportService { get; set; }
        /// <summary>
        /// 三大法人買賣超訊息
        /// </summary>
        public List<BuySellPriceItem> BuySellInfoCollection
        {
            get => _buySellInfoCollection;
            set
            {
                _buySellInfoCollection = value;
                OnPropertyChanged();
            }
        }

        public InstitutionalInvestorsViewModel()
        {
            BuySellInfoCollection = new List<BuySellPriceItem>();
        }

        protected override async void OnPageFirstLoad()
        {
          
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;

            BuySellInfoCollection = await StockReportService.ListInvestorBuySellTaskAsync(year, month, day);

        }


    }
}
