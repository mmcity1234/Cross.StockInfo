﻿using Syncfusion.RangeNavigator.XForms;
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
             
        #endregion

        public LineChartModel()
        {
            SeriesCollection = new ChartSeriesCollection();
        }

        public void AddSeries(string label, List<DataPoint> dataPoints)
        {
            var lineSeries = new FastLineSeries() { ItemsSource = dataPoints, Label = label, XBindingPath = "Time", YBindingPath = "Value" };
            SeriesCollection.Add(lineSeries);
        }
    }
}
