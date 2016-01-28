using System;
using System.Data.Entity.Core.Objects;
using FinancialPlanner.Data.Entity;

namespace FinancialPlanner.Infrastructure.Domain.Display.Timeline.Repository
{
    public interface ITimelineRepository : IDisposable
    {
        ObjectResult<spCreateLedgerReadout_Result> GetLedger(
            DateTime timeFrameBegin,
            DateTime timeFrameEnd,
            string userName);
    }
}