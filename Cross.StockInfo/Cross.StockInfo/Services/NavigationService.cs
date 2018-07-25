using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Cross.StockInfo.Services
{
    public class NavigationService : INavigationService
    {
        public void GoBack()
        {
            throw new NotImplementedException();
        }

        public async void Navigate(Type targetPage)
        {
            Page page = (Page)Activator.CreateInstance(targetPage);
            await Application.Current.MainPage.Navigation.PushAsync(page);
        }

        public async void Navigate(Type sourcePage, object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
