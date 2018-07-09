using Cross.StockInfo.Common;
using Cross.StockInfo.Model.Mops;
using Cross.StockInfo.Services;
using Cross.StockInfo.ViewModels.Control.MasterDetail;
using Cross.StockInfo.Views.ProductIndex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Cross.StockInfo.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private MasterPageMenuItem _selectedMenuItem;     
        private bool _isShowMasterDetail;
        

        public IStockQueryService StockService { get; set; }

        #region Binding Property
        public bool IsShowMasterDetail
        {
            get => _isShowMasterDetail;
            set
            {
                _isShowMasterDetail = value;
                OnPropertyChanged();
            }
        }
        public MasterPageMenuItem SelectedMenuItem
        {
            get => _selectedMenuItem;
            set
            {
                _selectedMenuItem = value;
                OnPropertyChanged();
            }
        }      
        #endregion
    
        public DelegateCommand<SelectedItemChangedEventArgs> MasterDetailItemSelectedCommand { get; set; }

        public MainViewModel()
        {           
            MasterDetailItemSelectedCommand = new DelegateCommand<SelectedItemChangedEventArgs>(MasterDetailItemSelectedEvent);
            // NavigateTo(typeof(GoldIndexView));
        }

        private void MasterDetailItemSelectedEvent(SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as MasterPageMenuItem;
            if (item != null)
            {
                NavigateTo(item.TargetType);
                SelectedMenuItem = null;
                IsShowMasterDetail = false;
            }
        }

        /// <summary>
        /// Navigate to the content page
        /// </summary>
        /// <param name="pageType"></param>
        public void NavigateTo(Type pageType)
        {
            (Application.Current.MainPage as MasterDetailPage).Detail =
                new NavigationPage((Page)Activator.CreateInstance(pageType));
        }

    }

}
