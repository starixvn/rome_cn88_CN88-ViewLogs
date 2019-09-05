namespace Alpha.ViewLogs.Site.Controllers
{
    using System.Web.Mvc;
    using MediatR;
    using Requests.Status;
    using Sunplus.Agent.Model.Entities;

    public class StatusController : ViewLogBaseController
    {
        private const string TemplatePath = "~/App_Data/TrackingValueTransformTemplates";
        private readonly IMediator mediator;

        public StatusController(IUserSession userSession, IMediator mediator)
            : base(userSession)
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