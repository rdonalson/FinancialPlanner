using System;
using System.Collections.Generic;
using System.Linq;
using FinancialPlanner.Data.Entity;
using FinancialPlanner.Infrastructure.Domain.Display.Models;

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
        public List<PrimaryDataView> GetLedger(
            DateTime timeFrameBegin,
            DateTime timeFrameEnd,
            string userName)
        {
            try
            {
                var list = _db.spCreateLedgerReadout(timeFrameBegin, timeFrameEnd, userName, true).ToList();
                var grouped = (
                    from c in list.GroupBy(item => new
                    {
                        item.RollupKey,
                        item.Year,
                        item.WDate,
                        item.CreditSummary,
                        item.DebitSummary,
                        item.Net,
                        item.RunningTotal
                    }, (key, g) => new
                    {
                        key.RollupKey,
                        key.Year,
                        key.WDate,
                        key.CreditSummary,
                        key.DebitSummary,
                        key.Net,
                        key.RunningTotal
                    })
                    select new PrimaryDataView
                    {
                        RollupKey = c.RollupKey,
                        Year = c.Year,
                        WDate = c.WDate,
                        CreditSummary = c.CreditSummary,
                        DebitSummary = c.DebitSummary,
                        Net = c.Net,
                        RunningTotal = c.RunningTotal,
                        DetailsList = new List<DetailDataView>()
                    }).ToList();

                foreach (var item in grouped)
                {
                    item.DetailsList.AddRange(from l in list
                        where l.RollupKey == item.RollupKey
                              && l.Year == item.Year
                        orderby l.OccurrenceDate
                        select new DetailDataView
                        {
                            RollupKey = l.RollupKey,
                            Year = l.Year,
                            OccurrenceDate = l.OccurrenceDate,
                            ItemType = l.ItemType,
                            PeriodName = l.PeriodName,
                            Name = l.Name,
                            Amount = l.Amount
                        }
                        );
                }
                return grouped;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /*
        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Return ObjectResult of Timeline Items.  Scheduled Credits and Debits
        ///     in a Ledger format
        /// </summary>
        /// <param name="TimeFrameBegin">DateTime</param>
        /// <param name="TimeFrameEnd">DateTime</param>
        /// <param name="userName">userName</param>
        /// <returns>ObjectResult(spCreateLedgerReadout_Result)</returns>
        /// ---------------------------------------------------------------------
        public ObjectResult<spCreateLedgerReadout_Result> GetLedger(
            DateTime TimeFrameBegin,
            DateTime TimeFrameEnd,
            string userName)
        {
            try
            {
                return _db.spCreateLedgerReadout(TimeFrameBegin, TimeFrameEnd, userName, true);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        */

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