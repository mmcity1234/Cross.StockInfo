﻿using Cross.StockInfo.Common.IoC;
using Cross.StockInfo.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Cross.StockInfo.Views
{
    public class TabViewPage : TabbedPage, IViewPage
    {
        private IViewModel _viewModel;
        public IViewModel ViewModel
        {
            get => _viewModel;
            set
            {
                _viewModel = value;
                BindingContext = _viewModel;
            }
        }
        public TabViewPage() : base() { }

        public void DataBinding<T>() where T : class, IViewModel
        {
            try
            {
                // Binding view model
                T _viewModel;
                using (var scope = IocProvider.Instance.Container.BeginLifetimeScope())
                {
                    _viewModel = IocProvider.Instance.Container.Resolve<T>();
                }            
                ViewModel = _viewModel;

                //_viewModel.Navigation = Application.Current.MainPage.Navigation;

                // Binding event
                Appearing += ViewPageAppearing;
                Disappearing += ViewPageDisappearing;
            }
            catch (Exception e)
            {

            }
        }

        private void ViewPageDisappearing(object sender, EventArgs e)
        {
            base.OnDisappearing();
            ViewModel.OnPageDisappearing();
        }

        private void ViewPageAppearing(object sender, EventArgs e)
        {
            base.OnAppearing();
            ViewModel.OnPageLoading();
        }

        /// <summary>
        /// Only for UWP back button
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed()
        {
            if (Device.RuntimePlatform == Device.UWP)
            {
                ViewModel?.OnBackButtonTab();
            }
            return base.OnBackButtonPressed();
        }

        /// <summary>
        /// For android  of the hardware back button and navigation bar back button tab
        /// </summary>
        /// <returns></returns>
        public void OnBackButtonTab()
        {
            ViewModel?.OnBackButtonTab();
        }
    }
}
