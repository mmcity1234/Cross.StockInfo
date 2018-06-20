using Cross.StockInfo.Common;
using Cross.StockInfo.Models.Mops;
using Cross.StockInfo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cross.StockInfo.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private List<StockInfoModel> _suggestionItemsSource = null;
        private string _searchStockText = null;

        public IStockQueryService StockService { get; set; }

        public INavigationService NavigationService { get; set; }

        #region Model Binding
     

    
        #endregion

        public MainViewModel()
        {
      
        }

      
     

    }
}
