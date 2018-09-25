using Cross.StockInfo.Assets.Strings;
using Cross.StockInfo.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Cross.StockInfo.ViewModels.Control
{
    public class DatePickerModel : BaseViewModel
    {
        private object _selectedDate = null;

        public ObservableCollection<string> ColumnHeaders { get; set; }

        // Months API is used to modify the Day collection as per change in Month
        // internal Dictionary<string, string> Months { get; set; }
        /// <summary>
        /// Date is the actual DataSource for SfPicker control which will holds the collection of Day ,Month and Year
        /// </summary>
        /// <value>The date.</value>
        public ObservableCollection<object> DateCollection { get; set; }

        /// <summary>
        /// Day is the collection of day numbers Month is the collection of Month Names
        /// </summary>
        public ObservableCollection<string> Month { get; set; }

        public ObservableCollection<string> Year { get; set; }

        public object SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged();
            }
        }


        public string SelectedYear
        {
            get
            {
                IList<object> date = SelectedDate as IList<object>;
                if (SelectedDate != null && date != null)
                {
                    string year = ((string)date[0]).Replace(AppResources.Year, string.Empty);
                    return year;
                }
                else
                {
                    return null;
                }

            }
        }

        public string SelectedMonth
        {
            get
            {
                IList<object> date = SelectedDate as IList<object>;
                if (SelectedDate != null && date != null)
                {
                    string month = ((string)date[1]).Replace(AppResources.Month, string.Empty);
                    return month;
                }
                else
                {
                    return null;
                }
            }
        }


        public DatePickerModel()
        {

            //  Months = new Dictionary<string, string>();
            DateCollection = new ObservableCollection<object>();
            Month = new ObservableCollection<string>();
            Year = new ObservableCollection<string>();
            ColumnHeaders = new ObservableCollection<string>();
            ColumnHeaders.Add(AppResources.Year);
            ColumnHeaders.Add(AppResources.Month);

            // this.SelectionChanged += CustomDatePicker_SelectionChanged;
        }
    }
}
