using Cross.StockInfo.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Cross.StockInfo.ViewModels.MasterDetail
{
    public class MasterPageViewModel : BaseViewModel
    {
        public DelegateCommand<SelectedItemChangedEventArgs> ItemSelectedCommand { get; set; }

        public MasterPageViewModel()
        {
            ItemSelectedCommand = new DelegateCommand<SelectedItemChangedEventArgs>(ItemSelectedEvent);
        }

        private void ItemSelectedEvent(SelectedItemChangedEventArgs args)
        {

        }

    }
}