using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using Cross.StockInfo.Assets.Strings;
using System.Threading;
using System.Threading.Tasks;

namespace Cross.StockInfo.Services.Product
{
    public class ProductFactory
    {
        static Dictionary<string, Type> _productMapping = null;

        public static async Task<BaseProduct> GetProductIndexTaskAsync(string name)
        {
            if (_productMapping == null)
                _productMapping = await LoadTypeWithAttributeTaskAsync<StockIndexAttribute>();

            Type indexType = null;
            _productMapping.TryGetValue(name, out indexType);

            if (indexType == null)
                throw new TypeLoadException(AppResources.Exception_ProductIndexTypeLoadError);

            return Activator.CreateInstance(indexType) as BaseProduct;
        }

        private static Task<Dictionary<string, Type>> LoadTypeWithAttributeTaskAsync<T>() where T : StockIndexAttribute
        {
            return Task.Run(() =>
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
            });

        }
    }
}
