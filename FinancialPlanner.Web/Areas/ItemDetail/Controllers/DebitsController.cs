using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FinancialPlanner.Data.Entity;
using FinancialPlanner.Infrastructure.Domain.ItemDetail.Debits.Repository;
using FinancialPlanner.Infrastructure.Domain.ItemDetail.Enums;
using FinancialPlanner.Infrastructure.Domain.ItemDetail.ViewModels;
using FinancialPlanner.Web.Helpers;
using FinancialPlanner.Web.Properties;

namespace FinancialPlanner.Web.Areas.ItemDetail.Controllers
{
    /// =====================================================================
    /// <summary>
    ///     Debit Controller
    /// </summary>
    /// =====================================================================
    [Authorize]
    public class DebitsController : Controller
    {
        private readonly IDebitRepository _debitRepository;
        private readonly WebHelpers _webHelpers = new WebHelpers();

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Base Constructor
        /// </summary>
        /// ---------------------------------------------------------------------
        public DebitsController()
        {
            var conndirection = Convert.ToInt32(Resources.CONN_DIRECTION);
            string server, database, userid, password;
            ConnectionHelpers.ConnStringParameters(conndirection, out server, out database, out userid, out password);
            _debitRepository = new DebitRepository(conndirection, server, database, userid, password);
        }

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     GET: ItemDetail/Debit
        /// </summary>
        /// <returns>ActionResult</returns>
        /// ---------------------------------------------------------------------
        public ActionResult Index()
        {
            IQueryable<Debit> debits = _debitRepository.GetDebits(User.Identity.Name);
            return View(debits.ToList());
        }

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     GET: ItemDetail/Debits/Create
        /// </summary>
        /// <returns>ActionResult</returns>
        /// ---------------------------------------------------------------------
        public ActionResult Create()
        {
            SelectList dowList = _webHelpers.WeekdaySelectList();
            ViewBag.WeekDaysDOW = dowList;      // Weekly Radio List
            ViewBag.WeekDaysEODOW = dowList;    // Every Other Week Radio List
            ViewBag.FkPeriod = _webHelpers.PeriodSelectList();

            ViewData.Add("Action", "Create");   // Insert Marker
            return View();
        }

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     POST: ItemDetail/Debits/Create
        ///     To protect from overposting attacks, please enable the specific properties you want to bind to, for
        ///     more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// </summary>
        /// <param name="detailItemView">DetailItemView</param>
        /// <returns>ActionResult</returns>
        /// ---------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(
                Include =
                    "PkID,UserName,Name,StrAmount,Amount,FkPeriod,BeginDate,EndDate,WeeklyDOW,EverOtherWeekDOW,BiMonthlyDay1,BiMonthlyDay2,MonthlyDOM,Quarterly1Month,Quarterly1Day,Quarterly2Month,Quarterly2Day,Quarterly3Month,Quarterly3Day,Quarterly4Month,Quarterly4Day,SemiAnnual1Month,SemiAnnual1Day,SemiAnnual2Month,SemiAnnual2Day,AnnualMOY,AnnualDOM,DateRangeReq"
                )] DetailItemView detailItemView)
        {
            int? period = detailItemView.FkPeriod;
            bool dateRange = detailItemView.DateRangeReq;

            switch (period)
            {
                case 1: // One Time Occurrence
                    ModelState["EndDate"].Errors.Clear();
                    // Weekly
                    ModelState["WeeklyDOW"].Errors.Clear();
                    // Every Other Week
                    ModelState["EverOtherWeekDOW"].Errors.Clear();
                    // Bi-Monthly
                    ModelState["BiMonthlyDay1"].Errors.Clear();
                    ModelState["BiMonthlyDay2"].Errors.Clear();
                    // Monthly
                    ModelState["MonthlyDOM"].Errors.Clear();
                    // Quarterly
                    ModelState["Quarterly1Month"].Errors.Clear();
                    ModelState["Quarterly1Day"].Errors.Clear();
                    ModelState["Quarterly2Month"].Errors.Clear();
                    ModelState["Quarterly2Day"].Errors.Clear();
                    ModelState["Quarterly3Month"].Errors.Clear();
                    ModelState["Quarterly3Day"].Errors.Clear();
                    ModelState["Quarterly4Month"].Errors.Clear();
                    ModelState["Quarterly4Day"].Errors.Clear();
                    // Semi-Annual
                    ModelState["SemiAnnual1Month"].Errors.Clear();
                    ModelState["SemiAnnual1Day"].Errors.Clear();
                    ModelState["SemiAnnual2Month"].Errors.Clear();
                    ModelState["SemiAnnual2Day"].Errors.Clear();
                    // Annual
                    ModelState["AnnualMOY"].Errors.Clear();
                    ModelState["AnnualDOM"].Errors.Clear();
                    break;
                case 2: // Daily
                    if (!dateRange)
                    {
                        ModelState["BeginDate"].Errors.Clear();
                        ModelState["EndDate"].Errors.Clear();
                    }
                    // Weekly
                    ModelState["WeeklyDOW"].Errors.Clear();
                    // Every Other Week
                    ModelState["EverOtherWeekDOW"].Errors.Clear();
                    // Bi-Monthly
                    ModelState["BiMonthlyDay1"].Errors.Clear();
                    ModelState["BiMonthlyDay2"].Errors.Clear();
                    // Monthly
                    ModelState["MonthlyDOM"].Errors.Clear();
                    // Quarterly
                    ModelState["Quarterly1Month"].Errors.Clear();
                    ModelState["Quarterly1Day"].Errors.Clear();
                    ModelState["Quarterly2Month"].Errors.Clear();
                    ModelState["Quarterly2Day"].Errors.Clear();
                    ModelState["Quarterly3Month"].Errors.Clear();
                    ModelState["Quarterly3Day"].Errors.Clear();
                    ModelState["Quarterly4Month"].Errors.Clear();
                    ModelState["Quarterly4Day"].Errors.Clear();
                    // Semi-Annual
                    ModelState["SemiAnnual1Month"].Errors.Clear();
                    ModelState["SemiAnnual1Day"].Errors.Clear();
                    ModelState["SemiAnnual2Month"].Errors.Clear();
                    ModelState["SemiAnnual2Day"].Errors.Clear();
                    // Annual
                    ModelState["AnnualMOY"].Errors.Clear();
                    ModelState["AnnualDOM"].Errors.Clear();
                    break;
                case 3: // Weekly
                    if (!dateRange)
                    {
                        ModelState["BeginDate"].Errors.Clear();
                        ModelState["EndDate"].Errors.Clear();
                    }
                    // Weekly
                    //modelState["WeeklyDOW"].Errors.Clear();
                    // Every Other Week
                    ModelState["EverOtherWeekDOW"].Errors.Clear();
                    // Bi-Monthly
                    ModelState["BiMonthlyDay1"].Errors.Clear();
                    ModelState["BiMonthlyDay2"].Errors.Clear();
                    // Monthly
                    ModelState["MonthlyDOM"].Errors.Clear();
                    // Quarterly
                    ModelState["Quarterly1Month"].Errors.Clear();
                    ModelState["Quarterly1Day"].Errors.Clear();
                    ModelState["Quarterly2Month"].Errors.Clear();
                    ModelState["Quarterly2Day"].Errors.Clear();
                    ModelState["Quarterly3Month"].Errors.Clear();
                    ModelState["Quarterly3Day"].Errors.Clear();
                    ModelState["Quarterly4Month"].Errors.Clear();
                    ModelState["Quarterly4Day"].Errors.Clear();
                    // Semi-Annual
                    ModelState["SemiAnnual1Month"].Errors.Clear();
                    ModelState["SemiAnnual1Day"].Errors.Clear();
                    ModelState["SemiAnnual2Month"].Errors.Clear();
                    ModelState["SemiAnnual2Day"].Errors.Clear();
                    // Annual
                    ModelState["AnnualMOY"].Errors.Clear();
                    ModelState["AnnualDOM"].Errors.Clear();
                    break;
                case 4: // Every Two Weeks
                    if (!dateRange)
                    {
                        ModelState["BeginDate"].Errors.Clear();
                        ModelState["EndDate"].Errors.Clear();
                    }
                    // Weekly
                    ModelState["WeeklyDOW"].Errors.Clear();
                    // Every Other Week
                    //modelState["EverOtherWeekDOW"].Errors.Clear();
                    // Bi-Monthly
                    ModelState["BiMonthlyDay1"].Errors.Clear();
                    ModelState["BiMonthlyDay2"].Errors.Clear();
                    // Monthly
                    ModelState["MonthlyDOM"].Errors.Clear();
                    // Quarterly
                    ModelState["Quarterly1Month"].Errors.Clear();
                    ModelState["Quarterly1Day"].Errors.Clear();
                    ModelState["Quarterly2Month"].Errors.Clear();
                    ModelState["Quarterly2Day"].Errors.Clear();
                    ModelState["Quarterly3Month"].Errors.Clear();
                    ModelState["Quarterly3Day"].Errors.Clear();
                    ModelState["Quarterly4Month"].Errors.Clear();
                    ModelState["Quarterly4Day"].Errors.Clear();
                    // Semi-Annual
                    ModelState["SemiAnnual1Month"].Errors.Clear();
                    ModelState["SemiAnnual1Day"].Errors.Clear();
                    ModelState["SemiAnnual2Month"].Errors.Clear();
                    ModelState["SemiAnnual2Day"].Errors.Clear();
                    // Annual
                    ModelState["AnnualMOY"].Errors.Clear();
                    ModelState["AnnualDOM"].Errors.Clear();
                    break;
                case 5: // Bi-Monthly
                    if (!dateRange)
                    {
                        ModelState["BeginDate"].Errors.Clear();
                        ModelState["EndDate"].Errors.Clear();
                    }
                    // Weekly
                    ModelState["WeeklyDOW"].Errors.Clear();
                    // Every Other Week
                    ModelState["EverOtherWeekDOW"].Errors.Clear();
                    // Bi-Monthly
                    //ModelState["BiMonthlyDay1"].Errors.Clear();
                    //ModelState["BiMonthlyDay2"].Errors.Clear();
                    // Monthly
                    ModelState["MonthlyDOM"].Errors.Clear();
                    // Quarterly
                    ModelState["Quarterly1Month"].Errors.Clear();
                    ModelState["Quarterly1Day"].Errors.Clear();
                    ModelState["Quarterly2Month"].Errors.Clear();
                    ModelState["Quarterly2Day"].Errors.Clear();
                    ModelState["Quarterly3Month"].Errors.Clear();
                    ModelState["Quarterly3Day"].Errors.Clear();
                    ModelState["Quarterly4Month"].Errors.Clear();
                    ModelState["Quarterly4Day"].Errors.Clear();
                    // Semi-Annual
                    ModelState["SemiAnnual1Month"].Errors.Clear();
                    ModelState["SemiAnnual1Day"].Errors.Clear();
                    ModelState["SemiAnnual2Month"].Errors.Clear();
                    ModelState["SemiAnnual2Day"].Errors.Clear();
                    // Annual
                    ModelState["AnnualMOY"].Errors.Clear();
                    ModelState["AnnualDOM"].Errors.Clear();
                    break;
                case 6: // Monthly
                    if (!dateRange)
                    {
                        ModelState["BeginDate"].Errors.Clear();
                        ModelState["EndDate"].Errors.Clear();
                    }
                    // Weekly
                    ModelState["WeeklyDOW"].Errors.Clear();
                    // Every Other Week
                    ModelState["EverOtherWeekDOW"].Errors.Clear();
                    // Bi-Monthly
                    ModelState["BiMonthlyDay1"].Errors.Clear();
                    ModelState["BiMonthlyDay2"].Errors.Clear();
                    // Monthly
                    //ModelState["MonthlyDOM"].Errors.Clear();
                    // Quarterly
                    ModelState["Quarterly1Month"].Errors.Clear();
                    ModelState["Quarterly1Day"].Errors.Clear();
                    ModelState["Quarterly2Month"].Errors.Clear();
                    ModelState["Quarterly2Day"].Errors.Clear();
                    ModelState["Quarterly3Month"].Errors.Clear();
                    ModelState["Quarterly3Day"].Errors.Clear();
                    ModelState["Quarterly4Month"].Errors.Clear();
                    ModelState["Quarterly4Day"].Errors.Clear();
                    // Semi-Annual
                    ModelState["SemiAnnual1Month"].Errors.Clear();
                    ModelState["SemiAnnual1Day"].Errors.Clear();
                    ModelState["SemiAnnual2Month"].Errors.Clear();
                    ModelState["SemiAnnual2Day"].Errors.Clear();
                    // Annual
                    ModelState["AnnualMOY"].Errors.Clear();
                    ModelState["AnnualDOM"].Errors.Clear();
                    break;
                case 7: // Quarterly
                    if (!dateRange)
                    {
                        ModelState["BeginDate"].Errors.Clear();
                        ModelState["EndDate"].Errors.Clear();
                    }
                    // Weekly
                    ModelState["WeeklyDOW"].Errors.Clear();
                    // Every Other Week
                    ModelState["EverOtherWeekDOW"].Errors.Clear();
                    // Bi-Monthly
                    ModelState["BiMonthlyDay1"].Errors.Clear();
                    ModelState["BiMonthlyDay2"].Errors.Clear();
                    // Monthly
                    ModelState["MonthlyDOM"].Errors.Clear();
                    // Quarterly
                    //ModelState["Quarterly1Month"].Errors.Clear();
                    //ModelState["Quarterly1Day"].Errors.Clear();
                    //ModelState["Quarterly2Month"].Errors.Clear();
                    //ModelState["Quarterly2Day"].Errors.Clear();
                    //ModelState["Quarterly3Month"].Errors.Clear();
                    //ModelState["Quarterly3Day"].Errors.Clear();
                    //ModelState["Quarterly4Month"].Errors.Clear();
                    //ModelState["Quarterly4Day"].Errors.Clear();
                    // Semi-Annual
                    ModelState["SemiAnnual1Month"].Errors.Clear();
                    ModelState["SemiAnnual1Day"].Errors.Clear();
                    ModelState["SemiAnnual2Month"].Errors.Clear();
                    ModelState["SemiAnnual2Day"].Errors.Clear();
                    // Annual
                    ModelState["AnnualMOY"].Errors.Clear();
                    ModelState["AnnualDOM"].Errors.Clear();
                    break;
                case 8: // Semi-Annually
                    if (!dateRange)
                    {
                        ModelState["BeginDate"].Errors.Clear();
                        ModelState["EndDate"].Errors.Clear();
                    }
                    // Weekly
                    ModelState["WeeklyDOW"].Errors.Clear();
                    // Every Other Week
                    ModelState["EverOtherWeekDOW"].Errors.Clear();
                    // Bi-Monthly
                    ModelState["BiMonthlyDay1"].Errors.Clear();
                    ModelState["BiMonthlyDay2"].Errors.Clear();
                    // Monthly
                    ModelState["MonthlyDOM"].Errors.Clear();
                    // Quarterly
                    ModelState["Quarterly1Month"].Errors.Clear();
                    ModelState["Quarterly1Day"].Errors.Clear();
                    ModelState["Quarterly2Month"].Errors.Clear();
                    ModelState["Quarterly2Day"].Errors.Clear();
                    ModelState["Quarterly3Month"].Errors.Clear();
                    ModelState["Quarterly3Day"].Errors.Clear();
                    ModelState["Quarterly4Month"].Errors.Clear();
                    ModelState["Quarterly4Day"].Errors.Clear();
                    // Semi-Annual
                    //ModelState["SemiAnnual1Month"].Errors.Clear();
                    //ModelState["SemiAnnual1Day"].Errors.Clear();
                    //ModelState["SemiAnnual2Month"].Errors.Clear();
                    //ModelState["SemiAnnual2Day"].Errors.Clear();
                    // Annual
                    ModelState["AnnualMOY"].Errors.Clear();
                    ModelState["AnnualDOM"].Errors.Clear();
                    break;
                case 9: // Semi-Annually
                    if (!dateRange)
                    {
                        ModelState["BeginDate"].Errors.Clear();
                        ModelState["EndDate"].Errors.Clear();
                    }
                    // Weekly
                    ModelState["WeeklyDOW"].Errors.Clear();
                    // Every Other Week
                    ModelState["EverOtherWeekDOW"].Errors.Clear();
                    // Bi-Monthly
                    ModelState["BiMonthlyDay1"].Errors.Clear();
                    ModelState["BiMonthlyDay2"].Errors.Clear();
                    // Monthly
                    ModelState["MonthlyDOM"].Errors.Clear();
                    // Quarterly
                    ModelState["Quarterly1Month"].Errors.Clear();
                    ModelState["Quarterly1Day"].Errors.Clear();
                    ModelState["Quarterly2Month"].Errors.Clear();
                    ModelState["Quarterly2Day"].Errors.Clear();
                    ModelState["Quarterly3Month"].Errors.Clear();
                    ModelState["Quarterly3Day"].Errors.Clear();
                    ModelState["Quarterly4Month"].Errors.Clear();
                    ModelState["Quarterly4Day"].Errors.Clear();
                    // Semi-Annual
                    ModelState["SemiAnnual1Month"].Errors.Clear();
                    ModelState["SemiAnnual1Day"].Errors.Clear();
                    ModelState["SemiAnnual2Month"].Errors.Clear();
                    ModelState["SemiAnnual2Day"].Errors.Clear();
                    // Annual
                    //ModelState["AnnualMOY"].Errors.Clear();
                    //ModelState["AnnualDOM"].Errors.Clear();
                    break;
                default:
                    throw new Exception();
            }

            if (ModelState.IsValid)
            {
                detailItemView.UserName = User.Identity.Name;
                if (_debitRepository.Add(detailItemView))
                {
                    DisplaySuccessMessage("Has appended a Debit record");
                    return RedirectToAction("Index");
                }
            }
            ViewBag.FkPeriod = _webHelpers.PeriodSelectList(detailItemView.FkPeriod);
            DisplayErrorMessage();
            return View(detailItemView);
        }

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     GET: ItemDetail/Debits/Edit/5
        /// </summary>
        /// <param name="id">int?</param>
        /// <returns>ActionResult</returns>
        /// ---------------------------------------------------------------------
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Weekday? weekday = null;

            // Get Base Record
            DetailItemView detailItemView = _debitRepository.GetDebitView(id, User.Identity.Name);
            ViewData.Add("Action", "Edit");  // Edit Marker

            // Create Period Dropdown with selected value
            ViewBag.FkPeriod = _webHelpers.PeriodSelectList(detailItemView.FkPeriod);

            // Weekly Radio List with Selected value
            if (detailItemView.WeeklyDOW != null)
            {
                weekday = detailItemView.WeeklyDOW.Value;
            }
            ViewBag.WeekDaysDOW = _webHelpers.WeekdaySelectList(weekday);

            // Every Other Week Radio List with Selected value
            if (detailItemView.EverOtherWeekDOW != null)
            {
                weekday = detailItemView.EverOtherWeekDOW.Value;
            }
            ViewBag.WeekDaysEODOW = _webHelpers.WeekdaySelectList(weekday);

            return View(detailItemView);
        }

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     POST: ItemDetail/Debits/Edit/5
        ///     To protect from overposting attacks, please enable the specific properties you want to bind to, for more details
        ///     see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// </summary>
        /// <param name="detailItemView">DetailItemView</param>
        /// <returns>ActionResult</returns>
        /// ---------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(
                Include =
                    "PkID,UserName,Name,StrAmount,FkPeriod,BeginDate,EndDate,WeeklyDOW,EverOtherWeekDOW,BiMonthlyDay1,BiMonthlyDay2,MonthlyDOM,Quarterly1Month,Quarterly1Day,Quarterly2Month,Quarterly2Day,Quarterly3Month,Quarterly3Day,Quarterly4Month,Quarterly4Day,SemiAnnual1Month,SemiAnnual1Day,SemiAnnual2Month,SemiAnnual2Day,AnnualMOY,AnnualDOM,DateRangeReq,Period"
                )] DetailItemView detailItemView)
        {
            int? period = detailItemView.FkPeriod;
            bool dateRange = detailItemView.DateRangeReq;

            switch (period)
            {
                case 1: // One Time Occurrence
                    ModelState["EndDate"].Errors.Clear();
                    // Weekly
                    ModelState["WeeklyDOW"].Errors.Clear();
                    // Every Other Week
                    ModelState["EverOtherWeekDOW"].Errors.Clear();
                    // Bi-Monthly
                    ModelState["BiMonthlyDay1"].Errors.Clear();
                    ModelState["BiMonthlyDay2"].Errors.Clear();
                    // Monthly
                    ModelState["MonthlyDOM"].Errors.Clear();
                    // Quarterly
                    ModelState["Quarterly1Month"].Errors.Clear();
                    ModelState["Quarterly1Day"].Errors.Clear();
                    ModelState["Quarterly2Month"].Errors.Clear();
                    ModelState["Quarterly2Day"].Errors.Clear();
                    ModelState["Quarterly3Month"].Errors.Clear();
                    ModelState["Quarterly3Day"].Errors.Clear();
                    ModelState["Quarterly4Month"].Errors.Clear();
                    ModelState["Quarterly4Day"].Errors.Clear();
                    // Semi-Annual
                    ModelState["SemiAnnual1Month"].Errors.Clear();
                    ModelState["SemiAnnual1Day"].Errors.Clear();
                    ModelState["SemiAnnual2Month"].Errors.Clear();
                    ModelState["SemiAnnual2Day"].Errors.Clear();
                    // Annual
                    ModelState["AnnualMOY"].Errors.Clear();
                    ModelState["AnnualDOM"].Errors.Clear();
                    break;
                case 2: // Daily
                    if (!dateRange)
                    {
                        ModelState["BeginDate"].Errors.Clear();
                        ModelState["EndDate"].Errors.Clear();
                    }
                    // Weekly
                    ModelState["WeeklyDOW"].Errors.Clear();
                    // Every Other Week
                    ModelState["EverOtherWeekDOW"].Errors.Clear();
                    // Bi-Monthly
                    ModelState["BiMonthlyDay1"].Errors.Clear();
                    ModelState["BiMonthlyDay2"].Errors.Clear();
                    // Monthly
                    ModelState["MonthlyDOM"].Errors.Clear();
                    // Quarterly
                    ModelState["Quarterly1Month"].Errors.Clear();
                    ModelState["Quarterly1Day"].Errors.Clear();
                    ModelState["Quarterly2Month"].Errors.Clear();
                    ModelState["Quarterly2Day"].Errors.Clear();
                    ModelState["Quarterly3Month"].Errors.Clear();
                    ModelState["Quarterly3Day"].Errors.Clear();
                    ModelState["Quarterly4Month"].Errors.Clear();
                    ModelState["Quarterly4Day"].Errors.Clear();
                    // Semi-Annual
                    ModelState["SemiAnnual1Month"].Errors.Clear();
                    ModelState["SemiAnnual1Day"].Errors.Clear();
                    ModelState["SemiAnnual2Month"].Errors.Clear();
                    ModelState["SemiAnnual2Day"].Errors.Clear();
                    // Annual
                    ModelState["AnnualMOY"].Errors.Clear();
                    ModelState["AnnualDOM"].Errors.Clear();
                    break;
                case 3: // Weekly
                    if (!dateRange)
                    {
                        ModelState["BeginDate"].Errors.Clear();
                        ModelState["EndDate"].Errors.Clear();
                    }
                    // Weekly
                    //modelState["WeeklyDOW"].Errors.Clear();
                    // Every Other Week
                    ModelState["EverOtherWeekDOW"].Errors.Clear();
                    // Bi-Monthly
                    ModelState["BiMonthlyDay1"].Errors.Clear();
                    ModelState["BiMonthlyDay2"].Errors.Clear();
                    // Monthly
                    ModelState["MonthlyDOM"].Errors.Clear();
                    // Quarterly
                    ModelState["Quarterly1Month"].Errors.Clear();
                    ModelState["Quarterly1Day"].Errors.Clear();
                    ModelState["Quarterly2Month"].Errors.Clear();
                    ModelState["Quarterly2Day"].Errors.Clear();
                    ModelState["Quarterly3Month"].Errors.Clear();
                    ModelState["Quarterly3Day"].Errors.Clear();
                    ModelState["Quarterly4Month"].Errors.Clear();
                    ModelState["Quarterly4Day"].Errors.Clear();
                    // Semi-Annual
                    ModelState["SemiAnnual1Month"].Errors.Clear();
                    ModelState["SemiAnnual1Day"].Errors.Clear();
                    ModelState["SemiAnnual2Month"].Errors.Clear();
                    ModelState["SemiAnnual2Day"].Errors.Clear();
                    // Annual
                    ModelState["AnnualMOY"].Errors.Clear();
                    ModelState["AnnualDOM"].Errors.Clear();
                    break;
                case 4: // Every Two Weeks
                    if (!dateRange)
                    {
                        ModelState["BeginDate"].Errors.Clear();
                        ModelState["EndDate"].Errors.Clear();
                    }
                    // Weekly
                    ModelState["WeeklyDOW"].Errors.Clear();
                    // Every Other Week
                    //modelState["EverOtherWeekDOW"].Errors.Clear();
                    // Bi-Monthly
                    ModelState["BiMonthlyDay1"].Errors.Clear();
                    ModelState["BiMonthlyDay2"].Errors.Clear();
                    // Monthly
                    ModelState["MonthlyDOM"].Errors.Clear();
                    // Quarterly
                    ModelState["Quarterly1Month"].Errors.Clear();
                    ModelState["Quarterly1Day"].Errors.Clear();
                    ModelState["Quarterly2Month"].Errors.Clear();
                    ModelState["Quarterly2Day"].Errors.Clear();
                    ModelState["Quarterly3Month"].Errors.Clear();
                    ModelState["Quarterly3Day"].Errors.Clear();
                    ModelState["Quarterly4Month"].Errors.Clear();
                    ModelState["Quarterly4Day"].Errors.Clear();
                    // Semi-Annual
                    ModelState["SemiAnnual1Month"].Errors.Clear();
                    ModelState["SemiAnnual1Day"].Errors.Clear();
                    ModelState["SemiAnnual2Month"].Errors.Clear();
                    ModelState["SemiAnnual2Day"].Errors.Clear();
                    // Annual
                    ModelState["AnnualMOY"].Errors.Clear();
                    ModelState["AnnualDOM"].Errors.Clear();
                    break;
                case 5: // Bi-Monthly
                    if (!dateRange)
                    {
                        ModelState["BeginDate"].Errors.Clear();
                        ModelState["EndDate"].Errors.Clear();
                    }
                    // Weekly
                    ModelState["WeeklyDOW"].Errors.Clear();
                    // Every Other Week
                    ModelState["EverOtherWeekDOW"].Errors.Clear();
                    // Bi-Monthly
                    //ModelState["BiMonthlyDay1"].Errors.Clear();
                    //ModelState["BiMonthlyDay2"].Errors.Clear();
                    // Monthly
                    ModelState["MonthlyDOM"].Errors.Clear();
                    // Quarterly
                    ModelState["Quarterly1Month"].Errors.Clear();
                    ModelState["Quarterly1Day"].Errors.Clear();
                    ModelState["Quarterly2Month"].Errors.Clear();
                    ModelState["Quarterly2Day"].Errors.Clear();
                    ModelState["Quarterly3Month"].Errors.Clear();
                    ModelState["Quarterly3Day"].Errors.Clear();
                    ModelState["Quarterly4Month"].Errors.Clear();
                    ModelState["Quarterly4Day"].Errors.Clear();
                    // Semi-Annual
                    ModelState["SemiAnnual1Month"].Errors.Clear();
                    ModelState["SemiAnnual1Day"].Errors.Clear();
                    ModelState["SemiAnnual2Month"].Errors.Clear();
                    ModelState["SemiAnnual2Day"].Errors.Clear();
                    // Annual
                    ModelState["AnnualMOY"].Errors.Clear();
                    ModelState["AnnualDOM"].Errors.Clear();
                    break;
                case 6: // Monthly
                    if (!dateRange)
                    {
                        ModelState["BeginDate"].Errors.Clear();
                        ModelState["EndDate"].Errors.Clear();
                    }
                    // Weekly
                    ModelState["WeeklyDOW"].Errors.Clear();
                    // Every Other Week
                    ModelState["EverOtherWeekDOW"].Errors.Clear();
                    // Bi-Monthly
                    ModelState["BiMonthlyDay1"].Errors.Clear();
                    ModelState["BiMonthlyDay2"].Errors.Clear();
                    // Monthly
                    //ModelState["MonthlyDOM"].Errors.Clear();
                    // Quarterly
                    ModelState["Quarterly1Month"].Errors.Clear();
                    ModelState["Quarterly1Day"].Errors.Clear();
                    ModelState["Quarterly2Month"].Errors.Clear();
                    ModelState["Quarterly2Day"].Errors.Clear();
                    ModelState["Quarterly3Month"].Errors.Clear();
                    ModelState["Quarterly3Day"].Errors.Clear();
                    ModelState["Quarterly4Month"].Errors.Clear();
                    ModelState["Quarterly4Day"].Errors.Clear();
                    // Semi-Annual
                    ModelState["SemiAnnual1Month"].Errors.Clear();
                    ModelState["SemiAnnual1Day"].Errors.Clear();
                    ModelState["SemiAnnual2Month"].Errors.Clear();
                    ModelState["SemiAnnual2Day"].Errors.Clear();
                    // Annual
                    ModelState["AnnualMOY"].Errors.Clear();
                    ModelState["AnnualDOM"].Errors.Clear();
                    break;
                case 7: // Quarterly
                    if (!dateRange)
                    {
                        ModelState["BeginDate"].Errors.Clear();
                        ModelState["EndDate"].Errors.Clear();
                    }
                    // Weekly
                    ModelState["WeeklyDOW"].Errors.Clear();
                    // Every Other Week
                    ModelState["EverOtherWeekDOW"].Errors.Clear();
                    // Bi-Monthly
                    ModelState["BiMonthlyDay1"].Errors.Clear();
                    ModelState["BiMonthlyDay2"].Errors.Clear();
                    // Monthly
                    ModelState["MonthlyDOM"].Errors.Clear();
                    // Quarterly
                    //ModelState["Quarterly1Month"].Errors.Clear();
                    //ModelState["Quarterly1Day"].Errors.Clear();
                    //ModelState["Quarterly2Month"].Errors.Clear();
                    //ModelState["Quarterly2Day"].Errors.Clear();
                    //ModelState["Quarterly3Month"].Errors.Clear();
                    //ModelState["Quarterly3Day"].Errors.Clear();
                    //ModelState["Quarterly4Month"].Errors.Clear();
                    //ModelState["Quarterly4Day"].Errors.Clear();
                    // Semi-Annual
                    ModelState["SemiAnnual1Month"].Errors.Clear();
                    ModelState["SemiAnnual1Day"].Errors.Clear();
                    ModelState["SemiAnnual2Month"].Errors.Clear();
                    ModelState["SemiAnnual2Day"].Errors.Clear();
                    // Annual
                    ModelState["AnnualMOY"].Errors.Clear();
                    ModelState["AnnualDOM"].Errors.Clear();
                    break;
                case 8: // Semi-Annually
                    if (!dateRange)
                    {
                        ModelState["BeginDate"].Errors.Clear();
                        ModelState["EndDate"].Errors.Clear();
                    }
                    // Weekly
                    ModelState["WeeklyDOW"].Errors.Clear();
                    // Every Other Week
                    ModelState["EverOtherWeekDOW"].Errors.Clear();
                    // Bi-Monthly
                    ModelState["BiMonthlyDay1"].Errors.Clear();
                    ModelState["BiMonthlyDay2"].Errors.Clear();
                    // Monthly
                    ModelState["MonthlyDOM"].Errors.Clear();
                    // Quarterly
                    ModelState["Quarterly1Month"].Errors.Clear();
                    ModelState["Quarterly1Day"].Errors.Clear();
                    ModelState["Quarterly2Month"].Errors.Clear();
                    ModelState["Quarterly2Day"].Errors.Clear();
                    ModelState["Quarterly3Month"].Errors.Clear();
                    ModelState["Quarterly3Day"].Errors.Clear();
                    ModelState["Quarterly4Month"].Errors.Clear();
                    ModelState["Quarterly4Day"].Errors.Clear();
                    // Semi-Annual
                    //ModelState["SemiAnnual1Month"].Errors.Clear();
                    //ModelState["SemiAnnual1Day"].Errors.Clear();
                    //ModelState["SemiAnnual2Month"].Errors.Clear();
                    //ModelState["SemiAnnual2Day"].Errors.Clear();
                    // Annual
                    ModelState["AnnualMOY"].Errors.Clear();
                    ModelState["AnnualDOM"].Errors.Clear();
                    break;
                case 9: // Semi-Annually
                    if (!dateRange)
                    {
                        ModelState["BeginDate"].Errors.Clear();
                        ModelState["EndDate"].Errors.Clear();
                    }
                    // Weekly
                    ModelState["WeeklyDOW"].Errors.Clear();
                    // Every Other Week
                    ModelState["EverOtherWeekDOW"].Errors.Clear();
                    // Bi-Monthly
                    ModelState["BiMonthlyDay1"].Errors.Clear();
                    ModelState["BiMonthlyDay2"].Errors.Clear();
                    // Monthly
                    ModelState["MonthlyDOM"].Errors.Clear();
                    // Quarterly
                    ModelState["Quarterly1Month"].Errors.Clear();
                    ModelState["Quarterly1Day"].Errors.Clear();
                    ModelState["Quarterly2Month"].Errors.Clear();
                    ModelState["Quarterly2Day"].Errors.Clear();
                    ModelState["Quarterly3Month"].Errors.Clear();
                    ModelState["Quarterly3Day"].Errors.Clear();
                    ModelState["Quarterly4Month"].Errors.Clear();
                    ModelState["Quarterly4Day"].Errors.Clear();
                    // Semi-Annual
                    ModelState["SemiAnnual1Month"].Errors.Clear();
                    ModelState["SemiAnnual1Day"].Errors.Clear();
                    ModelState["SemiAnnual2Month"].Errors.Clear();
                    ModelState["SemiAnnual2Day"].Errors.Clear();
                    // Annual
                    //ModelState["AnnualMOY"].Errors.Clear();
                    //ModelState["AnnualDOM"].Errors.Clear();
                    break;
                default:
                    throw new Exception();
            }

            if (ModelState.IsValid)
            {
                if (_debitRepository.Save(detailItemView))
                {
                    DisplaySuccessMessage("Has updated a Debit record");
                    return RedirectToAction("Index");
                }
            }
            ViewBag.FkPeriod = _webHelpers.PeriodSelectList(detailItemView.FkPeriod);
            DisplayErrorMessage();
            return View(detailItemView);
        }

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     POST: ItemDetail/Debits/Delete/5
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>ActionResult</returns>
        /// ---------------------------------------------------------------------
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewData.Add("Action", "Delete");  // Delete Marker
            if (_debitRepository.Delete(id, User.Identity.Name))
            {
                DisplaySuccessMessage("Has deleted a Debit record");
            }
            else
            {
                DisplayErrorMessage();
            }

            return RedirectToAction("Index");
        }

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Success Message
        /// </summary>
        /// <param name="msgText">string</param>
        /// ---------------------------------------------------------------------
        private void DisplaySuccessMessage(string msgText)
        {
            TempData["SuccessMessage"] = msgText;
        }

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Error Message
        /// </summary>
        /// ---------------------------------------------------------------------
        private void DisplayErrorMessage()
        {
            TempData["ErrorMessage"] = "Save changes was unsuccessful.";
        }
    }
}