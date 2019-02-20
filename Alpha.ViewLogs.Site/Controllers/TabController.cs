namespace Alpha.ViewLogs.Site.Controllers
{
    using System.Web.Mvc;
    using Constants;
    using ViewModels;

    public class TabController : Controller
    {
        [ChildActionOnly]
        public ActionResult Index(string tabName = TabType.Setting)
        {
            return PartialView(new TabViewModel(tabName));
        }
    }
}