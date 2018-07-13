using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Linq;
namespace Cross.StockInfo.Common.Helper
{
    public static class EnumHelper
    {
        /// <summary>
        /// Get the enum string from Description attribute otherwise get enum as string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ParseToString(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            List<DescriptionAttribute> attributes = fi.GetCustomAttributes<DescriptionAttribute>(false).ToList();
            if (attributes.Count > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }

    }
}
