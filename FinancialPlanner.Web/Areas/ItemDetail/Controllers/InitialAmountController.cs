using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FinancialPlanner.Data.Entity;
using FinancialPlanner.Infrastructure.Domain.ItemDetail.Initial.Repository;
using FinancialPlanner.Infrastructure.Domain.ItemDetail.ViewModels;
using FinancialPlanner.Web.Helpers;
using FinancialPlanner.Web.Properties;

namespace FinancialPlanner.Web.Areas.ItemDetail.Controllers
{
    /// =====================================================================
    /// <summary>
    ///     Initial Amount Controller
    /// </summary>
    /// =====================================================================
    [Authorize]
    public class InitialAmountController : Controller
    {
        private readonly IInitialAmountRepository _initialAmountRepository;

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Base Constructor
        /// </summary>
        /// ---------------------------------------------------------------------
        public InitialAmountController()
        {
            var conndirection = Convert.ToInt32(Resources.CONN_DIRECTION);
            string server, database, userid, password;
            ConnectionHelpers.ConnStringParameters(conndirection, out server, out database, out userid, out password);
            _initialAmountRepository = new InitialAmountRepository(conndirection, server, database, userid, password);
        }

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     GET: ItemDetail/InitialAmount
        /// </summary>
        /// <returns>ActionResult</returns>
        /// ---------------------------------------------------------------------
        public ActionResult Index()
        {
            IQueryable<InitialAmount> initialAmounts = _initialAmountRepository.GetInitialAmounts(User.Identity.Name);
            ViewData.Add("Exists", initialAmounts.Count() != 0 ? "true": "false");
            return View(initialAmounts.ToList());
        }

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     GET: ItemDetail/InitialAmount/Create
        /// </summary>
        /// <returns>ActionResult</returns>
        /// ---------------------------------------------------------------------
        public ActionResult Create()
        {
            ViewData.Add("Action", "Create"); // Insert Marker
            return View();
        }

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     POST: ItemDetail/InitialAmount/Create
        ///     To protect from overposting attacks, please enable the specific properties you want to bind to, for
        ///     more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// </summary>
        /// <param name="initialAmountView">InitialAmountView</param>
        /// <returns>ActionResult</returns>
        /// ---------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(
                Include = "PkID,UserName,StrAmount,BeginDate"
                )] InitialAmountView initialAmountView)
        {
            if (ModelState.IsValid)
            {
                initialAmountView.UserName = User.Identity.Name;
                if (_initialAmountRepository.Add(initialAmountView))
                {
                    DisplaySuccessMessage("Has created the Initial Amount record");
                    return RedirectToAction("Index");
                }
            }
            DisplayErrorMessage();
            return View(initialAmountView);
        }

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     GET: ItemDetail/InitialAmount/Edit/5
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

            // Get Base Record
            InitialAmountView initialAmountView = _initialAmountRepository.GetInitialAmountView(id, User.Identity.Name);
            ViewData.Add("Action", "Edit"); // Edit Marker

            return View(initialAmountView);
        }

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     POST: ItemDetail/InitialAmount/Edit/5
        ///     To protect from overposting attacks, please enable the specific properties you want to bind to, for more details
        ///     see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// </summary>
        /// <param name="initialAmountView">InitialAmountView</param>
        /// <returns>ActionResult</returns>
        /// ---------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(
                Include =
                    "PkID,UserName,StrAmount,BeginDate"
                )] InitialAmountView initialAmountView)
        {
            if (ModelState.IsValid)
            {
                if (_initialAmountRepository.Save(initialAmountView))
                {
                    DisplaySuccessMessage("Has updated the Initial Amount record");
                    return RedirectToAction("Index");
                }
            }
            DisplayErrorMessage();
            return View(initialAmountView);
        }

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     POST: ItemDetail/Credits/Delete/5
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>ActionResult</returns>
        /// ---------------------------------------------------------------------
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewData.Add("Action", "Delete"); // Delete Marker
            if (_initialAmountRepository.Delete(id, User.Identity.Name))
            {
                DisplaySuccessMessage("Has deleted the Initial Amount record");
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