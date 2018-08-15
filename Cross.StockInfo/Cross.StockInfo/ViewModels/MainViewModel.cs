using Cross.StockInfo.Assets.Strings;
using Cross.StockInfo.Common;
using Cross.StockInfo.Services;
using Cross.StockInfo.ViewModels.Control.MasterDetail;
using Cross.StockInfo.ViewModels.ProductIndex;
using Cross.StockInfo.ViewModels.ProductIndex.Config;
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
            IsShowMasterDetail = true;
        }

        private void MasterDetailItemSelectedEvent(SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as MasterPageMenuItem;
            if (item != null)
            {
                NavigateTo(item.TargetType, item.ConfigsType);
                SelectedMenuItem = null;
                IsShowMasterDetail = false;
            }

        }

        /// <summary>
        /// Navigate to the content page
        /// </summary>
        /// <param name="pageType"></param>
        /// <param name="configType">頁面內容資訊設定</param>
        public void NavigateTo(Type pageType, Type configType)
        {
            try
            {
                Page currentPage = (Page)Activator.CreateInstance(pageType);   
                if(currentPage.BindingContext is IMenuItemData)
                {
                    if (configType == null)
                        throw new Exception(AppResources.Exception_Internal_ConfigTypeNotAssigned);
                    ((IMenuItemData)currentPage.BindingContext).ConfigParameter =configType;
                }

                NavigationPage navPage = (Application.Current.MainPage as NavigationPage);
                (navPage.CurrentPage as MainPage).Detail = new NavigationPage(currentPage);
            }
            catch (Exception e)
            {

            }
        }

    }

}
