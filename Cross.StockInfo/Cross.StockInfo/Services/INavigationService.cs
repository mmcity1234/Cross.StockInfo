using Cross.StockInfo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cross.StockInfo.Services
{
    public interface INavigationService
    {
        Task Navigate(Type targetPage);
      
        Task Navigate(Type targetPage, object bindingContext);

        Task Navigate(Type targetPage, IViewModel bindingContext);


        void GoBack();
    }
}
