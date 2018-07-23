using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Cross.StockInfo.Model.News
{
    public enum MoneyDJNewsType
    {
        /// <summary>
        /// 全部新聞
        /// </summary>
        [Description("MB00")]
        All,

        /// <summary>
        /// 即時新聞
        /// </summary>
        [Description("MB010000")]
        Topic,

        /// <summary>
        /// 國際新聞
        /// </summary>
        [Description("MB03")]
        Global,

        /// <summary>
        /// 台股新聞
        /// </summary>
        [Description("MB06")]
        TaiwanStock,

        /// <summary>
        /// 美股新聞
        /// </summary>
        [Description("MB030100")]
        AmericanStock,

        /// <summary>
        /// 商品原物料新聞
        /// </summary>
        [Description("MB080000")]
        Product,

        /// <summary>
        /// 產業新聞
        /// </summary>
        [Description("MB07")]
        Industry,

        /// <summary>
        /// 研究報告
        /// </summary>
        [Description("MB090000")]
        Research
    }
}
