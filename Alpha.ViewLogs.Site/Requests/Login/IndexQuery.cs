namespace Alpha.ViewLogs.Site.Requests.Login
{
    using System.Collections.Generic;
    using MediatR;
    using Sunplus.Agent.Model.Entities;
    using ViewLogs.Helpers;
    using ViewModels.Login;

    public class IndexQuery : LogQuery, IRequest<IndexViewModel>
    {
        public IndexQuery() : this(null, null, null)
        {
        }

        public IndexQuery(
            IUserSession currentUserSession,
            IDateTime dateTimeWrap,
            bool? isShowOldValue,
            Dictionary<int, bool> actionIdVisibilities = null,
            Dictionary<string, bool> sportBetTypeVisibilities = null,
            Dictionary<string, int> resourcesByBetTypeCacheRef = null,
            Dictionary<string, int> resourcesByProductCacheRef = null,
            bool? isShowMyanmarOdds = null) :
            base(
                currentUserSession,
                dateTimeWrap,
                isShowOldValue,
                actionIdVisibilities,
                sportBetTypeVisibilities,
                resourcesByBetTypeCacheRef,
                resourcesByProductCacheRef,
                isShowMyanmarOdds)
        {
        }

        public bool IsAgentViewing { get; set; }
    }
}