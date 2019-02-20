namespace Alpha.ViewLogs.Site.Requests.Setting
{
    using System.Collections.Generic;
    using MediatR;
    using Sunplus.Agent.Model.Entities;
    using ViewLogs.Helpers;
    using ViewModels.Setting;

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
            bool? isShowMyanmarOdds = null,
            bool? isShowPokerGames = null) :
            base(
                currentUserSession,
                dateTimeWrap,
                isShowOldValue,
                actionIdVisibilities,
                sportBetTypeVisibilities,
                resourcesByBetTypeCacheRef,
                resourcesByProductCacheRef,
                isShowMyanmarOdds,
                isShowPokerGames)
        {
        }

        public string TrackingValueTransformTemplatePath { get; set; }
    }
}