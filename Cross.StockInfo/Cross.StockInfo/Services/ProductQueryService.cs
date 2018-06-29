using Cross.StockInfo.Model.ProductIndex;
using Cross.StockInfo.RestClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cross.StockInfo.Services
{
    public class ProductQueryService : IProductQueryService
    {
        public ProductIndexRestApi ProductRestApi { get; set; }
        public async Task<List<ProductIndexData>> ListBDIIndexReport()
        {
            var results = await ProductRestApi.GetBDIIndexReportAsync();
            return results;

        }
    }
}
