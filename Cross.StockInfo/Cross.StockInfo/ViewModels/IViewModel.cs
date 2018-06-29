using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Cross.StockInfo.ViewModels
{
    public interface IViewModel
    {
        //INavigation Navigation { get; set; }

        /// <summary>
        /// When the page enter and start to load
        /// </summary>
        void OnPageLoading();

        /// <summary>
        /// When the page is start left
        /// </summary>
        void OnPageDisappearing();
    }
}
