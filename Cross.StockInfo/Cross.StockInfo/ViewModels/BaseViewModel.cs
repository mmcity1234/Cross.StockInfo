using Cross.StockInfo.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace Cross.StockInfo.ViewModels
{
    public class BaseViewModel : IViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _isPageLoading;

        /// <summary>
        ///  Check if the page is first time to load
        /// </summary>
        protected bool IsPageFirstLoad { get; private set; }

        /// <summary>
        /// Get or set the page loading bar status
        /// </summary>
        public bool IsPageLoading
        {
            get => _isPageLoading;
            set
            {
                _isPageLoading = value;
                OnPropertyChanged();
            }
        }

        #region Inection
        /// <summary>
        /// Get or set the navigation service 
        /// </summary>
        public INavigationService Navigation { get; set; }
        #endregion

        public BaseViewModel()
        {
            IsPageFirstLoad = true;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// When page is start show and triger the method
        /// </summary>
        public void OnPageLoading()
        {
            if (IsPageFirstLoad)
            {
                OnPageFirstLoad();
                IsPageFirstLoad = false;
            }
            OnPageLoaded();
        }

        /// <summary>
        /// 只有第一次頁面載入才會觸發執行
        /// </summary>
        protected virtual void OnPageFirstLoad()
        {

        }

        /// <summary>
        /// 頁面每次載入皆會觸發執行
        /// </summary>
        protected virtual void OnPageLoaded()
        {

        }

        public virtual void OnPageDisappearing()
        {
           
        }


        /// <summary>
        /// Handle the both of hardware back button and navigation bar back button
        /// </summary>
        public virtual void OnBackButtonTab()
        {

        }
    }
}
