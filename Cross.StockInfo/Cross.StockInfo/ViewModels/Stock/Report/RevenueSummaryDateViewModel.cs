using Cross.StockInfo.Assets.Strings;
using Cross.StockInfo.Common;
using Cross.StockInfo.ViewModels.Control;
using Syncfusion.SfPicker.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Cross.StockInfo.ViewModels.Stock.Report
{
    public class RevenueSummaryDateViewModel : DatePickerModel
    {
        public DelegateCommand<EventArgs> OkButtonClickedCommand { get; set; }

        public DelegateCommand<EventArgs> CancelButtonClickedCommand { get; set; }

        public DelegateCommand<SelectionChangedEventArgs> DateSelectionChange { get; set; }

        public event Action<RevenueSummaryDateViewModel> SelectedFinish;

        public RevenueSummaryDateViewModel() : base()
        {
            OkButtonClickedCommand = new DelegateCommand<EventArgs>(OkButton_EventHandler);
            DateSelectionChange = new DelegateCommand<SelectionChangedEventArgs>(CustomDatePicker_SelectionChanged);


            SelectedDate = new ObservableCollection<object> { Convert.ToString(GetLatestRevenueYear()), Convert.ToString(GetLatestRevenueMonth()) };
            //populate Days
            DateCollection.Add(CreateYearCollection());
            DateCollection.Add(CreateMonthCollection());
        }


        public override void OnBackButtonTab()
        {
            base.OnBackButtonTab();
        }

        /// <summary>
        /// 日期選擇完成
        /// </summary>
        private void OkButton_EventHandler(EventArgs args)
        {
            try
            {
                SelectedFinish?.Invoke(this);
                Navigation.GoBack();
            }
            catch (Exception e)
            {

            }
        }
        
        private void CustomDatePicker_SelectionChanged(SelectionChangedEventArgs args)
        {
            // if year has changed
            if (args.OldValue == null || args.NewValue == null)
                return;
            if (!object.Equals((args.OldValue as IList<object>)[0], (args.NewValue as IList<object>)[0]))
            {
                CreateMonthCollection();
            }
        }

        private ObservableCollection<string> CreateMonthCollection()
        {
            Month.Clear();
            int loopMonth = 12;
            int latestMonth = GetLatestRevenueMonth();
            if(SelectedYear == Convert.ToString(System.DateTime.Now.Year) && latestMonth != 12)
                loopMonth = latestMonth;
            
            //populate months
            for (int i = 1; i <= loopMonth; i++)
            {
                Month.Add(i + AppResources.Month);
            }
            return Month;
        }

        private ObservableCollection<string> CreateYearCollection()
        {
            Year.Clear();
            int currentYear = GetLatestRevenueYear();
            //populate year        
            for (int i = currentYear; i >= currentYear - 5; i--)
            {
                Year.Add(i.ToString());
            }
            return Year;
        }

        /// <summary>
        /// 取得目前營收的年份
        /// </summary>
        /// <returns></returns>
        private int GetLatestRevenueYear()
        {
            int currentMonth = System.DateTime.Now.Month;
            int currentYear = currentMonth == 1 ? System.DateTime.Now.Year - 1 : System.DateTime.Now.Year;
            return currentYear;
        }

        private int GetLatestRevenueMonth()
        {
            // 當前日期只會公佈到上個月的營收，
            int currentMonth = System.DateTime.Now.Month == 1 ? 12 : System.DateTime.Now.Month - 1;
            return currentMonth;
        }
    }
}
