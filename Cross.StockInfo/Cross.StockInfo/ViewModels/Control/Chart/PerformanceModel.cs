using System;
using System.Collections.Generic;
using System.Text;

namespace Cross.StockInfo.ViewModels.Control.Chart
{
    public class PerformanceModel : BaseViewModel
    {
        private string _weekPerformance;
        private string _monthPerformance;
        private string _quarterPerformance;
        private string _halfYearPerformance;
        private string _yearPerformance;
        private string _thisYearPerformance;
        /// <summary>
        /// 週績效
        /// </summary>
        public string WeekPerformance
        {
            get => _weekPerformance;
            set
            {
                _weekPerformance = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 月績效
        /// </summary>
        public string MonthPerformance
        {
            get => _monthPerformance;
            set
            {
                _monthPerformance = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// 季績效
        /// </summary>
        public string QuarterPerformance
        {
            get => _quarterPerformance;
            set
            {
                _quarterPerformance = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 半年績效
        /// </summary>
        public string HalfYearPerformance
        {
            get => _halfYearPerformance;
            set
            {
                _halfYearPerformance = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// 年績效
        /// </summary>
        public string YearPerformance
        {
            get => _yearPerformance;
            set
            {
                _yearPerformance = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// 今年以來的績效
        /// </summary>
        public string ThisYearPerformance
        {
            get => _thisYearPerformance;
            set
            {
                _thisYearPerformance = value;
                OnPropertyChanged();
            }
        }
    }
}
