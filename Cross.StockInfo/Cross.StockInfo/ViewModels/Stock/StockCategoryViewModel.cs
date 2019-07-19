using Cross.StockInfo.Model.Stock.Category;
using Cross.StockInfo.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cross.StockInfo.ViewModels.Stock
{
    public class StockCategoryViewModel : BaseViewModel
    {
        private List<StockCategory> _categoryList = new List<StockCategory>();

        public IStockQueryService stockQueryService { get; set; }

        #region ViewModel

        public List<StockCategory> StockCategoryList
        {
            get => _categoryList;
            set
            {
                _categoryList = value;
                OnPropertyChanged();
            }
        }

        #endregion

        protected override async void OnPageFirstLoad()
        {
            base.OnPageFirstLoad();
            try
            {
                IsPageLoading = true;
                StockCategoryList = await stockQueryService.ListStockCtegory();


                IsPageLoading = false;
            }
            catch (Exception e)
            {

            }
        }

    }
}
