using Cross.StockInfo.ViewModels.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cross.StockInfo.Views.Stock
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StockCategoryView : ViewPage
    {
        public StockCategoryView()
        {
            InitializeComponent();
            DataBinding<StockCategoryViewModel>();

        }
    }
}