using System.Web.Mvc;

namespace FinancialPlanner.Web.Areas.Display.Controllers
{
    /// =====================================================================
    /// <summary>
    ///     Display Home Controller
    /// </summary>
    /// =====================================================================
    [Authorize]
    public class DisplayHomeController : Controller
    {
        /// ---------------------------------------------------------------------
        /// <summary>
        /// ItemDetail Home Index
        /// -------------------------------------
        /// GET: Display/DHome
        /// </summary>
        /// <returns>ActionResult</returns>
        /// ---------------------------------------------------------------------
        public ActionResult Index()
        {
            return View();
        }
    }
}