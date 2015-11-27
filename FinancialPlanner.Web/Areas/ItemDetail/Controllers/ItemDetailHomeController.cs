using System.Web.Mvc;

namespace FinancialPlanner.Web.Areas.ItemDetail.Controllers
{
    [Authorize]
    public class ItemDetailHomeController : Controller
    {
        // GET: ItemDetail/IDHome
        public ActionResult Index()
        {
            return View();
        }
    }
}