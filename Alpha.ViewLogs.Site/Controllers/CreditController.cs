namespace Alpha.ViewLogs.Site.Controllers
{
    using System.Web.Mvc;
    using MediatR;
    using Requests.Credit;
    using Sunplus.Agent.Model.Entities;

    public class CreditController : ViewLogBaseController
    {
        private readonly IMediator mediator;

        public CreditController(IUserSession userSession, IMediator mediator) : base(userSession)
        {
            this.mediator = mediator;
        }

        public ActionResult Index(IndexQuery indexQuery)
        {
            var viewModel = mediator.Send(indexQuery);

            return View(viewModel);
        }
    }
}