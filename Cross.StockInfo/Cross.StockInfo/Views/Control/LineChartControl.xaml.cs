using Cross.StockInfo.ViewModels.Chart;
using Syncfusion.SfChart.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cross.StockInfo.Views.Control
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LineChartControl : UserControlView
    {
        public static readonly BindableProperty ChartSeriesProperty = BindableProperty.Create(
                "ChartSeries", 
                typeof(ChartSeriesCollection),
                typeof(LineChartControl),
                defaultValue : new ChartSeriesCollection(),
                defaultBindingMode: BindingMode.TwoWay,
                propertyChanged: OnSeriesPropertyChanged);       

        public ChartSeriesCollection ChartSeries
        {
            get => (ChartSeriesCollection)GetValue(ChartSeriesProperty);            
            set => SetValue(ChartSeriesProperty, value);
        }
                     
        public static void OnSeriesPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as LineChartControl;
            if (control == null)
                return;
            control.chartLine.Series = newValue as ChartSeriesCollection;
        }
      
        public LineChartControl()
		{       
			InitializeComponent ();          
        }

        protected override void OnChildAdded(Element child)
        {
            // 避免Chart元建置入於其他content page使Series為null導致sfChart render發生錯誤
            if (chartLine.Series == null)
                chartLine.Series = new ChartSeriesCollection();
            base.OnChildAdded(child);
        }
    }
}