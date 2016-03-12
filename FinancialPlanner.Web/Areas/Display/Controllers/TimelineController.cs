using System;
using System.Linq;
using System.Web.Mvc;
using FinancialPlanner.Infrastructure.Domain.Display.Models;
using FinancialPlanner.Infrastructure.Domain.Display.Timeline.Repository;
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
    [Authorize]
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
        ///     Display Criterial
        ///     -------------------------------------
        ///     GET: Display/Timeline
        /// </summary>
        /// <returns>ActionResult</returns>
        /// ---------------------------------------------------------------------
        public ActionResult Index()
        {
            var vm = new TimelineCriteriaView();
            var cm = new TempDateCriteriaView();
            if (TempData["CriteriaModel"] == null)
            {
                var dateTime = DateTime.Today;
                vm.TimeFrameBegin = dateTime;
                vm.TimeFrameEnd = dateTime.AddMonths(1);
                cm.TimeFrameBegin = vm.TimeFrameBegin;
                cm.TimeFrameEnd = vm.TimeFrameEnd;
                TempData["CriteriaModel"] = cm;
            }
            else
            {
                cm = TempData["CriteriaModel"] as TempDateCriteriaView;
                if (cm != null) vm.TimeFrameBegin = cm.TimeFrameBegin;
                if (cm != null) vm.TimeFrameEnd = cm.TimeFrameEnd;
            }
            vm.Result =
                _timelineRepository.GetLedger(vm.TimeFrameBegin, vm.TimeFrameEnd, User.Identity.GetUserName()).ToList();
            return View(vm);
        }

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Receive "TimelineCriteriaViewModel" View Model data from
        ///     Timeline Criteria View
        ///     -------------------------------------
        ///     POST: Display/Timeline
        /// </summary>
        /// <param name="vm">TimelineCriteriaViewModel</param>
        /// <returns>ActionResult</returns>
        /// ---------------------------------------------------------------------
        [HttpPost]
        public ActionResult Index(TimelineCriteriaView vm)
        {
            vm.Result =
                _timelineRepository.GetLedger(vm.TimeFrameBegin, vm.TimeFrameEnd, User.Identity.GetUserName()).ToList();
            return View(vm);
        }

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     *** Archived ***
        ///     Return List of Timeline Items from the TimelineRepository repository.
        ///     Scheduled Credits and Debits in a Ledger format
        ///     -------------------------------------
        ///     GET: Display/Timeline/GetLedgerReadout
        /// </summary>
        /// <param name="timeFrameBegin">DateTime</param>
        /// <param name="timeFrameEnd">DateTime</param>
        /// <returns>ActionResult</returns>
        /// ---------------------------------------------------------------------
        [HttpGet]
        public ActionResult GetLedgerReadout(DateTime timeFrameBegin, DateTime timeFrameEnd)
        {
            var result = _timelineRepository.GetLedger(timeFrameBegin, timeFrameEnd, User.Identity.GetUserName());
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
