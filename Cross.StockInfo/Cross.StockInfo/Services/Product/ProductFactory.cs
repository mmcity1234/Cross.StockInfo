using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using Cross.StockInfo.Assets.Strings;

namespace Cross.StockInfo.Services.Product
{
    public class ProductFactory
    {
        static Dictionary<string, Type> _productMapping = null;

        public static BaseProduct GetProductIndex(string name)
        {
            if (_productMapping == null)
                _productMapping = LoadTypeWithAttribute<StockIndexAttribute>();

            Type indexType = null;
            _productMapping.TryGetValue(name, out indexType);

            if (indexType == null)
                throw new TypeLoadException(AppResources.Exception_ProductIndexTypeLoadError);
               
            return Activator.CreateInstance(indexType) as BaseProduct;
        }

        private static Dictionary<string, Type> LoadTypeWithAttribute<T>() where T : StockIndexAttribute
        {
            Dictionary<string, Type> _typeMapping = new Dictionary<string, Type>();
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
            {
                foreach (StockIndexAttribute productAtt in type.GetCustomAttributes<T>())
                {
                    _typeMapping.Add(productAtt.Name, type);
                }
            }
            return _typeMapping;
        }
    }
}
