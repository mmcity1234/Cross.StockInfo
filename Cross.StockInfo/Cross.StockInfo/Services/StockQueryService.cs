using Cross.StockInfo.Models.Mops;
using Cross.StockInfo.RestClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cross.StockInfo.Services
{
    public class StockQueryService : IStockQueryService
    {

     
        public async Task<List<StockInfoModel>> GetStockQueryList(string stock)
        {
            MopsRestApi api = new MopsRestApi();
            List<StockInfoModel> stockListTask = await api.GetStockList(stock);
            return stockListTask;
        }


         
    }
}
