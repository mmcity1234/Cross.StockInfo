using Cross.StockInfo.ViewModels;
using Cross.StockInfo.Views;
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
            BindingContext = new MainViewModel();
		}
	}
}
