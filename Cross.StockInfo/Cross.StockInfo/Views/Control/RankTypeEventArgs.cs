using System;
using System.Collections.Generic;
using System.Text;

namespace Cross.StockInfo.Views.Control
{
    public enum RankType
    {
        OverBuy,
        OverSell
    }
    public class RankTypeEventArgs : EventArgs
    {
        public RankType Type { get; set; }

        public RankTypeEventArgs(RankType rankType)
        {
            Type = rankType;
        }
    }
}
