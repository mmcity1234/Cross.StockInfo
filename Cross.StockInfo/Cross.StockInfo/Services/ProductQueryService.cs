using Cross.StockInfo.Assets.Strings;
using Cross.StockInfo.Model.ProductIndex;
using Cross.StockInfo.RestClient;
using Cross.StockInfo.Services.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cross.StockInfo.Services
{
    public class ProductQueryService : IProductQueryService
    {
      
        public async Task<List<ProductIndexData>> ListProductIndexTaskAsync(string productName)
        {
            DateTime end = DateTime.Now;
            DateTime start = end.AddYears(-5);
            return await ListProductIndexTaskAsync(productName, start, end);
        }

        public async Task<List<ProductIndexData>> ListProductIndexTaskAsync(string productName, DateTime start, DateTime end)
        {
            try
            {
                var productIndex = ProductFactory.GetProductIndex(productName);
                var resultIndexList = await productIndex.GetHistoricalReportTaskAsync(start, end);
                return resultIndexList;
            }
            catch (Exception e)
            {
                throw new Exception(string.Format(AppResources.Exception_QueryProductRepotError, productName, e.Message));
            }
        }
    }
}
