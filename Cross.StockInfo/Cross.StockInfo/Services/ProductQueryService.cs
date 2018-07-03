using Cross.StockInfo.Assets.Strings;
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
            try
            {
                var results = await ProductRestApi.GetBDIIndexReportAsync();
                return results;
            }
            catch (Exception e)
            {
                throw new Exception(string.Format(AppResources.Exception_QueryBdiRepotError, e.Message));
            }
        }

        public async Task<List<ProductIndexData>> ListBPIIndexReport()
        {
            try
            {
                var results = await ProductRestApi.GetBPIIndexReportAsync();
                return results;
            }
            catch (Exception e)
            {
                throw new Exception(string.Format(AppResources.Exception_QueryBpiRepotError, e.Message));
            }
        }
    }
}
