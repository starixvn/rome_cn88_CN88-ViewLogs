namespace Alpha.ViewLogs.Site.Controllers
{
    using System.Web.Mvc;
    using Helpers;
    using MediatR;
    using Requests.Setting;
    using Sunplus.Agent.Model.Entities;

    public class SettingController : ViewLogBaseController
    {
        private const string TemplatePath = "~/App_Data/TrackingValueTransformTemplates";
        private readonly IMediator mediator;

        public SettingController(
            IUserSession userSession,
            IMediator mediator,
            MicroSites microSites) : base(userSession, microSites)
        {
            this.mediator = mediator;
        }

        public ActionResult Index(IndexQuery indexQuery)
        {
            indexQuery.TrackingValueTransformTemplatePath = HttpContext.Server.MapPath(TemplatePath);

            var viewModel = mediator.Send(indexQuery);

            return View(viewModel);
        }
    }
}