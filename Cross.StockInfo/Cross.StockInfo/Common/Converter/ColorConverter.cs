using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Cross.StockInfo.Common.Converter
{

    public class ColorConverter : IValueConverter
    {     
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {  
                var converter = new ColorTypeConverter();
                return converter.ConvertFromInvariantString((string)value);
            }
            else
                return Color.Default;
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
