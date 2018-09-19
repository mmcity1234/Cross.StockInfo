using Cross.StockInfo.Assets.Strings;
using Cross.StockInfo.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cross.StockInfo.ViewModels.Control
{
    public class DatePickerViewModel : BaseViewModel
    {
        public object SelectedDate { get; set; }

        public DelegateCommand<EventArgs> OkButtonClickedCommand { get; set; }

        public DelegateCommand<EventArgs> CancelButtonClickedCommand { get; set; }

        public event Action<DatePickerViewModel> SelectedFinish;

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
        

        public DatePickerViewModel()
        {
            OkButtonClickedCommand = new DelegateCommand<EventArgs>(OkButton_EventHandler);           
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
            catch(Exception e)
            {

            }
        }    
    }
}
