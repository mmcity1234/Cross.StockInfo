using Cross.StockInfo.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cross.StockInfo.Views
{
    public interface IViewPage
    {
        IViewModel ViewModel { get; set; }

        /// <summary>
        /// For android of the hardware back button and navigation bar back button tab
        /// </summary>
        /// <returns></returns>
        void OnBackButtonTab();
    }
}
