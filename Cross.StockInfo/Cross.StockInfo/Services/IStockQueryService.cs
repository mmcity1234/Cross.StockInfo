using Cross.StockInfo.Model.Stock;
using Cross.StockInfo.Model.Stock.Category;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cross.StockInfo.Services
{
    /// <summary>
    /// 查詢個股資訊
    /// </summary>
    public interface IStockQueryService
    {
        /// <summary>
        /// 取得股票產業分類清單
        /// </summary>
        /// <returns></returns>
        Task<List<StockCategory>> ListStockCtegory();
    }
}