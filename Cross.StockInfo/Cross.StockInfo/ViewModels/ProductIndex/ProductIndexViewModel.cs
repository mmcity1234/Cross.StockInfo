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
using System.Threading;

namespace Cross.StockInfo.ViewModels.ProductIndex
{
    public class ProductIndexViewModel : BaseViewModel, IMenuItemData
    {
        private LineChartModel _lineChart;
        /// <summary>
        /// 是否第一次載入頁面
        /// </summary>
        private bool _isFirstLoad = true;
        private DailyPriceControlModel _priceContorlModel;
        private string _chartTitle;
        private ProductInfo _productItemInfo;
        // Cancel token for line chart loading
        CancellationTokenSource _tokenSource = null;

        #region Injection
        public IProductQueryService ProductService { get; set; }
        #endregion


        #region Property
        public Type ConfigParameter
        {
            get => throw new NotImplementedException();
            set
            {
                if (!value.IsSubclassOf(typeof(ProductInfo)))
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
            IsPageLoading = true;

        }
        public override async void OnPageLoading()
        {
            if (_isFirstLoad)
            {
                try
                {
                    IsPageLoading = true;
                    CancelChartLoading();                
                    await LoadChartData(AverageType.Day);                    
                    IsPageLoading = false;
                    _isFirstLoad = false;
                }
                catch (Exception e)
                {
                    throw new Exception(string.Format(AppResources.Exception_LoadDataError, e.Message));
                }
            }
        }

        private void CancelChartLoading()
        {
            if (_tokenSource != null)
                _tokenSource.Cancel();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="averageType"></param>
        /// <returns></returns>
        /// <exception cref="OperationCanceledException"></exception>
        private Task LoadChartData(AverageType averageType)
        {
            _tokenSource = new CancellationTokenSource();
            var localTokenSource = _tokenSource;

            return Task.Run(() =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    LineChart.ClearData();

                    List<DataPoint> filterSeriesForDailyPrice = new List<DataPoint>();
                    foreach (var series in ProductInfo.SeriesInfoCollection)
                    {
                        var indexList = await ProductService.ListProductIndexTaskAsync(series.QueryKey, averageType);
                        // if task had cancel
                        if (localTokenSource.Token.IsCancellationRequested)
                            return;
                        if (series.IsPrimary) // primary series
                        {
                            filterSeriesForDailyPrice = indexList;
                            AddSeries(series.Name, indexList, true, series.Visible);
                        }
                        else   // sub series                         
                            AddSeries(series.Name, indexList, false, series.Visible);
                    }

                    var filterBdi = filterSeriesForDailyPrice.OrderByDescending(x => x.Time).Take(60);

                    PriceContorlModel = new DailyPriceControlModel { Title = ProductInfo.DailyPriceTitle, DataPoints = new ObservableCollection<DataPoint>(filterBdi) };

                  
                });
            }, localTokenSource.Token);
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
            if (_isFirstLoad)
                return;
            try
            {
                
                IsPageLoading = true;
                CancelChartLoading();
                await LoadChartData(args.Type);
                IsPageLoading = false;
            }
            catch (Exception e)
            {
                throw new Exception(string.Format(AppResources.Exception_LoadDataError, e.Message));
            }
        }
    }
}
