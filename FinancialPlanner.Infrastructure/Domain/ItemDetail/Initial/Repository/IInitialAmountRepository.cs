using System;
using System.Linq;
using FinancialPlanner.Data.Entity;
using FinancialPlanner.Infrastructure.Domain.ItemDetail.ViewModels;

namespace FinancialPlanner.Infrastructure.Domain.ItemDetail.Initial.Repository
{
    /// =====================================================================
    /// <summary>
    ///     Public Interface for the Initial Amount Repository
    /// </summary>
    /// =====================================================================
    public interface IInitialAmountRepository : IDisposable
    {
        IQueryable<InitialAmount> GetInitialAmounts(string userName);
        InitialAmountView GetInitialAmountView(int? id, string userName);
        bool Save(InitialAmountView initialAmountView);
        bool Add(InitialAmountView initialAmountView);
        bool Delete(int? id, string userName);
    }
}