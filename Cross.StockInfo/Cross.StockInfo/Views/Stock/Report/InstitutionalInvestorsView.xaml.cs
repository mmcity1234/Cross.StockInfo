﻿using Cross.StockInfo.ViewModels.Stock.Report;
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
	public partial class InstitutionalInvestorsView : ViewPage
	{
		public InstitutionalInvestorsView ()
		{
			InitializeComponent ();
            DataBinding<InstitutionalInvestorsViewModel>();

        }
	}
}