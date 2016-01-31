using System.Web.Mvc;

namespace FinancialPlanner.Web.Areas.ItemDetail.Controllers
{
    /// =====================================================================
    /// <summary>
    ///     ItemDetail Home Controller
    /// </summary>
    /// =====================================================================
    [Authorize]
    public class ItemDetailHomeController : Controller
    {
        /// ---------------------------------------------------------------------
        /// <summary>
        /// ItemDetail Home Index
        /// -------------------------------------
        /// GET: ItemDetail/IDHome
        /// </summary>
        /// <returns>ActionResult</returns>
        /// ---------------------------------------------------------------------
        public ActionResult Index()
        {
            return View();
        }
    }
}