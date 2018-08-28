using Cross.StockInfo.ViewModels;
using Cross.StockInfo.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Cross.StockInfo.Services
{
    public class NavigationService : INavigationService
    {
        public void GoBack()
        {
            throw new NotImplementedException();
        }

        public async Task Navigate(Type targetPage)
        {
            await Navigate(targetPage, null);
        }

        public async Task Navigate(Type targetPage, IViewModel bindingContext)
        {
            IViewPage page = (IViewPage)Activator.CreateInstance(targetPage);
            page.ViewModel = bindingContext;
            await Application.Current.MainPage.Navigation.PushAsync((Page)page, true);
        }

        public async Task Navigate(Type targetPage, object bindingContext)
        {
            Page page = (Page)Activator.CreateInstance(targetPage);
            page.BindingContext = bindingContext;
            await Application.Current.MainPage.Navigation.PushAsync(page, true);
        }
    }
}
