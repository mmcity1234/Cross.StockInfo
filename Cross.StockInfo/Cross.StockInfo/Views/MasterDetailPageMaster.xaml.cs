using Cross.StockInfo.ViewModels;
using Cross.StockInfo.ViewModels.MasterDetail;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cross.StockInfo.Views
{    
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailPageMaster : ViewPage
    {
        public ListView ListView;

        public MasterDetailPageMaster()
        {
            InitializeComponent();
            DataBinding<MasterPageViewModel>();
        }
    }
}