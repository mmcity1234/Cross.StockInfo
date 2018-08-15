using Cross.StockInfo.Services;
using Cross.StockInfo.ViewModels.Control.Chart;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Cross.StockInfo.Assets.Strings;
using Xamarin.Forms;
using System.Threading.Tasks;
using Cross.StockInfo.ViewModels.Control;
using System.Collections.ObjectModel;
using Cross.StockInfo.ViewModels.ProductIndex.Config;
using Cross.StockInfo.Common;
using Cross.StockInfo.Views.Control;
using Cross.StockInfo.Services.Product;

namespace Cross.StockInfo.ViewModels.ProductIndex
{
    public class ProductIndexViewModel : BaseViewModel, IMenuItemData
    {
        private LineChartModel _lineChart;
        private bool _isLoaded = false;
        private DailyPriceControlModel _priceContorlModel;
        private string _chartTitle;
        private ProductInfo _productItemInfo;

        #region Injection
        public IProductQueryService ProductService { get; set; }
        #endregion


        #region Property
        public Type ConfigParameter
        {
            get => throw new NotImplementedException();
            set {
                if(!value.IsSubclassOf(typeof(ProductInfo)))
                    throw new Exception(AppResources.Exception_Internal_ProductInfoNotAssigned);
                ProductInfo = (ProductInfo)Activator.CreateInstance(value);
            }
        }

        /// <summary>
        /// 取得或設定商品指數頁面的資訊描述內容
        /// </summary>
        public ProductInfo ProductInfo
        {
            get => _productItemInfo;
            set
            {
                _productItemInfo = value;
                ChartTitle = _productItemInfo.ChartTitle;
            }
        }
        #endregion

        #region ViewModel
        public string ChartTitle
        {
            get => _chartTitle;
            set
            {
                _chartTitle = value;
                OnPropertyChanged();
            }
        }
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
        public DelegateCommand<AverageTimeEventArgs> AverageSelectedCommand { get; set; }
        


        #endregion

        public ProductIndexViewModel()
        {
            LineChart = new LineChartModel();
            AverageSelectedCommand = new DelegateCommand<AverageTimeEventArgs>(AverageSelectedEventHandler);


        }
        public override async void OnPageLoading()
        {
            if (!_isLoaded)
            {
                try
                {
                    await LoadChartData(AverageType.Day);
                }
                catch (Exception e)
                {
                    throw new Exception(string.Format(AppResources.Exception_LoadDataError, e.Message));
                }
            }
        }

        private async Task LoadChartData(AverageType averageType)
        {
            //return Task.Run(() =>
            //{
            //    Device.BeginInvokeOnMainThread(async() => {
                    LineChart.ClearData();

                    int count = 1;
                    List<DataPoint> filterSeriesForDailyPrice = new List<DataPoint>();
                    foreach (var series in ProductInfo.SeriesInfoCollection)
                    {
                        var indexList = await ProductService.ListProductIndexTaskAsync(series.QueryKey, averageType);
                        if (count++ == 1) // primary series
                        {
                            filterSeriesForDailyPrice = indexList;
                            AddSeries(series.Name, indexList, true, series.Visible);
                        }
                        else   // sub series                         
                            AddSeries(series.Name, indexList, false, series.Visible);
                    }

                    var filterBdi = filterSeriesForDailyPrice.OrderByDescending(x => x.Time).Take(60);
                    
                    PriceContorlModel = new DailyPriceControlModel { Title = ProductInfo.DailyPriceTitle, DataPoints = new ObservableCollection<DataPoint>(filterBdi) };

                    _isLoaded = true;
            //    });
            //});
        }

        private void AddSeries(string title, List<DataPoint> dataList, bool isPrimary = true, bool isVisible = true)
        {
            var dataPoints = dataList.Select(x => new DataPoint(x.Time, x.Value, x.ChangeValue, x.ChangeValuePercentage)).ToList();
            if (isPrimary)
                LineChart.AddPrimarySeries(title, dataPoints);
            else
                LineChart.AddSeries(title, dataPoints, isVisible);
        }

        /// <summary>
        /// 圖表選擇移動平均統計方式的事件
        /// </summary>
        private async void AverageSelectedEventHandler(AverageTimeEventArgs args)
        {
            await LoadChartData(args.Type);
        }
    }
}
