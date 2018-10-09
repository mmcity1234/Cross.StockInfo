using Cross.StockInfo.Model.Stock;
using Cross.StockInfo.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cross.StockInfo.ViewModels.Stock.Report
{
    public class InstitutionalInvestorsViewModel : BaseViewModel
    {
        public IStockReportService StockReportService { get; set; }
        /// <summary>
        /// 三大法人買賣超訊息
        /// </summary>
        public List<BuySellPriceItem> BuySellInfoCollection { get; set; }

        public InstitutionalInvestorsViewModel()
        {

        }

        protected override async void OnPageFirstLoad()
        {
            BuySellInfoCollection.Clear();
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;

            var results = await StockReportService.ListInvestorBuySellTaskAsync(year, month, day);
            BuySellInfoCollection.AddRange(results);
        }


    }
}
