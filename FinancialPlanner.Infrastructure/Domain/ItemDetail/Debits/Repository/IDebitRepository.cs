using System;
using System.Linq;
using FinancialPlanner.Data.Entity;
using FinancialPlanner.Infrastructure.Domain.ItemDetail.ViewModels;

namespace FinancialPlanner.Infrastructure.Domain.ItemDetail.Debits.Repository
{
    /// =====================================================================
    /// <summary>
    ///     Debit Repository Interface
    /// </summary>
    /// =====================================================================
    public interface IDebitRepository : IDisposable
    {
        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Return List of all Debits
        /// </summary>
        /// <param name="userName">userName</param>
        /// <returns>IQueryable(Debit)</returns>
        /// ---------------------------------------------------------------------
        IQueryable<Debit> GetDebits(string userName);

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Get Debit View
        /// </summary>
        /// <param name="id">int?</param>
        /// <param name="userName">string</param>
        /// <returns>DetailItemView</returns>
        /// ---------------------------------------------------------------------
        DetailItemView GetDebitView(int? id, string userName);

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Add New Debit
        /// </summary>
        /// <param name="detailItemView">DetailItemView</param>
        /// <returns>bool</returns>
        /// ---------------------------------------------------------------------
        bool Add(DetailItemView detailItemView);

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Save Debit View
        /// </summary>
        /// <param name="detailItemView">DetailItemView</param>
        /// <returns>bool</returns>
        /// ---------------------------------------------------------------------
        bool Save(DetailItemView detailItemView);

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Delete Debit
        /// </summary>
        /// <param name="id">int?</param>
        /// <param name="userName">string</param>
        /// <returns>bool</returns>
        /// ---------------------------------------------------------------------
        bool Delete(int? id, string userName);
    }
}