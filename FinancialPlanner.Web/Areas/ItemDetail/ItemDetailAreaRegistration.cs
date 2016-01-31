using System.Web.Mvc;

namespace FinancialPlanner.Web.Areas.ItemDetail
{
    public class ItemDetailAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "ItemDetail"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ItemDetail_default",
                "ItemDetail/{controller}/{action}/{id}",
                new {action = "Index", id = UrlParameter.Optional}
                );
        }
    }
}