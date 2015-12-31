using FinancialPlanner.Data.Entity;

namespace FinancialPlanner.Infrastructure.Domain.ItemDetail.Ledger.Repository
{
    /// =====================================================================
    /// <summary>
    ///     Ledger Repository
    /// </summary>
    /// =====================================================================
    public class LedgerRepository : ILedgerRepository
    {
        private readonly ItemDetailEntities _db;

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Base Constructor
        /// </summary>
        /// ---------------------------------------------------------------------
        public LedgerRepository(
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
    }
}