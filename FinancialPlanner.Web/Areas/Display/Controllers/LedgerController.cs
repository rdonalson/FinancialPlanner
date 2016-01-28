using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using FinancialPlanner.Data.Entity;
using FinancialPlanner.Infrastructure.Domain.Display.Ledger.Repository;
using FinancialPlanner.Web.Areas.Display.Models;
using FinancialPlanner.Web.Helpers;
using FinancialPlanner.Web.Properties;
using Microsoft.AspNet.Identity;
using OfficeOpenXml;

namespace FinancialPlanner.Web.Areas.Display.Controllers
{
    /// =====================================================================
    /// <summary>
    ///     Ledger Controller
    /// </summary>
    /// =====================================================================
    public class LedgerController : Controller
    {
        private readonly ILedgerRepository _ledgerRepository;

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Base Constructor
        /// </summary>
        /// ---------------------------------------------------------------------
        public LedgerController()
        {
            var conndirection = Convert.ToInt32(Resources.CONN_DIRECTION);
            string server, database, userid, password;
            ConnectionHelpers.ConnStringParameters(conndirection, out server, out database, out userid, out password);
            _ledgerRepository = new LedgerRepository(conndirection, server, database, userid, password);
        }

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Display Ledger Criteria View
        ///     -------------------------------------
        ///     GET: Display/Ledger
        /// </summary>
        /// <returns>ActionResult</returns>
        /// ---------------------------------------------------------------------
        public ActionResult Index()
        {
            return View();
        }

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Receive "LedgerCriteriaViewModel" View Model data from
        ///     Ledger Criteria View
        ///     -------------------------------------
        ///     POST: Display/Ledger
        /// </summary>
        /// <param name="vm">LedgerCriteriaViewModel</param>
        /// <returns>ActionResult</returns>
        /// ---------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LedgerCriteriaViewModel vm)
        {
            vm.Result =
                _ledgerRepository.GetLedger(vm.timeFrameBegin, vm.timeFrameEnd, User.Identity.GetUserName()).ToList();
            if (vm.DoExport && vm.Result.Count > 0)
            {
                return LedgerExport(vm.Result);
            }
            return View(vm);
        }

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Exports Data from "spCreateLedgerReadout" procedure in the 
        ///     Ledger Repository to Excel
        /// </summary>
        /// <param name="data">List(spCreateLedgerReadout_Result)</param>
        /// <returns>ActionResult</returns>
        /// ---------------------------------------------------------------------
        private ActionResult LedgerExport(List<spCreateLedgerReadout_Result> data)
        {
            //Export Excel
            using (var package = new ExcelPackage())
            {
                var ws = package.Workbook.Worksheets.Add("Ledger");
                var currRow = 1;
                ws.Cells[currRow, 1].Value = "Date";
                ws.Cells[currRow, 2].Value = "Credit Summary";
                ws.Cells[currRow, 3].Value = "Debit Summary";
                ws.Cells[currRow, 4].Value = "Net Daily";
                ws.Cells[currRow, 5].Value = "Running Total";
                ws.Cells[currRow, 6].Value = "Item Type";
                ws.Cells[currRow, 7].Value = "Period Type";
                ws.Cells[currRow, 8].Value = "Item Name";
                ws.Cells[currRow, 9].Value = "Item Amount";
                foreach (var item in data)
                {
                    currRow += 1;
                    ws.Cells[currRow, 1].Value = item.WDate;
                    ws.Cells[currRow, 2].Value = item.CreditSummary;
                    ws.Cells[currRow, 3].Value = item.DebitSummary;
                    ws.Cells[currRow, 4].Value = item.NetDaily;
                    ws.Cells[currRow, 5].Value = item.RunningTotal;
                    ws.Cells[currRow, 6].Value = item.ItemType;
                    ws.Cells[currRow, 7].Value = item.PeriodName;
                    ws.Cells[currRow, 8].Value = item.Name;
                    ws.Cells[currRow, 9].Value = item.Amount;
                }
                //輸出
                var stream = new MemoryStream();
                package.SaveAs(stream);
                var fileName = string.Format("Ledger-{0}.xlsx", DateTime.Today.ToString("yyyy-MM-dd"));
                const string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                stream.Position = 0;
                return File(stream, contentType, fileName);
            }
        }
    }
}