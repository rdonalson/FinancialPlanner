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
        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Return List of all Credits
        /// </summary>
        /// <param name="userName">userName</param>
        /// <returns>IQueryable(Credit)</returns>
        /// ---------------------------------------------------------------------
        IQueryable<Credit> GetCredits(string userName);

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Get Credit View
        /// </summary>
        /// <param name="id">int?</param>
        /// <param name="userName">string</param>
        /// <returns>DetailItemView</returns>
        /// ---------------------------------------------------------------------
        DetailItemView GetCreditView(int? id, string userName);

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Add New Credit
        /// </summary>
        /// <param name="detailItemView">DetailItemView</param>
        /// <returns>bool</returns>
        /// ---------------------------------------------------------------------
        bool Add(DetailItemView detailItemView);

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Save Credit View
        /// </summary>
        /// <param name="detailItemView">DetailItemView</param>
        /// <returns>bool</returns>
        /// ---------------------------------------------------------------------
        bool Save(DetailItemView detailItemView);

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Delete Credit
        /// </summary>
        /// <param name="id">int?</param>
        /// <param name="userName">string</param>
        /// <returns>bool</returns>
        /// ---------------------------------------------------------------------
        bool Delete(int? id, string userName);
    }
}