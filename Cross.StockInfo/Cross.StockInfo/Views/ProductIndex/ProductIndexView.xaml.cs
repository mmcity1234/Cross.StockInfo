﻿using Cross.StockInfo.ViewModels.ProductIndex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cross.StockInfo.Views.ProductIndex
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductIndexView : ViewPage
    {
        public ProductIndexView()
        {            
            InitializeComponent();
            DataBinding<ProductIndexViewModel>();

        }
    }
}