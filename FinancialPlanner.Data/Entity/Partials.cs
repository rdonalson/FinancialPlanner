using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialPlanner.Data.Entity
{
    [MetadataType(typeof(spCreateLedgerReadout_ResultMetadata))]
    public partial class spCreateLedgerReadout_Result
    {
    }

    public partial class spCreateLedgerReadout_ResultMetadata
    {
        [Display(Name = "PkLMain")]
        public int PkLMain { get; set; }

        [Display(Name = "Date")]
        public System.DateTime? WDate { get; set; }

        [Display(Name = "Credit Summary")]
        public double? CreditSummary { get; set; }

        [Display(Name = "Debit Summary")]
        public double? DebitSummary { get; set; }

        [Display(Name = "Net Daily")]
        public double? NetDaily { get; set; }

        [Display(Name = "Running Total")]
        public double? RunningTotal { get; set; }

        [Display(Name = "Item Type")]
        public int? ItemType { get; set; }

        [Display(Name = "FkItemDetail")]
        public int? FkItemDetail { get; set; }

        [Display(Name = "Period Type")]
        public string PeriodName { get; set; }

        [Display(Name = "Item Name")]
        public string Name { get; set; }

        [Display(Name = "Item Amount")]
        public decimal? Amount { get; set; }

    }
}
