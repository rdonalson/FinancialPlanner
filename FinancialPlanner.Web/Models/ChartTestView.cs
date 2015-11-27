using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialPlanner.Web.Models
{
    //[Serializable]
    public class ChartTestView
    {
        public string Labels { get; set; }
        public decimal? Values { get; set; }
    }
}