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
        private ChartSeriesCollection _seriesData;
        private string _changedPrice;
        private string _changedPricePercentage;

        #region Property

        /// <summary>
        /// 目前最新的成交價
        /// </summary>
        public double LatestPrice
        {
            get => _latestPrice;
            set
            {
                _latestPrice = value;
                OnPropertyChanged();
            }
        }

        public ChartSeriesCollection SeriesData
        {
            get => _seriesData;
            set
            {
                _seriesData = value;
                OnPropertyChanged();
            }
        }
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public string ChangedPrice
        {
            get => _changedPrice;
            set
            {
                _changedPrice = value;
                OnPropertyChanged();
            }
        }

        public string ChangedPricePercentage
        {
            get => _changedPricePercentage;
            set
            {
                _changedPricePercentage = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public LineChartModel()
        {
            SeriesData = new ChartSeriesCollection();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="label"></param>
        /// <param name="dataPoints"></param>
        /// <param name="isVisible"></param>
        public void AddSeries(string label, List<DataPoint> dataPoints, bool isVisible = true)
        {
            if (dataPoints == null)
                return;

            var lineSeries = new FastLineSeries()
            {
                IsVisible = isVisible,
                ItemsSource = dataPoints,
                Label = label,
                XBindingPath = "Time",
                YBindingPath = "Value"
            };
            SeriesData.Add(lineSeries);
        }

        /// <summary>
        /// Add the primary line data to the chart and show the info on the char control
        /// </summary>
        public void AddPrimarySeries(string label, List<DataPoint> dataPoints)
        {            
            AddSeries(label, dataPoints, true);
            if (dataPoints != null && dataPoints.Count > 0)
            {
               
                var latestPoint = dataPoints[dataPoints.Count - 1];
                string prefix = latestPoint.Value >= 0 ? "+" : "-"; 
                LatestPrice = latestPoint.Value;
                ChangedPrice = prefix + Convert.ToString(latestPoint.ChangeValue);
                ChangedPricePercentage = prefix + Convert.ToString(latestPoint.ChangeValuePercentage) + "%";
            }

        }


    }
}
