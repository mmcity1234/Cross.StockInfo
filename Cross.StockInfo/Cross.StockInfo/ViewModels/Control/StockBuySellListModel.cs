using Cross.StockInfo.Common;
using Cross.StockInfo.Model.Stock;
using Syncfusion.XForms.Buttons;
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
     

        public List<StockBuySellItem> OverBuyList { get; set; }
        public List<StockBuySellItem> OverSellList { get; set; }


    }
}
