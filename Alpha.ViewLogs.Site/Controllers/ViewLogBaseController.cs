namespace Alpha.ViewLogs.Site.Controllers
{
    using System.Web.Mvc;
    using System.Web.SessionState;
    using CN88.Web.Controllers;
    using CN88.Web.Helpers;
    using Configurations;
    using Sunplus.Agent.Model.Entities;

    [SessionState(SessionStateBehavior.ReadOnly)]
    public abstract class ViewLogBaseController : CN88BaseController
    {
        protected ViewLogBaseController(IUserSession userSession)
        {
            UserSession = userSession;
        }

        public IUserSession UserSession { get; set; }

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            var pageAccess = UserSession.SubPageAccess;

            if (!pageAccess.ViewMemberInfo && !pageAccess.UpdateAccount)
            {
                filterContext.Result = new RedirectResult(MicroSites.MainUrl(AppSettings.AccessDeniedPage));
            }
        }
    }
}