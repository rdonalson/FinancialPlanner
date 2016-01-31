using System;
using System.Data.Entity.Core.Objects;
using System.Linq;
using FinancialPlanner.Data.Entity;

namespace FinancialPlanner.Infrastructure.Domain.Display.Timeline.Repository
{
    /// =====================================================================
    /// <summary>
    ///     Timeline Repository
    /// </summary>
    /// =====================================================================
    public class TimelineRepository : ITimelineRepository
    {
        private readonly ItemDetailEntities _db;

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Base Constructor
        /// </summary>
        /// ---------------------------------------------------------------------
        public TimelineRepository(
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
        ///     Return ObjectResult of Timeline Items.  Scheduled Credits and Debits
        ///     in a Ledger format
        /// </summary>
        /// <param name="timeFrameBegin">DateTime</param>
        /// <param name="timeFrameEnd">DateTime</param>
        /// <param name="userName">userName</param>
        /// <returns>ObjectResult(spCreateLedgerReadout_Result)</returns>
        /// ---------------------------------------------------------------------
        public ObjectResult<spCreateLedgerReadout_Result> GetLedger(
            DateTime timeFrameBegin,
            DateTime timeFrameEnd,
            string userName)
        {
            try
            {
                return _db.spCreateLedgerReadout(timeFrameBegin, timeFrameEnd, userName);
            }
            catch (Exception ex)
            {
                return null;
            }
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