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
using Cross.StockInfo.Views.Control;

namespace Cross.StockInfo.ViewModels.Stock.Report
{
    public class InstitutionalInvestorsViewModel : BaseViewModel
    {
        /// <summary>
        /// 法人買賣超金額
        /// </summary>
        private List<BuySellPriceItem> _buySellInfoCollection;
        /// <summary>
        /// 外資股票買賣超資訊紀錄
        /// </summary>
        private StockBuySellListModel _foreignBuySellModel;

        private StockBuySellListModel _dealerBuySellModel;

        private List<StockBuySellItem> _foreignCurrentSelectList;
        private List<StockBuySellItem> _dealerCurrentSelectList;

        public IStockReportService StockReportService { get; set; }

        public DelegateCommand<RankTypeEventArgs> ForignStockRankSwitchCommand { get; set; }
        public DelegateCommand<RankTypeEventArgs> DealerStockRankSwitchCommand { get; set; }
        public DelegateCommand<SelectionChangedEventArgs> BottomTabChangedCommand { get; set; }

        /// <summary>
        /// 目前外資買超或賣超股票排名
        /// </summary>
        public List<StockBuySellItem> ForeignCurrentSelectList
        {
            get => _foreignCurrentSelectList;
            set
            {
                _foreignCurrentSelectList = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// 目前自營商買超或賣超股票排名
        /// </summary>
        public List<StockBuySellItem> DealerCurrentSelectList
        {
            get => _dealerCurrentSelectList;
            set
            {
                _dealerCurrentSelectList = value;
                OnPropertyChanged();
            }
        }

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
            BottomTabChangedCommand = new DelegateCommand<SelectionChangedEventArgs>(BottomTabChanged_EventHandler);
            ForignStockRankSwitchCommand = new DelegateCommand<RankTypeEventArgs>(ForeignRankStockButtonChanged_EventHandler);
            DealerStockRankSwitchCommand = new DelegateCommand<RankTypeEventArgs>(DealerRankStockButtonChanged_EventHandler);
        }

        protected override async void OnPageFirstLoad()
        {
            IsPageLoading = true;
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;
            BuySellInfoCollection = await StockReportService.ListBuySellTaskAsync(year, month, day);


            IsPageLoading = false;
        }

        #region Event Handler
        /// <summary>
        /// 外資股票買賣超切換按鈕事件
        /// </summary>
        /// <param name="args"></param>
        private void ForeignRankStockButtonChanged_EventHandler(RankTypeEventArgs args)
        {
            if (_foreignBuySellModel == null)
                return;
            if (args.Type == RankType.OverBuy)
                ForeignCurrentSelectList = _foreignBuySellModel.OverBuyList;
            else
                ForeignCurrentSelectList = _foreignBuySellModel.OverSellList;
        }
        /// <summary>
        /// 自營商股票買賣超切換按鈕事件
        /// </summary>
        /// <param name="args"></param>
        private void DealerRankStockButtonChanged_EventHandler(RankTypeEventArgs args)
        {
            if (_dealerBuySellModel == null)
                return;
            if (args.Type == RankType.OverBuy)
                DealerCurrentSelectList = _dealerBuySellModel.OverBuyList;
            else
                DealerCurrentSelectList = _dealerBuySellModel.OverSellList;
        }

        private async void BottomTabChanged_EventHandler(SelectionChangedEventArgs args)
        {
            if (args.Index == 1)
            {
                if (_foreignBuySellModel != null)
                    return;
                IsPageLoading = true;
                _foreignBuySellModel = await StockReportService.ListForeignStockRankTaskAsync(20);
                ForeignCurrentSelectList = _foreignBuySellModel.OverBuyList;
                IsPageLoading = false;
            }
            else if (args.Index == 2)
            {
                if (_dealerBuySellModel != null)
                    return;
                IsPageLoading = true;
                _dealerBuySellModel = await StockReportService.ListDealerStockRankTaskAsync(20);
                DealerCurrentSelectList = _dealerBuySellModel.OverBuyList;
                IsPageLoading = false;
            }
        }
        #endregion
    }
}
