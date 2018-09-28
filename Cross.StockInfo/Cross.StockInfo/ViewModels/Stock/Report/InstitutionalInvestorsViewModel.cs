using Cross.StockInfo.Model.Stock;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cross.StockInfo.ViewModels.Stock.Report
{
    public class InstitutionalInvestorsViewModel : BaseViewModel
    {
        /// <summary>
        /// 三大法人買賣超訊息
        /// </summary>
        public List<BuySellPriceItem> BuySellInfoCollection { get; set; }
    }
}
