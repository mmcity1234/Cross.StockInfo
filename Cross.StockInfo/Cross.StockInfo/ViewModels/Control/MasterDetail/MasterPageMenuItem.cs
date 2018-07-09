using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cross.StockInfo.ViewModels.Control.MasterDetail
{

    public class MasterPageMenuItem
    {
        public MasterPageMenuItem()
        {
           
        }
        public string IconSource { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}