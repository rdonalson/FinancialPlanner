using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using FinancialPlanner.Data.Entity;
using FinancialPlanner.Infrastructure.Domain.Display.Models;

namespace FinancialPlanner.Infrastructure.Domain.Display.Timeline.Repository
{
    /// =====================================================================
    /// <summary>
    ///     Timeline Repository Interface
    /// </summary>
    /// =====================================================================
    public interface ITimelineRepository : IDisposable
    {
        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Return ObjectResult of Timeline Items.  Scheduled Credits and Debits
        ///     in a Ledger format
        /// </summary>
        /// <param name="timeFrameBegin">DateTime</param>
        /// <param name="timeFrameEnd">DateTime</param>
        /// <param name="userName">userName</param>
        /// <returns>ObjectResult(spCreateLedgerReadout_Result)</returns>
        /// ---------------------------------------------------------------------
        //ObjectResult<spCreateLedgerReadout_Result> GetLedger(
        //    DateTime timeFrameBegin,
        //    DateTime timeFrameEnd,
        //    string userName);

        List<PrimaryDataView> GetLedger(
            DateTime timeFrameBegin,
            DateTime timeFrameEnd,
            string userName);
    }
}