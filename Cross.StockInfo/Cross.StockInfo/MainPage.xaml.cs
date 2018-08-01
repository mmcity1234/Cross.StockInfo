using Cross.StockInfo.Common.IoC;
using Cross.StockInfo.ViewModels;
using Cross.StockInfo.Views;
using Cross.StockInfo.Views.ProductIndex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Cross.StockInfo
{
	public partial class MainPage : MasterDetailPage
    {
		public MainPage()
		{
			InitializeComponent();
            
            var _viewModel = IocProvider.Instance.Container.Resolve<MainViewModel>();   
            //_viewModel.Navigation = Application.Current.MainPage.Navigation;
            //_viewModel.NavigateTo(typeof(OilIndexView));
            BindingContext = _viewModel;
          
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Placeholder.Content = testStackLayout;
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Placeholder.Content = productStackLayout;
        }
    }
}
