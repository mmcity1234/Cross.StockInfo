using Cross.StockInfo.Common.IoC;
using Cross.StockInfo.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Cross.StockInfo.Views
{
    public class ViewPage : ContentPage
    {

        public void DataBinding<T>() where T : class, IViewModel
        {
            T _viewModel;
            using (var scope = IocProvider.Instance.Container.BeginLifetimeScope())
            {
                _viewModel = IocProvider.Instance.Container.Resolve<T>();
            }
            BindingContext = _viewModel;
        }
    }
}
