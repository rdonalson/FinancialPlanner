using System;
using System.Linq;
using FinancialPlanner.Data.Entity;

namespace FinancialPlanner.Infrastructure.Domain.ItemDetail.Periods.Repository
{
    /// =====================================================================
    /// <summary>
    ///     Period Repository
    /// </summary>
    /// =====================================================================
    public class PeriodRepository : IPeriodRepository
    {
        private readonly ItemDetailEntities _db;

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Base Constructor
        /// </summary>
        /// ---------------------------------------------------------------------
        public PeriodRepository(
            int conndirection,
            string server,
            string database,
            string userid,
            string password)
        {
            _db = new ItemDetailEntities(ConnectionStringManager.ConnectionString(
                    conndirection,
                    2,
                    server,
                    database,
                    userid,
                    password));
        }

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Remove DBContext
        /// </summary>
        /// ---------------------------------------------------------------------
        public void Dispose()
        {
            _db.Dispose();
        }

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Gets a specific Period
        /// </summary>
        /// <param name="id">int?</param>
        /// <returns>Period</returns>
        /// ---------------------------------------------------------------------
        public Period GetPeriod(int? id)
        {
            try
            {
                return _db.Periods.SingleOrDefault(d => d.PkPeriod == id);
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Gets all the Periods
        /// </summary>
        /// <returns>IQueryable(Period)</returns>
        /// ---------------------------------------------------------------------
        public IQueryable<Period> GetPeriods()
        {
            try
            {
                return _db.Periods;
            }
            catch (Exception exception)
            {
                return null;
            }
        }
    }
}