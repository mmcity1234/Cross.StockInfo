using Core.Utility.Network;
using Cross.StockInfo.ViewModels.Control.Chart;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cross.StockInfo.Services.Product
{

    /// <summary>
    /// 圖表的K線圖統計
    /// </summary>
    public enum AverageType
    {
        /// <summary>
        /// 日K線圖
        /// </summary>
        Day,
        /// <summary>
        /// 週K線圖
        /// </summary>
        Week
    }
    public abstract class BaseProduct
    {
        /// <summary>
        /// 產品指數來源網址
        /// </summary>
        /// <returns></returns>
        protected abstract string GetUrl(AverageType averageType);

        /// <summary>
        /// 網路服務回傳資料處理方式
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected abstract List<DataPoint> ProcessData(string result);
      

        public virtual async Task<List<DataPoint>> GetHistoricalReportTaskAsync(DateTime start, DateTime end, AverageType averageType = AverageType.Day)
        {
            string prodcutUrl = GetUrl(averageType);            
            string result = await RestApi.GetContentTaskAsync(GetUrl(averageType));
            return ProcessData(result);           
        }
    }
}
