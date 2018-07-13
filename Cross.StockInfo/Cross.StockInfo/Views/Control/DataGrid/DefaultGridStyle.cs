using Cross.StockInfo.Assets.Styles;
using Syncfusion.SfDataGrid.XForms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Linq;
using Cross.StockInfo.Common.Helper;

namespace Cross.StockInfo.Views.Control.DataGrid
{
    public class DefaultGridStyle : DataGridStyle
    {
        public override Color GetAlternatingRowBackgroundColor()
        {
            return ResourceDictionaryHelper.GetResource<Color>("LightGrayColor_ColumnAlternating");
        }
        public override GridLinesVisibility GetGridLinesVisibility()
        {
            return GridLinesVisibility.None;
        }
           
    }
}
