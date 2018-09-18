using Cross.StockInfo.Assets.Strings;
using Syncfusion.SfPicker.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Cross.StockInfo.Views.Control.DateTime
{
    public class DatePickerControl : SfPicker
    {

        public ObservableCollection<string> Headers { get; set; }

        // Months API is used to modify the Day collection as per change in Month

        internal Dictionary<string, string> Months { get; set; }

        /// <summary>
        /// Date is the actual DataSource for SfPicker control which will holds the collection of Day ,Month and Year
        /// </summary>
        /// <value>The date.</value>
        public ObservableCollection<object> Date { get; set; }

        /// <summary>
        /// Day is the collection of day numbers Month is the collection of Month Names
        /// </summary>
        internal ObservableCollection<string> Month { get; set; }

        internal ObservableCollection<string> Year { get; set; }

        public DatePickerControl()
        {
            Months = new Dictionary<string, string>();
            Date = new ObservableCollection<object>();
            Month = new ObservableCollection<string>();
            Year = new ObservableCollection<string>();
            Headers = new ObservableCollection<string>();
            Headers.Add(AppResources.Year);
            Headers.Add(AppResources.Month);

            PopulateDateCollection();
            this.ItemsSource = Date;
           // this.SelectionChanged += CustomDatePicker_SelectionChanged;
        }

        private void PopulateDateCollection()
        {
            //populate months
            for (int i = 1; i < 13; i++)
            {
                Month.Add(i + AppResources.Month);
            }
            //populate year
            int currentYear = System.DateTime.Now.Year;
            for (int i = currentYear ; i >= currentYear - 5; i--)
            {
                Year.Add(i.ToString());
            }

            //populate Days
            Date.Add(Year);
            Date.Add(Month);
        }
        //private void CustomDatePicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{

        //    try
        //    {
        //        bool update = false;
        //        if (e.OldValue != null && e.NewValue != null && (e.OldValue as IList<object>).Count > 0 && (e.NewValue as IList<object>).Count > 0)
        //        {
        //            // selected year or month
        //            string originalYear = (e.OldValue as IList<object>)[0] as string;
        //            string originalMonth = (e.OldValue as IList<object>)[1] as string;
        //            string selectedYear = (e.NewValue as IList<object>)[0] as string;
        //            string selectedMonth = (e.NewValue as IList<object>)[1] as string;

        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        // throw ex;
        //    }
        //}
    }
}
