using Cross.StockInfo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cross.StockInfo.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WebView : ViewPage
	{
		public WebView ()
		{
			InitializeComponent ();
            DataBinding<WebViewModel>();
		}

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    var model = BindingContext as WebViewModel;
        //    if(model != null)
        //    {                
        //        webViewControl.Source = model.SourceUrl;                
        //    }
        //}

    }
}