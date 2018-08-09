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
        /// For the news item list loading
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

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// When page is start show and triger the method
        /// </summary>
        public virtual void OnPageLoading()
        {
            
        }

        public virtual void OnPageDisappearing()
        {
           
        }
    }
}
