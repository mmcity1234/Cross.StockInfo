using Cross.StockInfo.Assets.Strings;
using Cross.StockInfo.Common;
using Cross.StockInfo.ViewModels.Control;
using Syncfusion.SfPicker.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;

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
            //populate Days
            var yearCollection = CreateYearCollection();
            var monthCollection = CreateMonthCollection();

            SelectedDate = new ObservableCollection<object>
            {
                yearCollection[0],
                monthCollection[monthCollection.Count - 1]
            };
        }

        protected override void OnPageLoaded()
        {
            base.OnPageLoaded();
            ////populate Days
            //var yearCollection = CreateYearCollection();
            //var monthCollection = CreateMonthCollection();

            //SelectedDate = new ObservableCollection<object>
            //{
            //    yearCollection[0],
            //    monthCollection[monthCollection.Count - 1]
            //};

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
            var tt = args.OldValue as IList<object>;

            var oldValue = args.OldValue as IList<object>;
            var newValue = args.NewValue as IList<object>;
           
            if (oldValue != null && oldValue.Count != 0 && 
                newValue != null && newValue.Count != 0 && 
                !object.Equals(oldValue[0], newValue[0]))
            {
                var monthCollection = CreateMonthCollection();
            
                string matchedMonthObj = monthCollection.FirstOrDefault(x => x.Contains(SelectedMonth));
                SelectedDate[1] = matchedMonthObj ?? monthCollection[monthCollection.Count -1];
            }
        }

        private ObservableCollection<string> CreateMonthCollection()
        {
            Month = new ObservableCollection<string>();
            int loopMonth = 12;
            int latestMonth = GetLatestRevenueMonth();
            if(SelectedYear == null || (SelectedYear == Convert.ToString(System.DateTime.Now.Year) && latestMonth != 12))
                loopMonth = latestMonth;
            
            //populate months
            for (int i = 1; i <= loopMonth; i++)
            {               
                Month.Add((i < 10 ? "0" + i : Convert.ToString(i)) + AppResources.Month);
            }
            if (DateCollection.Count > 1)
                DateCollection.RemoveAt(1);
            DateCollection.Insert(1, Month);

            return Month;
        }

        private ObservableCollection<string> CreateYearCollection()
        {
            Year = new ObservableCollection<string>();
            int currentYear = GetLatestRevenueYear();
            //populate year        
            for (int i = currentYear; i >= currentYear - 5; i--)
            {
                Year.Add(i.ToString());
            }

            if (DateCollection.Count > 0)
                DateCollection.RemoveAt(0);
            DateCollection.Insert(0, Year);

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
