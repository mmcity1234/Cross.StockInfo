using Cross.StockInfo.Services;
using Cross.StockInfo.ViewModels.Chart;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Cross.StockInfo.ViewModels.ProductIndex
{
    public class BDIIndexViewModel : BaseViewModel
    {
        private LineStockChartViewModel _lineChart;
        #region Injection
        public IProductQueryService ProductService { get; set; }
        #endregion

        #region Property

        public LineStockChartViewModel LineChart
        {
            get => _lineChart;
            set
            {
                _lineChart = value;
                OnPropertyChanged();
            }
        }

        #endregion


        public BDIIndexViewModel()
        {
            LineChart = new LineStockChartViewModel();


        }
        public override async void OnPageLoading()
        {
            var bdiIndexList = await ProductService.ListBDIIndexReport();

            var bdiPoints = bdiIndexList.Select(x => new DataPoint(x.Time, x.Value)).ToList();
            LineChart.AddSeries("BDI指數", bdiPoints);

        }
    }
}
