using System;
using System.Collections.Generic;
using System.Text;

namespace Cross.StockInfo.ViewModels
{
    /// <summary>
    /// 定義主選單項目的參數類型，時作此介面的ViewMoele，可接收到MainView傳送過來的參數
    /// </summary>
    public interface IMenuItemData
    {
         Type ConfigParameter { get; set; }
    }
}
