using Cross.StockInfo.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cross.StockInfo.Views
{
    public interface IViewPage
    {
        IViewModel ViewModel { get; set; }
    }
}
