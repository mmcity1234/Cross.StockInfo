using Core.Utility.Network;
using Cross.StockInfo.Model.ProductIndex;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cross.StockInfo.Services.Product
{
    public abstract class BaseProduct
    {
        /// <summary>
        /// 產品指數來源網址
        /// </summary>
        /// <returns></returns>
        protected abstract string GetUrl();

        /// <summary>
        /// 網路服務回傳資料處理方式
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected abstract List<ProductIndexData> ProcessData(string result);
      

        public virtual async Task<List<ProductIndexData>> GetHistoricalReportTaskAsync(DateTime start, DateTime end)
        {
            string prodcutUrl = GetUrl();            
            string result = await RestApi.GetContentTaskAsync(GetUrl());
            return ProcessData(result);           
        }
    }
}
