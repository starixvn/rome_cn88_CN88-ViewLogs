namespace Alpha.ViewLogs.Site.Controllers
{
    using System.Web.Mvc;
    using CN88.Web.Helpers;
    using Configurations;
    using MediatR;
    using Requests.Setting;
    using Sunplus.Agent.Model.Entities;

    public class SettingController : ViewLogBaseController
    {
        private const string TemplatePath = "~/App_Data/TrackingValueTransformTemplates";
        private readonly IMediator mediator;

        public SettingController(IUserSession userSession, IMediator mediator)
            : base(userSession)
        {
            this.mediator = mediator;
        }

        public ActionResult Index(IndexQuery indexQuery)
        {
            indexQuery.TrackingValueTransformTemplatePath = HttpContext.Server.MapPath(TemplatePath);

            if (indexQuery.Invalid())
            {
                return Redirect(MicroSites.MainUrl(AppSettings.AccessDeniedPage));
            }

            var viewModel = mediator.Send(indexQuery);

            return View(viewModel);
        }
    }
}
