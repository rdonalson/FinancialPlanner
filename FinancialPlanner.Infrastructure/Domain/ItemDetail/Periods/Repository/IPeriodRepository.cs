using System;
using System.Linq;
using FinancialPlanner.Data.Entity;

namespace FinancialPlanner.Infrastructure.Domain.ItemDetail.Periods.Repository
{
    /// =====================================================================
    /// <summary>
    ///     Period Repository Interface
    /// </summary>
    /// =====================================================================
    public interface IPeriodRepository : IDisposable
    {
        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Gets a specific Period
        /// </summary>
        /// <param name="id">int?</param>
        /// <returns>Period</returns>
        /// ---------------------------------------------------------------------
        Period GetPeriod(int? id);

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Gets all the Periods
        /// </summary>
        /// <returns>IQueryable(Period)</returns>
        /// ---------------------------------------------------------------------
        IQueryable<Period> GetPeriods();
    }
}