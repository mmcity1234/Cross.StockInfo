using Syncfusion.SfDataGrid.XForms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Cross.StockInfo.Views.Control.DataGrid
{
    public class FlatGridStyle : DataGridStyle
    {
        public override GridLinesVisibility GetGridLinesVisibility()
        {
            return GridLinesVisibility.None;
        }
        public override float GetHeaderBorderWidth()
        {
            return 0;
        }
        public override Color GetHeaderBackgroundColor()
        {
            return Color.Transparent;
        }

    }
}
