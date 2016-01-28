using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FinancialPlanner.Data.Entity;

namespace FinancialPlanner.Web.Areas.Display.Models
{
    public class LedgerCriteriaViewModel
    {
        [Required]
        [Display(Name = "Time Frame Begin")]
        public DateTime timeFrameBegin { get; set; }
        [Required]
        [Display(Name = "Time Frame End")]
        public DateTime timeFrameEnd { get; set; }

        public bool DoExport { get; set; }

        public List<spCreateLedgerReadout_Result> Result { get; set; }
    }
}