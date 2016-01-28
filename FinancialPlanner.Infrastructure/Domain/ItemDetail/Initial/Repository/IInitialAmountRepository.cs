using System;
using System.Linq;
using FinancialPlanner.Data.Entity;
using FinancialPlanner.Infrastructure.Domain.ItemDetail.ViewModels;

namespace FinancialPlanner.Infrastructure.Domain.ItemDetail.Initial.Repository
{
    /// =====================================================================
    /// <summary>
    ///     Initial Amount Repository Interface
    /// </summary>
    /// =====================================================================
    public interface IInitialAmountRepository : IDisposable
    {
        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Return the List of Initial Amounts which will always be just one
        /// </summary>
        /// <param name="userName">userName</param>
        /// <returns>IQueryable(InitialAmount)</returns>
        /// ---------------------------------------------------------------------
        IQueryable<InitialAmount> GetInitialAmounts(string userName);

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Get Initial Amount View
        /// </summary>
        /// <param name="id">int?</param>
        /// <param name="userName">string</param>
        /// <returns>DetailItemView</returns>
        /// ---------------------------------------------------------------------
        InitialAmountView GetInitialAmountView(int? id, string userName);

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Save Credit View
        /// </summary>
        /// <param name="initialAmountView">InitialAmountView</param>
        /// <returns>bool</returns>
        /// ---------------------------------------------------------------------
        bool Save(InitialAmountView initialAmountView);

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Add New Credit
        /// </summary>
        /// <param name="initialAmountView">InitialAmountView</param>
        /// <returns>bool</returns>
        /// ---------------------------------------------------------------------
        bool Add(InitialAmountView initialAmountView);

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Delete Initial Amount
        /// </summary>
        /// <param name="id">int?</param>
        /// <param name="userName">string</param>
        /// <returns>bool</returns>
        /// ---------------------------------------------------------------------
        bool Delete(int? id, string userName);
    }
}