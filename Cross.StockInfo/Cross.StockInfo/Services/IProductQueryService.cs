﻿using Cross.StockInfo.Model.ProductIndex;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cross.StockInfo.Services
{
    public interface IProductQueryService
    {
        /// <summary>
        /// Query the product index historical report throught product name
        /// </summary>
        /// <param name="productName"></param>
        /// <returns></returns>
        Task<List<ProductIndexData>> ListProductIndexTaskAsync(string productName);
        /// <summary>
        /// Query the product index historical report with date time range
        /// </summary>
        /// <param name="productName"></param>
        /// <returns></returns>
        Task<List<ProductIndexData>> ListProductIndexTaskAsync(string productName, DateTime start, DateTime end);
    }
}
