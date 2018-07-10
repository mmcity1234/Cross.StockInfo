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
        public static readonly BindableProperty DataPointsProperty = BindableProperty.Create(
                "DataPoints", 
                typeof(ChartSeriesCollection),
                typeof(LineChartControl),
                defaultValue : new ChartSeriesCollection(),
                defaultBindingMode: BindingMode.TwoWay,
                propertyChanged: OnDataPointsPropertyChanged);

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
              "Title",
              typeof(string),
              typeof(LineChartControl),
              defaultValue: string.Empty,
              defaultBindingMode: BindingMode.TwoWay,
              propertyChanged: OnTitlePropertyChanged);


        public ChartSeriesCollection DataPoints
        {
            get => (ChartSeriesCollection)GetValue(DataPointsProperty);            
            set => SetValue(DataPointsProperty, value);
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set
            {
                SetValue(TitleProperty, value);     
            }
        }

        public static void OnTitlePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as LineChartControl;
            if (control == null)
                return;
           // control.chartLine.Series = newValue as ChartSeriesCollection;
        }

        public static void OnDataPointsPropertyChanged(BindableObject bindable, object oldValue, object newValue)
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