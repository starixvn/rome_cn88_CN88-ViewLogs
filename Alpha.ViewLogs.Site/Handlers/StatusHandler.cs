namespace Alpha.ViewLogs.Site.Handlers
{
    using System.Collections.Generic;
    using System.Web;
    using Helpers;
    using MediatR;
    using Models;
    using Params;
    using Requests.Status;
    using Services;
    using ViewLogs.Helpers;
    using ViewModels;

    public class StatusHandler : BaseHandler, IRequestHandler<IndexQuery, LogViewModel>
    {
        private readonly TrackingValueFormatter trackingValueFormatter;
        private readonly TransformService transformService;

        public StatusHandler(
            LogService logService,
            LogFormatter logFormatter,
            IDateTime dateTimeWrapper,
            TransformService transformService,
            TrackingValueFormatter trackingValueFormatter) :
             base(logService, logFormatter, dateTimeWrapper)
        {
            this.trackingValueFormatter = trackingValueFormatter;
            this.transformService = transformService;
        }

        public LogViewModel Handle(IndexQuery message)
        {
            var logsParam = CreateLogParam(message);
            var pagingLogs = LogService.GetPagingLogs(logsParam);

            var viewModel = new LogViewModel(logsParam, pagingLogs.TotalRecords);
            viewModel.ActionTypeId = message.ActionTypeId;

            TransformLogs(pagingLogs, message, viewModel);

            return viewModel;
        }

        private void TransformLogs(
            PagingLogs<Log> pagingLogs,
            IndexQuery message,
            LogViewModel viewModel)
        {
            var transformParam = new TransformParam
            {
                IsShowOldValue = message.IsShowOldValue,
                TrackingValueTransformTemplatePath = message.TrackingValueTransformTemplatePath,
                CustStatuses = message.SportBetTypeVisibilities,
                ResourcesByBetTypeCacheRef = message.ResourcesByBetTypeCacheRef,
                ResourcesProductCacheRef = message.ResourcesByProductCacheRef,
                IsShowMyanmarOdds = message.IsShowMyanmarOdds,
                IsShowPokerGames = message.IsShowPokerGames
            };

            var logItems = new List<LogItemViewModel>();

            foreach (var log in pagingLogs.Logs)
            {
                if (message.ActionIdVisibilities.ContainsKey(log.ActionId)
                    && !message.ActionIdVisibilities[log.ActionId])
                {
                    viewModel.TotalRecords -= 1;
                    continue;
                }

                var formattedTrackingValue = trackingValueFormatter.GetFormattedDiffTrackingValue(log, message.IsShowOldValue);
                var htmlTrackingValue = transformService.TransformTrackingValue(formattedTrackingValue, log.ActionId, transformParam);

                logItems.Add(
                    new LogItemViewModel
                    {
                        ActionId = log.ActionId,
                        ActionName = LogService.GetLogActionName(log.ActionId, message.Language),

                        ActionTypeId = log.ActionTypeId,
                        CustId = log.CustId,
                        CustName = log.CustName,
                        LogDate = DateTimeHelper.ToLongDateWithSlash(log.LogDate),
                        LogId = log.LogId,
                        SPName = log.StoreProcedureName,
                        UserId = log.UserId,
                        Username = log.Username,
                        TrackingValue = HttpUtility.HtmlDecode(htmlTrackingValue),
                        IP = LogFormatter.FormatIP(log.IP)
                    });
            }

            viewModel.LogItems = logItems;
        }
    }
}