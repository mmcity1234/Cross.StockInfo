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
        public IViewModel ViewModel { get; set; }
        public ViewPage() : base() { }

        public void DataBinding<T>() where T : class, IViewModel
        {
            // Binding view model
            T _viewModel;
            using (var scope = IocProvider.Instance.Container.BeginLifetimeScope())
            {
                _viewModel = IocProvider.Instance.Container.Resolve<T>();
            }
            BindingContext = _viewModel;
            ViewModel = _viewModel;

            //_viewModel.Navigation = Application.Current.MainPage.Navigation;

            // Binding event
            Appearing += ViewPageAppearing;
            Disappearing += ViewPageDisappearing;
        }

        private void ViewPageDisappearing(object sender, EventArgs e)
        {
            ViewModel.OnPageDisappearing();
        }

        private void ViewPageAppearing(object sender, EventArgs e)
        {
            ViewModel.OnPageLoading();
        }
    }
}
