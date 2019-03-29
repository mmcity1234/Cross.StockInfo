using Cross.StockInfo.Assets.Strings;
using Cross.StockInfo.Model.Stock;
using Syncfusion.XForms.Buttons;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cross.StockInfo.Views.Control
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BuySellListControl : UserControlView
    {
        /// <summary>
        /// 股票買賣超清單
        /// </summary>
        public static readonly BindableProperty StockListDataProperty = BindableProperty.Create(
                "StockListData",
                typeof(List<StockBuySellItem>),
                typeof(BuySellListControl),
                defaultValue: new List<StockBuySellItem>(),
                defaultBindingMode: BindingMode.TwoWay,
                propertyChanged: OnStockListDataPropertyChanged);
        /// <summary>
        /// 買賣超按鈕切換的是件
        /// </summary>
        public event EventHandler<RankTypeEventArgs> ButtonSwitched;
        public BuySellListControl()
		{
			InitializeComponent ();
            buySellsegmentButton.ItemsSource = new ObservableCollection<SfSegmentItem>
            {
                new SfSegmentItem { Text = AppResources.OverBuy, FontSize = 16 },
                new SfSegmentItem { Text = AppResources.OverSell, FontSize = 16 }
            };
        }

        public static void OnStockListDataPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            BindablePropertyChanged<BuySellListControl>(bindable, x => x.ListViewControl.ItemsSource = newValue as List<StockBuySellItem>);
        }

        private void buySellsegmentButton_SelectionChanged(object sender, Syncfusion.XForms.Buttons.SelectionChangedEventArgs e)
        {
            if (e.Index == 0)
                ButtonSwitched?.Invoke(sender, new RankTypeEventArgs(RankType.OverBuy));
            else if(e.Index == 1)
                ButtonSwitched?.Invoke(sender, new RankTypeEventArgs(RankType.OverSell));
        }
    }
}