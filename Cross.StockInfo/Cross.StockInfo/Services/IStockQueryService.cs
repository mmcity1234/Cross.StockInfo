using Cross.StockInfo.Models.Mops;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cross.StockInfo.Services
{
    public interface IStockQueryService
    {
        Task<List<StockInfoModel>> GetStockQueryList(string stock);
    }
}