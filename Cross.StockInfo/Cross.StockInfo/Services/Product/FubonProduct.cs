using Cross.StockInfo.Model.ProductIndex;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace Cross.StockInfo.Services.Product
{
    /// <summary>
    /// Processing the product index from fubon website
    /// </summary>
    public abstract class FubonProduct : BaseProduct
    {     

        protected override List<ProductIndexData> ProcessData(string result)
        {
            List<ProductIndexData> productIndexList = new List<ProductIndexData>();
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
