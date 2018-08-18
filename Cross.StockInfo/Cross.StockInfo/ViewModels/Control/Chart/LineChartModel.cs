using Syncfusion.RangeNavigator.XForms;
using Syncfusion.SfChart.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using System.Linq;

namespace Cross.StockInfo.ViewModels.Control.Chart
{
    public class LineChartModel : BaseViewModel
    {
        private string _title = "Undefined";
        private double _latestPrice;
        private ChartSeriesCollection _seriesData;
        private string _changedPrice;
        private string _changedPricePercentage;
        private PerformanceModel _indexPerformance;



        #region Property

        /// <summary>
        /// 指數績效
        /// </summary>
        public PerformanceModel IndexPerformance
        {
            get => _indexPerformance;
            set
            {
                _indexPerformance = value;
                OnPropertyChanged();
            }
        }

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
                LatestPrice = latestPoint.Value;
                ChangedPrice = latestPoint.ChangeValueLabel;
                ChangedPricePercentage = latestPoint.ChangeValuePercentageLabel;

                DateTime thisYear = new DateTime(DateTime.Now.Year, 1, 1);
                var weekAgoValue = dataPoints.First(x => x.Time >= DateTime.Today.AddDays(-7)).Value;
                var monthAgoValue = dataPoints.First(x => x.Time >= DateTime.Today.AddMonths(-1)).Value;
                var quarterAgoValue = dataPoints.First(x => x.Time >= DateTime.Today.AddMonths(-3)).Value;
                var halfYearAgoValue = dataPoints.First(x => x.Time >= DateTime.Today.AddMonths(-6)).Value;
                var thisYearAgoValue = dataPoints.First(x => x.Time >= thisYear).Value;
                var yearAgoValue = dataPoints.First(x => x.Time >= DateTime.Today.AddYears(-1)).Value;


                IndexPerformance = new PerformanceModel
                {
                    WeekPerformance = Math.Round((latestPoint.Value - weekAgoValue) / weekAgoValue * 100, 2).ToString() + "%",
                    MonthPerformance = Math.Round((latestPoint.Value - monthAgoValue) / monthAgoValue * 100, 2).ToString() + "%",
                    QuarterPerformance = Math.Round((latestPoint.Value - quarterAgoValue) / quarterAgoValue * 100, 2).ToString() + "%",
                    HalfYearPerformance = Math.Round((latestPoint.Value - halfYearAgoValue) / halfYearAgoValue * 100, 2).ToString() + "%",
                    ThisYearPerformance = Math.Round((latestPoint.Value - thisYearAgoValue) / thisYearAgoValue * 100, 2).ToString() + "%",
                    YearPerformance = Math.Round((latestPoint.Value - yearAgoValue) / yearAgoValue * 100, 2).ToString() + "%"
                };
            }

        }

        /// <summary>
        /// 清除圖表的指數線圖
        /// </summary>
        public void ClearData()
        {
            SeriesData.Clear();
        }


    }
}
