namespace Alpha.ViewLogs.Site.Handlers
{
    using System.Collections.Generic;
    using Alpha.ViewLogs.Extensions;
    using Fanex.Common;
    using MediatR;
    using Models;
    using Requests;
    using Requests.Credit;
    using Services;
    using ViewLogs.Helpers;
    using ViewModels;

    public class CreditHandler : BaseHandler, IRequestHandler<IndexQuery, LogViewModel>
    {
        public CreditHandler(LogService logService, LogFormatter logFormatter, IDateTime dateTimeWrapper) :
            base(logService, logFormatter, dateTimeWrapper)
        {
        }

        public LogViewModel Handle(IndexQuery message)
        {
            var logsParam = CreateLogParam(message);
            var pagingLogs = LogService.GetPagingLogs(logsParam);
            var viewModel = new LogViewModel(logsParam, pagingLogs.TotalRecords);

            TransformLogs(pagingLogs, message, viewModel);

            viewModel.ActionTypeId = message.ActionTypeId;

            return viewModel;
        }

        public void TransformLogs(
            PagingLogs<Log> pagingLogs,
            LogQuery message,
            LogViewModel viewModel)
        {
            var logItems = new List<LogItemViewModel>();

            foreach (var log in pagingLogs.Logs)
            {
                if (message.ActionIdVisibilities.ContainsKey(log.ActionId)
                    && !message.ActionIdVisibilities[log.ActionId])
                {
                    viewModel.TotalRecords -= 1;
                    continue;
                }

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

                    TrackingValue = Converter.ToDecimal(log.TrackingValue).ToNumericString(),
                    TrackingValueOld = Converter.ToDecimal(log.TrackingValueOld).ToNumericString(),
                    Details = log.Details,
                    IP = LogFormatter.FormatIP(log.IP)
                });
            }

            viewModel.LogItems = logItems;
        }
    }
}