using Cross.StockInfo.Model.ProductIndex;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cross.StockInfo.Services
{
    public interface IProductQueryService
    {
        Task<List<ProductIndexData>> ListBDIIndexReport();

        Task<List<ProductIndexData>> ListBPIIndexReport();
    }
}
