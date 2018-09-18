using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cross.StockInfo.Views.Stock.Report
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RevenueSummaryDateView : ViewPage
	{
		public RevenueSummaryDateView ()
		{
			InitializeComponent ();
		}

        private void date_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void date_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }
    }
}