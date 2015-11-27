using System;
using System.Linq;
using FinancialPlanner.Data.Entity;

namespace FinancialPlanner.Infrastructure.Domain.ItemDetail.Periods.Repository
{
    /// =====================================================================
    /// <summary>
    ///     Public Interface for the Period Repository
    /// </summary>
    /// =====================================================================
    public interface IPeriodRepository : IDisposable
    {
        Period GetPeriod(int? id);
        IQueryable<Period> GetPeriods();
    }
}