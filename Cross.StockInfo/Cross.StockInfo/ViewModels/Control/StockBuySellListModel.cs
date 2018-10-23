using Cross.StockInfo.Model.Stock;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cross.StockInfo.ViewModels.Control
{
    /// <summary>
    /// 法人買賣超個股資訊
    /// </summary>
    public class StockBuySellListModel : BaseViewModel
    {
        private List<StockBuySellItem> _overBuyList;
        private List<StockBuySellItem> _overSellList;

        public List<StockBuySellItem> OverBuyList
        {
            get => _overBuyList;
            set
            {
                _overBuyList = value;
                OnPropertyChanged();
            }
        }

        public List<StockBuySellItem> OverSellList
        {
            get => _overSellList;
            set
            {
                _overSellList = value;
                OnPropertyChanged();
            }
        }


    }
}
