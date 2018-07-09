using Syncfusion.RangeNavigator.XForms;
using Syncfusion.SfChart.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace Cross.StockInfo.ViewModels.Chart
{
    public class LineChartModel : BaseViewModel
    {
        private string _title = "Undefined";
        private double _latestPrice;

        #region Property

        /// <summary>
        /// 目前最新的成交價
        /// </summary>
        public double LatestPrice { get => _latestPrice;
            set
            {
                _latestPrice = value;
                OnPropertyChanged();              
            }
        }

        public ChartSeriesCollection SeriesCollection { get; set; }
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public LineChartModel()
        {
            SeriesCollection = new ChartSeriesCollection();
        }

        public void AddSeries(string label, List<DataPoint> dataPoints, bool isVisible = true)
        {
            var lineSeries = new FastLineSeries()
            {
                IsVisible = isVisible,
                ItemsSource = dataPoints,
                Label = label,
                XBindingPath = "Time",
                YBindingPath = "Value"
            };
            SeriesCollection.Add(lineSeries);
        }
    }
}
