using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinancialPlanner.Infrastructure.Domain.Display.Timeline.Repository;
using FinancialPlanner.Infrastructure.Domain.ItemDetail.Credits.Repository;
using FinancialPlanner.Web.Helpers;
using FinancialPlanner.Web.Properties;
using Microsoft.AspNet.Identity;

namespace FinancialPlanner.Web.Areas.Display.Controllers
{
    /// =====================================================================
    /// <summary>
    ///     Timeline Controller
    /// </summary>
    /// =====================================================================
    public class TimelineController : Controller
    {
        private readonly ITimelineRepository _timelineRepository;

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Base Constructor
        /// </summary>
        /// ---------------------------------------------------------------------
        public TimelineController()
        {
            var conndirection = Convert.ToInt32(Resources.CONN_DIRECTION);
            string server, database, userid, password;
            ConnectionHelpers.ConnStringParameters(conndirection, out server, out database, out userid, out password);
            _timelineRepository = new TimelineRepository(conndirection, server, database, userid, password);
        }

        /// ---------------------------------------------------------------------
        /// <summary>
        /// Display Criterial
        /// -------------------------------------
        /// GET: Display/Timeline
        /// </summary>
        /// <returns>ActionResult</returns>
        /// ---------------------------------------------------------------------
        public ActionResult Index()
        {
            return View();
        }

        /// ---------------------------------------------------------------------
        /// <summary>
        /// Return List of Timeline Items from the TimelineRepository repository.
        /// Scheduled Credits and Debits in a Ledger format
        /// -------------------------------------
        /// GET: Display/Timeline/GetLedgerReadout
        /// </summary>
        /// <param name="timeFrameBegin">DateTime</param>
        /// <param name="timeFrameEnd">DateTime</param>
        /// <returns>ActionResult</returns>
        /// ---------------------------------------------------------------------
        [HttpGet]
        public ActionResult GetLedgerReadout(DateTime timeFrameBegin, DateTime timeFrameEnd)
        {
            var result = _timelineRepository.GetLedger(timeFrameBegin, timeFrameEnd, User.Identity.GetUserName()).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}