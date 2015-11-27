using System;
using System.Linq;
using FinancialPlanner.Data.Entity;
using FinancialPlanner.Infrastructure.Domain.ItemDetail.ViewModels;

namespace FinancialPlanner.Infrastructure.Domain.ItemDetail.Credits.Repository
{
    /// =====================================================================
    /// <summary>
    ///     Public Interface for the Credit Repository
    /// </summary>
    /// =====================================================================
    public interface ICreditRepository : IDisposable
    {
        DetailItemView GetCreditView(int? id, string userName);
        IQueryable<Credit> GetCredits(string userName);
        bool Add(DetailItemView detailItemView);
        bool Save(DetailItemView detailItemView);
        bool Delete(int? id, string userName);
    }
}