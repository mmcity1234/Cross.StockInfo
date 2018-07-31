using Cross.StockInfo.ViewModels.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cross.StockInfo.Views.News
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewsView : TabViewPage
    {
		public NewsView ()
		{
			InitializeComponent ();
            DataBinding<NewsViewModel>();          
        }

        private void newsViewPage_CurrentPageChanged(object sender, EventArgs e)
        {

        }
    }
}