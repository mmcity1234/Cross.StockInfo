using Cross.StockInfo.ViewModels.Chart;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Cross.StockInfo.ViewModels.Control
{
    public class DailyPriceControlModel
    {
        public ObservableCollection<DataPoint> DataPoints { get; set; }

        public string Title { get; set; }
    }
}
