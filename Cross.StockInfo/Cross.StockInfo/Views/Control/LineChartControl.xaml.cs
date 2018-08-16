using Cross.StockInfo.Assets.Styles;
using Cross.StockInfo.Common.Helper;
using Cross.StockInfo.Services.Product;
using Cross.StockInfo.ViewModels.Control.Chart;
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
        /// <summary>
        /// 選擇圖表均線的事件
        /// </summary>
        public event EventHandler<AverageTimeEventArgs> AverageTimeSelected;

        /// <summary>
        /// 歷史資料點
        /// </summary>
        public static readonly BindableProperty SeriesDataProperty = BindableProperty.Create(
                "SeriesData",
                typeof(ChartSeriesCollection),
                typeof(LineChartControl),
                defaultValue: new ChartSeriesCollection(),
                defaultBindingMode: BindingMode.TwoWay,
                propertyChanged: OnSeriesDataPropertyChanged);


        /// <summary>
        /// 圖表標題
        /// </summary>
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
                "Title",
                typeof(string),
                typeof(LineChartControl),
                defaultValue: string.Empty,
                defaultBindingMode: BindingMode.OneWay);
        /// <summary>
        /// 最近成交價
        /// </summary>
        public static readonly BindableProperty LatestPriceProperty = BindableProperty.Create(
               "LatestPrice",
               typeof(string),
               typeof(LineChartControl),
               defaultValue: string.Empty,
               defaultBindingMode: BindingMode.OneWay,
               propertyChanged: OnLatestPricePropertyChanged);

        /// <summary>
        /// 漲跌價
        /// </summary>
        public static readonly BindableProperty ChangedPriceProperty = BindableProperty.Create(
                "ChangedPrice",
                typeof(string),
                typeof(LineChartControl),
                defaultValue: string.Empty,
                defaultBindingMode: BindingMode.OneWay,
                propertyChanged: OnChangedPricePropertyChanged);
        /// <summary>
        /// 漲跌幅
        /// </summary>
        public static readonly BindableProperty ChangedPricePercentageProperty = BindableProperty.Create(
                "ChangedPricePercentage",
                typeof(string),
                typeof(LineChartControl),
                defaultValue: string.Empty,
                defaultBindingMode: BindingMode.OneWay,
                propertyChanged: OnChangedPricePercentagePropertyChanged);

        public static readonly BindableProperty PerformanceProperty = BindableProperty.Create(
               "Performance",
               typeof(PerformanceModel),
               typeof(LineChartControl),
               defaultValue: null,
               defaultBindingMode: BindingMode.OneWay,
               propertyChanged: OnPerformancePropertyChanged);

        public static readonly BindableProperty TimeFormatProperty = BindableProperty.Create(
               "TimeFormat",
               typeof(string),
               typeof(LineChartControl),
               defaultValue: "yyyy/MM/dd",
               defaultBindingMode: BindingMode.OneWay);

        public ChartSeriesCollection SeriesData
        {
            get => (ChartSeriesCollection)GetValue(SeriesDataProperty);
            set => SetValue(SeriesDataProperty, value);
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public string LatestPrice
        {
            get => (string)GetValue(LatestPriceProperty);
            set => SetValue(LatestPriceProperty, value);
        }

        public string ChangedPrice
        {
            get => (string)GetValue(ChangedPriceProperty);
            set => SetValue(ChangedPriceProperty, value);

        }
        public string ChangedPricePercentage
        {
            get => (string)GetValue(ChangedPricePercentageProperty);
            set => SetValue(ChangedPricePercentageProperty, value);
        }

        /// <summary>
        /// 績效
        /// </summary>
        public PerformanceModel Performance
        {
            get => (PerformanceModel)GetValue(PerformanceProperty);
            set => SetValue(PerformanceProperty, value);
        }
        public string TimeFormat
        {
            get => (string)GetValue(TimeFormatProperty);
            set => SetValue(TimeFormatProperty, value);
        }

        public static void OnLatestPricePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            BindablePropertyChanged<LineChartControl>(bindable, x => x.latestPriceLabel.Text = newValue as string);
        }

        public static void OnSeriesDataPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            BindablePropertyChanged<LineChartControl>(bindable, x => x.chartLine.Series = newValue as ChartSeriesCollection);
        }

        private static void OnChangedPricePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            BindablePropertyChanged<LineChartControl>(bindable, x =>
            {
                x.changedPriceLabel.Text = newValue as string;
                double value;
                if (newValue != null && double.TryParse((string)newValue, out value))
                {
                    x.changedPriceLabel.TextColor = value >= 0 ? ResourceDictionaryHelper.GetResource<Color>("PriceUpColor") :
                       ResourceDictionaryHelper.GetResource<Color>("PriceDownColor");
                }
            });

        }
        private static void OnChangedPricePercentagePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            BindablePropertyChanged<LineChartControl>(bindable, x =>
            {
                x.changedPricePercentageLabel.Text = newValue as string;
                FillPercentageTextColor(x.changedPricePercentageLabel, newValue as string);
            });
        }
        private static void OnPerformancePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            BindablePropertyChanged<LineChartControl>(bindable, x =>
            {
                var model = newValue as PerformanceModel;
                if (model != null)
                {
                    x.weekPerformanceLabel.Text = model.WeekPerformance;
                    x.monthPerformanceLabel.Text = model.MonthPerformance;
                    x.quarterPerformanceLabel.Text = model.QuarterPerformance;
                    x.halfYearPerformanceLabel.Text = model.HalfYearPerformance;
                    x.thisYearPerformanceLabel.Text = model.ThisYearPerformance;
                    x.yearPerformanceLabel.Text = model.YearPerformance;

                    FillPercentageTextColor(x.weekPerformanceLabel, model.WeekPerformance);
                    FillPercentageTextColor(x.monthPerformanceLabel, model.MonthPerformance);
                    FillPercentageTextColor(x.quarterPerformanceLabel, model.QuarterPerformance);
                    FillPercentageTextColor(x.halfYearPerformanceLabel, model.HalfYearPerformance);
                    FillPercentageTextColor(x.thisYearPerformanceLabel, model.ThisYearPerformance);
                    FillPercentageTextColor(x.yearPerformanceLabel, model.YearPerformance);
                }
            });
        }

        private static void FillPercentageTextColor(Label targetLabel, string strValue)
        {
            if (strValue != null)
            {
                double value;
                string origValueString = ((string)strValue).Replace("%", string.Empty);
                double.TryParse(origValueString, out value);
                targetLabel.TextColor = value >= 0 ? ResourceDictionaryHelper.GetResource<Color>("PriceUpColor") :
                    ResourceDictionaryHelper.GetResource<Color>("PriceDownColor");
            }
        }


        public LineChartControl()
        {
            InitializeComponent();
        }

        protected override void OnChildAdded(Element child)
        {
            // 避免Chart元建置入於其他content page使Series為null導致sfChart render發生錯誤
            if (chartLine.Series == null)
                chartLine.Series = new ChartSeriesCollection();
            base.OnChildAdded(child);
        }

        private void TimePeriodButton_SelectionChanged(object sender, Syncfusion.XForms.Buttons.SelectionChangedEventArgs e)
        {
            if (e.Index == 0)
                AverageTimeSelected(sender, new AverageTimeEventArgs(AverageType.Day));
            else if (e.Index == 1)
                AverageTimeSelected(sender, new AverageTimeEventArgs(AverageType.Week));
        }
    }
}