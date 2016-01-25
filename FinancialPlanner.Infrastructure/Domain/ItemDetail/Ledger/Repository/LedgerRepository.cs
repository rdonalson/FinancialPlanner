using System;
using System.Collections.Generic;
using System.Linq;
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
        /// Get the Ledger Readout
        /// </summary>
        /// <param name="timeFrameBegin"></param>
        /// <param name="timeFrameEnd">DateTime</param>
        /// <param name="userName">string</param>
        /// <returns>List(spCreateLedgerReadout_Result)</returns>
        /// ---------------------------------------------------------------------
        public List<spCreateLedgerReadout_Result> GetLedgerReadout(DateTime timeFrameBegin, DateTime timeFrameEnd, string userName)
        {
            List<spCreateLedgerReadout_Result> result =
                _db.spCreateLedgerReadout(timeFrameBegin, timeFrameEnd, userName).ToList();
            return result;
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