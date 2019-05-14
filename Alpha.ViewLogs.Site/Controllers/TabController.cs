namespace Alpha.ViewLogs.Site.Controllers
{
    using System.Web.Mvc;
    using Constants;
    using Helpers;
    using ViewModels;

    public class TabController : Controller
    {
        private BrowserHelper browserHelper;

        public TabController()
        {
            browserHelper = new BrowserHelperImpl();
        }

        [ChildActionOnly]
        public ActionResult Index(string tabName = TabType.Setting)
        {
            return PartialView(new TabViewModel(tabName, browserHelper));
        }
    }
}