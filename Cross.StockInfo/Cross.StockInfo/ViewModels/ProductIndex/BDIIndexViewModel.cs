using Cross.StockInfo.Services;
using Cross.StockInfo.ViewModels.Chart;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Cross.StockInfo.Assets.Strings;
using Xamarin.Forms;
using System.Threading.Tasks;
using Cross.StockInfo.ViewModels.Control;
using System.Collections.ObjectModel;

namespace Cross.StockInfo.ViewModels.ProductIndex
{
    public class BDIIndexViewModel : BaseViewModel
    {
        private LineChartModel _lineChart;
        private bool _isLoaded = false;
        private DailyPriceControlModel _priceContorlModel;

        #region Injection
        public IProductQueryService ProductService { get; set; }
        #endregion

        #region Property

        public DailyPriceControlModel PriceContorlModel
        {
            get => _priceContorlModel;
            set
            {
                _priceContorlModel = value;
                OnPropertyChanged();
            }
        }

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
            if (!_isLoaded)
            {
                try
                {
                    var bdiIndexList = await ProductService.ListProductIndexTaskAsync("Product.BdiIndex");
                    var bpiIndexList = await ProductService.ListProductIndexTaskAsync("Product.BpiIndex");

                    AddSeries(AppResources.BDIIndex_Label, bdiIndexList);
                    AddSeries(AppResources.BPIIndex_Label, bpiIndexList, false);
                    var filterBdi = bdiIndexList.OrderByDescending(x => x.Time).Take(60);

                    PriceContorlModel = new DailyPriceControlModel { DataPoints = new ObservableCollection<DataPoint>(filterBdi) };

                    _isLoaded = true;
                } 
                catch(Exception e)
                {
                    throw new Exception(string.Format(AppResources.Exception_LoadDataError, e.Message));
                }
            }
        }

        private void AddSeries(string title, List<DataPoint> dataList, bool isVisible = true)
        {
            var dataPoints = dataList.Select(x => new DataPoint(x.Time, x.Value, x.ChangeValue, x.ChangeValuePercentage)).ToList();
            LineChart.AddSeries(title, dataPoints, isVisible);
        }
    }
}
