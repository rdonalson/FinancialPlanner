using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FinancialPlanner.Data.Entity;

namespace FinancialPlanner.Infrastructure.Domain.Display.Models
{
    /// =====================================================================
    /// <summary>
    ///     This View Model is used in the Timeline Criteria Section
    /// </summary>
    /// =====================================================================
    public class TimelineCriteriaView
    {
        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Fields used in the Timeline Criteria form
        /// </summary>
        /// ---------------------------------------------------------------------
        [Required]
        [Display(Name = "Time Frame Begin")]
        public DateTime timeFrameBegin { get; set; }

        [Required]
        [Display(Name = "Time Frame End")]
        public DateTime timeFrameEnd { get; set; }

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Item List to be passed to the "_TimelineChart" partial view.
        /// </summary>
        /// ---------------------------------------------------------------------
        public List<PrimaryDataView> Result { get; set; }
    }

    /// =====================================================================
    /// <summary>
    ///     This View Model is used in the Ledger Criteria Section
    /// </summary>
    /// =====================================================================
    public class LedgerCriteriaView
    {
        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Fields used in the Ledger Criteria form
        /// </summary>
        /// ---------------------------------------------------------------------
        [Required]
        [Display(Name = "Time Frame Begin")]
        public DateTime timeFrameBegin { get; set; }

        [Required]
        [Display(Name = "Time Frame End")]
        public DateTime timeFrameEnd { get; set; }

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Export switch
        /// </summary>
        /// ---------------------------------------------------------------------
        public bool DoExport { get; set; }

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Item List to be passed to the "_Ledger" partial view.
        /// </summary>
        /// ---------------------------------------------------------------------
        public List<spCreateLedgerReadout_Result> Result { get; set; }
    }
}
