using Cross.StockInfo.ViewModels.Stock.Report;
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
    public partial class RevenueSummaryView : ViewPage
    {
        public RevenueSummaryView()
        {
            InitializeComponent();
            DataBinding<RevenueSummaryViewModel>();
            RevenueSummaryViewModel viewModel = base.ViewModel as RevenueSummaryViewModel;
            if(viewModel != null)
            {
                viewModel.FilterChanged += ViewModel_FilterChanged;
            }
        }

        private void ViewModel_FilterChanged(RevenueSummaryFilterViewModel obj)
        {
            OnFilterChanged();
        }

        private void SearchBar_TextChanged(object sender, EventArgs e)
        {
            OnFilterChanged();
        }
        private void OnFilterChanged()
        {
            if (revenueDataGrid.View != null)
            {
                this.revenueDataGrid.View.Filter = ((RevenueSummaryViewModel)ViewModel).FilterRecordCondition;
                this.revenueDataGrid.View.RefreshFilter();
            }
        }     
    }
}