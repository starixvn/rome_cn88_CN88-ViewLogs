namespace Alpha.ViewLogs.Site.Handlers
{
    using System.Collections.Generic;
    using MediatR;
    using Models;
    using Models.Enumerations;
    using Requests.Login;
    using Services;
    using ViewLogs.Helpers;
    using ViewModels;
    using ViewModels.Login;

    public class LoginHandler : BaseHandler, IRequestHandler<IndexQuery, IndexViewModel>
    {
        public LoginHandler(LogService logService, LogFormatter logFormatter, IDateTime dateTimeWrapper) :
            base(logService, logFormatter, dateTimeWrapper)
        {
        }

        public IndexViewModel Handle(IndexQuery message)
        {
            var logsParam = CreateLogParam(message);
            var pagingLogs = LogService.GetPagingLogs(logsParam);

            var viewModel = new IndexViewModel(logsParam, pagingLogs.TotalRecords);

            TransformLogs(pagingLogs, message, viewModel);

            viewModel.PreviousUsername = message.PreviousUsername;
            viewModel.ActionTypeId = message.ActionTypeId;
            viewModel.IsAgent = viewModel.RoleId == UserLevel.Agent.Value;
            viewModel.Username = viewModel.IsAgent ? viewModel.PreviousUsername : viewModel.Username;

            return viewModel;
        }

        private void TransformLogs(
            PagingLogs<Log> pagingLogs,
            IndexQuery message,
            LogViewModel viewModel)
        {
            var logItems = new List<LogItemViewModel>();

            foreach (LoginLog log in pagingLogs.Logs)
            {
                if (message.ActionIdVisibilities.ContainsKey(log.ActionId) && !message.ActionIdVisibilities[log.ActionId])
                {
                    viewModel.TotalRecords -= 1;
                    continue;
                }

                logItems.Add(new LoginLogItemViewModel
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

                    Details = log.Details,
                    IP = LogFormatter.FormatIP(log.IP),

                    Domain = log.Domain,
                    Browser = log.Browser,
                    Country = LogFormatter.FormatCountryCode(log.IP, log.Country)
                });
            }

            viewModel.LogItems = logItems;
        }
    }
}