namespace Alpha.ViewLogs.Site.Handlers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using AppResources;
    using Fanex.Common;
    using Helpers;
    using MediatR;
    using Models;
    using Params;
    using Requests.Setting;
    using Services;
    using ViewLogs.Constants;
    using ViewLogs.Helpers;
    using ViewModels;
    using ViewModels.Setting;

    public class SettingHandler : BaseHandler, IRequestHandler<IndexQuery, IndexViewModel>
    {
        private readonly TrackingValueFormatter trackingValueFormatter;
        private readonly TransformService transformService;

        public SettingHandler(
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

        public IndexViewModel Handle(IndexQuery message)
        {
            var fromDate = GetFromDate(message);
            var toDate = GetToDate(message);

            var pageSize = string.IsNullOrWhiteSpace(message.PageSize) ? DefaultVariables.PageSize : Converter.ToInt(message.PageSize);

            var logsParam = new SettingParam
            {
                UplineId = message.UplineId,
                SubAccId = message.SubAccId,
                RoleId = message.RoleId,
                FromDate = fromDate,
                ToDate = toDate,
                Username = message.Username,
                IsInternal = message.IsInternal,

                PageSize = pageSize == 0 ? DefaultVariables.PageSize : pageSize,
                PageIndex = string.IsNullOrWhiteSpace(message.PageIndex) ? 0 : Converter.ToInt(message.PageIndex),

                Language = message.Language,
                HasViewLogPermission = message.HasViewLogPermission,
                ActionIdVisibilities = message.ActionIdVisibilities,

                MaxReportDate = message.MaxReportDate,
                MinReportDate = message.MinReportDate,
                IsShowOldValue = message.IsShowOldValue,
                ActionTypeId = string.IsNullOrWhiteSpace(message.ActionTypeId) ? ActionType.All : Converter.ToInt(message.ActionTypeId),
                LimitedDateToShowInfo = message.LimitedDateForShowInfo
            };

            var pagingLogs = LogService.GetPagingLogs(logsParam);

            var viewModel = new IndexViewModel(logsParam, pagingLogs.TotalRecords);
            viewModel.LogActionTypes = BuildLogActionTypeViewModels(message);
            viewModel.ActionTypeId = message.ActionTypeId;

            TransformLogs(pagingLogs, message, viewModel);

            return viewModel;
        }

        private IEnumerable<LogActionTypeViewModel> BuildLogActionTypeViewModels(IndexQuery message)
        {
            var logActionTypes = LogService.GetLogActionTypes();

            var logActionTypeViewModels = logActionTypes
                .Select(logActionType => new LogActionTypeViewModel(logActionType, message.Language))
                .ToList();

            logActionTypeViewModels.Insert(0, new LogActionTypeViewModel(ActionType.All, Resources.AllTypes));

            return logActionTypeViewModels;
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

                logItems.Add(new LogItemViewModel
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