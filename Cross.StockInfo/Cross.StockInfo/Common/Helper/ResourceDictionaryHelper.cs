using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Linq;
using Cross.StockInfo.Assets.Strings;

namespace Cross.StockInfo.Common.Helper
{
    public static class ResourceDictionaryHelper
    {
        public static T GetResource<T>(string key)
        {
            return (T)Application.Current.Resources[key];
            //var mergedDict = Application.Current.Resources.MergedDictionaries
            //    .FirstOrDefault(rs => rs.ContainsKey(key));
            //if (mergedDict == null)
            //    throw new Exception(string.Format(AppResources.Exception_ResourceDictionaryKeyNotFound, key));

            //return (T)mergedDict[key];
        }
    }
}
