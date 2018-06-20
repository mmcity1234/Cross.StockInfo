using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cross.StockInfo.Common.IoC
{
    public class IocProvider 
    {
        
        public static IocProvider Instance { get; set; }

       
        public IContainerProvider Container { get; private set; }

        static IocProvider()
        {
            Instance = new IocProvider();
        }

        public void InitializeContainer(IContainerProvider container)
        {
            Container = container;
        }
    }
}
