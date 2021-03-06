﻿using System;
using System.Data.Entity.Core.Objects;
using FinancialPlanner.Data.Entity;

namespace FinancialPlanner.Infrastructure.Domain.Display.Ledger.Repository
{
    /// =====================================================================
    /// <summary>
    ///     Ledger Repository Interface
    /// </summary>
    /// =====================================================================
    public interface ILedgerRepository : IDisposable
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
        ObjectResult<spCreateLedgerReadout_Result> GetLedger(
            DateTime timeFrameBegin,
            DateTime timeFrameEnd,
            string userName);
    }
}

