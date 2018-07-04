using System;
using System.Collections.Generic;
using System.Text;

namespace Cross.StockInfo.Services.Product
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class StockIndexAttribute : Attribute
    {
        public string Name { get; set; }
       
        public StockIndexAttribute(string name)
        {
            Name = name;
        }
    }
}
