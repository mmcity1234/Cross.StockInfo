using Cross.StockInfo.ViewModels.Control.Chart;
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

        protected override List<DataPoint> ProcessData(string result)
        {
            List<DataPoint> productIndexList = new List<DataPoint>();
            if (string.IsNullOrEmpty(result) && !result.Contains(" "))
                return productIndexList;

            // Split the data to get the date arry and point value array
            string[] rawData = result.Split(' ');
            string[] dateArray = rawData[0].Split(',');
            string[] valueArray = rawData[1].Split(',');

            DataPoint previousData = null;
            for (int i = 0; i < dateArray.Length && i < valueArray.Length; i++)
            {
                double currentValue = Convert.ToDouble(valueArray[i]);
                DataPoint currentIndexData = new DataPoint
                {                    
                    Time = ParseDateTime(dateArray[i]),
                    Value = currentValue,
                    ChangeValue = previousData == null ? 0 : Math.Round(currentValue - previousData.Value, 2), // float誤差需透過Math.Round去除多餘的小數點
                    ChangeValuePercentage = previousData == null ? 0 : Math.Round((currentValue - previousData.Value) / previousData.Value * 100, 2)
                };

                previousData = currentIndexData;
                productIndexList.Add(currentIndexData);
            }

            return productIndexList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        protected virtual DateTime ParseDateTime(string dateTime)
        {  
            // e.g. 1060509
            if (dateTime.Length == 7)
                return DateTime.ParseExact(dateTime, "yyyMMdd", CultureInfo.InvariantCulture).AddYears(1911);
            else
                return DateTime.Parse(dateTime, CultureInfo.InvariantCulture);
        }
    }
}
