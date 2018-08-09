
using Cross.StockInfo.Model.Stock;
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

     
        public async Task<List<StockBase>> GetStockQueryList(string stock)
        {
            MopsRestApi api = new MopsRestApi();
            List<StockBase> stockListTask = await api.GetStockList(stock);
            return stockListTask;
        }



         
    }
}
