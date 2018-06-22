using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Cross.StockInfo.ViewModels
{
    public interface IViewModel
    {
        INavigation Navigation { get; set; }
    }
}
