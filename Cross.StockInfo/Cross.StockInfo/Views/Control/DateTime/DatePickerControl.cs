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
            this.SelectionChanged += CustomDatePicker_SelectionChanged;
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
            for (int i = currentYear - 5; i <= currentYear; i++)
            {
                Year.Add(i.ToString());
            }

            //populate Days
            Date.Add(Month);
            Date.Add(Year);
        }
        private void CustomDatePicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateDays(Date, e);
        }

        //Update days method is used to alter the Date collection as per selection change in Month column(if Feb is Selected day collection has value from 1 to 28)

        public void UpdateDays(ObservableCollection<object> Date, SelectionChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    bool update = false;
                    if (e.OldValue != null && e.NewValue != null && (e.OldValue as IList<string>).Count > 0 && (e.NewValue as IList<string>).Count > 0)
                    {
                        if ((e.OldValue as IList<string>)[0] != (e.NewValue as IList<string>)[0])
                        {
                            update = true;
                        }
                        if ((e.OldValue as IList<object>)[2] != (e.NewValue as IList<string>)[2])
                        {
                            update = true;
                        }
                    }
                    if (update)
                    {
                        ObservableCollection<object> days = new ObservableCollection<object>();

                        int month = System.DateTime.ParseExact(Months[(e.NewValue as IList<string>)[0]], "MMMM", CultureInfo.InvariantCulture).Month;

                        int year = int.Parse((e.NewValue as IList<string>)[2].ToString());

                        for (int j = 1; j <= System.DateTime.DaysInMonth(year, month); j++)
                        {
                            if (j < 10)
                            {
                                days.Add("0" + j);
                            }
                            else
                                days.Add(j.ToString());
                        }
                        if (days.Count > 0)
                        {
                            Date.RemoveAt(1);
                            Date.Insert(1, days);
                        }
                    }
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            });
        }

    }
}
