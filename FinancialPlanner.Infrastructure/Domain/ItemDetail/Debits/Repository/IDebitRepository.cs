using System;
using System.Linq;
using FinancialPlanner.Data.Entity;
using FinancialPlanner.Infrastructure.Domain.ItemDetail.ViewModels;

namespace FinancialPlanner.Infrastructure.Domain.ItemDetail.Debits.Repository
{
    /// =====================================================================
    /// <summary>
    ///     Public Interface for the Debit Repository
    /// </summary>
    /// =====================================================================
    public interface IDebitRepository : IDisposable
    {
        DetailItemView GetDebitView(int? id, string userName);
        IQueryable<Debit> GetDebits(string userName);
        bool Add(DetailItemView detailItemView);
        bool Save(DetailItemView detailItemView);
        bool Delete(int? id, string userName);
    }
}