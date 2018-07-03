using Cross.StockInfo.Services;
using Cross.StockInfo.ViewModels.Chart;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Cross.StockInfo.Assets.Strings;
using Xamarin.Forms;
using System.Threading.Tasks;
using Cross.StockInfo.Model.ProductIndex;

namespace Cross.StockInfo.ViewModels.ProductIndex
{
    public class BDIIndexViewModel : BaseViewModel
    {
        private LineChartModel _lineChart;
        #region Injection
        public IProductQueryService ProductService { get; set; }
        #endregion

        #region Property

        public LineChartModel LineChart
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
            LineChart = new LineChartModel();
            LineChart.Title = AppResources.BDIIndex_ChartTitle;
        }
        public override async void OnPageLoading()
        {
            var bdiIndexList = await ProductService.ListBDIIndexReport();
            var bpiIndexList = await ProductService.ListBPIIndexReport();

            AddSeries(AppResources.BDIIndex_Label, bdiIndexList);
            AddSeries(AppResources.BPIIndex_Label, bpiIndexList);
        }

        private void AddSeries(string title, List<ProductIndexData> dataList)
        {
            var dataPoints = dataList.Select(x => new DataPoint(x.Time, x.Value, x.ChangeRange, x.ChangeRangePercentage)).ToList();
            LineChart.AddSeries(AppResources.BDIIndex_Label, dataPoints);
        }
    }
}
