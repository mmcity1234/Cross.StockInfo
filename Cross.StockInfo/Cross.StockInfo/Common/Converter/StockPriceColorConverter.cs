﻿using Cross.StockInfo.Common.Helper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Cross.StockInfo.Common.Converter
{
    public class StockPriceColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double cellValue = System.Convert.ToDouble(value);
            if (cellValue > 0)
                return ResourceDictionaryHelper.GetResource<Color>("PriceUpColor");
            else if (cellValue < 0)
                return ResourceDictionaryHelper.GetResource<Color>("PriceDownColor");
            else
                return ResourceDictionaryHelper.GetResource<Color>("PriceUnchangedColor");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
