using Cross.StockInfo.Model.Stock;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cross.StockInfo.Services
{
    /// <summary>
    /// 查詢台股營收與財報等整體資訊服務
    /// </summary>
    public interface IStockReportService
    {
        /// <summary>
        /// List all of the revenue of stock
        /// </summary>
        /// <param name="year">民國</param>
        /// <param name="month"></param>
        /// <returns></returns>
        Task<List<StockRevenue>> ListStockRevenueTaskAsync(int year, int month);
    }
}
