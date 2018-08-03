using Cross.StockInfo.Services.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cross.StockInfo.Views.Control
{
    /// <summary>
    /// 圖表K線圖的事件參數
    /// </summary>
    public class AverageTimeEventArgs : EventArgs
    {
        /// <summary>
        /// 取得或設定圖表的時間K線
        /// </summary>
        public AverageType Type { get; set; }

        public AverageTimeEventArgs(AverageType averageType)
        {
            Type = averageType;
        }
    }
}
