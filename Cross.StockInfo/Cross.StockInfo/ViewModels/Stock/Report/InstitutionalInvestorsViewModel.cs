using Cross.StockInfo.Model.Stock;
using Cross.StockInfo.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.XForms.TabView;
using Cross.StockInfo.Common;
using Cross.StockInfo.ViewModels.Control;

namespace Cross.StockInfo.ViewModels.Stock.Report
{
    public class InstitutionalInvestorsViewModel : BaseViewModel
    {
        private List<BuySellPriceItem> _buySellInfoCollection;
        public IStockReportService StockReportService { get; set; }



        public DelegateCommand<SelectionChangedEventArgs> BottomTabChangedCommand { get; set; }

        /// <summary>
        /// 法人個股買賣超排名
        /// </summary>
        public StockBuySellListModel StockBuySellModel { get; set; }

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
            StockBuySellModel = new StockBuySellListModel();
            BuySellInfoCollection = new List<BuySellPriceItem>();
            BottomTabChangedCommand = new DelegateCommand<SelectionChangedEventArgs>(BottomTabChanged_EventHandler);

        }

        protected override async void OnPageFirstLoad()
        {
            IsPageLoading = true;
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;
            BuySellInfoCollection = await StockReportService.ListInvestorBuySellTaskAsync(year, month, day);
           
            IsPageLoading = false;
        }

        private async void BottomTabChanged_EventHandler(SelectionChangedEventArgs args)
        {
            if(args.Index == 1)
            {
                List<StockBuySellItem> buyResult = await StockReportService.ListForeignBuySellTaskAsync(20);
                StockBuySellModel.OverBuyList = buyResult;
            }
        }
    }
}
