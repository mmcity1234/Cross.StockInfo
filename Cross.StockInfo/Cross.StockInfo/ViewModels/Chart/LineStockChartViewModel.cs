using Syncfusion.RangeNavigator.XForms;
using Syncfusion.SfChart.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Cross.StockInfo.ViewModels.Chart
{
    public class LineStockChartViewModel : BaseViewModel
    {
        private string _title;

        private int _interval;

        #region Property
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
      

        public int Interval
        {
            get => _interval;
            set
            {
                _interval = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public LineStockChartViewModel()
        {
            SeriesCollection = new ChartSeriesCollection();
        }

        public void AddSeries(string label, List<DataPoint> dataPoints)
        {
            var lineSeries = new LineSeries() { ItemsSource = dataPoints, Label = label, XBindingPath = "Time", YBindingPath = "Value" };
            SeriesCollection.Add(lineSeries);
        }
    }
}
