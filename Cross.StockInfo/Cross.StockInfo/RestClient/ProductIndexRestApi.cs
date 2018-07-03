﻿using Core.Utility.Network;
using Cross.StockInfo.Model.ProductIndex;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace Cross.StockInfo.RestClient
{
    public class ProductIndexRestApi
    {
        /// <summary>
        /// 散裝貨運BDI指數
        /// </summary>
        private const string BDIIndexUrl = "https://fubon-ebrokerdj.fbs.com.tw/Z/ZN/ZNM/CZNM.djbcd?A=FM400007";
        /// <summary>
        /// 散裝貨運巴拿馬指數
        /// </summary>
        private const string BPIIndexUrl = "https://fubon-ebrokerdj.fbs.com.tw/Z/ZN/ZNM/CZNM.djbcd?A=FM400008";
        public async Task<List<ProductIndexData>> GetBDIIndexReportAsync()
        {
            return await GetProductHistoricalReportAsync(BDIIndexUrl);
        }
        public async Task<List<ProductIndexData>> GetBPIIndexReportAsync()
        {
            return await GetProductHistoricalReportAsync(BPIIndexUrl);
        }

        private async Task<List<ProductIndexData>> GetProductHistoricalReportAsync(string url)
        {
            List<ProductIndexData> productIndexList = new List<ProductIndexData>();
            string result = await RestApi.GetContentTaskAsync(BDIIndexUrl);

            if (string.IsNullOrEmpty(result) && !result.Contains(" "))
                return productIndexList;

            // Split the data to get the date arry and point value array
            string[] rawData = result.Split(' ');
            string[] dateArray = rawData[0].Split(',');
            string[] valueArray = rawData[1].Split(',');

            ProductIndexData previousData = null;
            for (int i = 0; i < dateArray.Length && i < valueArray.Length; i++)
            {
                double currentValue = Convert.ToDouble(valueArray[i]);
                ProductIndexData currentIndexData = new ProductIndexData
                {
                    Time = DateTime.ParseExact(dateArray[i], "yyyMMdd", CultureInfo.InvariantCulture).AddYears(1911),
                    Value = currentValue,
                    ChangeRange = previousData == null ? 0 : currentValue - previousData.Value,
                    ChangeRangePercentage = previousData == null ? 0 : Math.Round((currentValue - previousData.Value) / previousData.Value * 100, 2)
                };

                previousData = currentIndexData;
                productIndexList.Add(currentIndexData);
            }

            return productIndexList;
        }

    }
}
