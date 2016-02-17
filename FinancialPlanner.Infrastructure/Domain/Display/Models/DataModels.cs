using System;
using System.Collections.Generic;

namespace FinancialPlanner.Infrastructure.Domain.Display.Models
{
    /// =====================================================================
    /// <summary>
    ///     This Data Model is used in the Ledger Output to contain the
    ///     Summary records; daily, weekly, monthly, quarterly and yearly
    ///     rollups.  There are two keys that used to connect the two datasets;
    ///     RollupKey and Year
    ///     ** RollupKey will contain:
    ///         Daily       -   PkLMain (Primary key from the @LedgerMain
    ///                                 table in the Procedure)
    ///         Weekly      -   Week    (Week Number)
    ///         Monthly     -   Month   (Month Number)
    ///         Quarterly   -   Quarter (Quarter Number)
    ///         Yearly      -   Year    (Year Number)
    ///     For every record in the Primary there will be a list of
    ///     "DetailDataView" records connected by the same keys
    /// </summary>
    /// =====================================================================
    public class PrimaryDataView
    {
        public int RollupKey { get; set; }
        public int? Year { get; set; }
        public DateTime? WDate { get; set; }
        public double? CreditSummary { get; set; }
        public double? DebitSummary { get; set; }
        public double? Net { get; set; }
        public double? RunningTotal { get; set; }

        /* Detail Items for each Summary Primary record */
        public List<DetailDataView> DetailsList { get; set; }
    }

    /// =====================================================================
    /// <summary>
    ///     This Data Model is used in the Ledger Output to contain the
    ///     Detail records and has the same keys as the Primary.  In this case
    ///     they act as foreign keys
    /// </summary>
    /// =====================================================================
    public class DetailDataView
    {
        public int RollupKey { get; set; }
        public int? Year { get; set; }
        public DateTime? OccurrenceDate { get; set; }
        public int ItemType { get; set; }
        public string PeriodName { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
    }
}