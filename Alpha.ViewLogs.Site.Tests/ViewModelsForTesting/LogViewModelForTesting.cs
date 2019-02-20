namespace Alpha.ViewLogs.Site.Tests.ViewModelsForTesting
{
    using System.Linq;
    using System.Text;
    using Params;
    using Site.Helpers;
    using ViewModels;

    public class LogViewModelForTesting : LogViewModel
    {
        public LogViewModelForTesting(LogParam logParam, int totalRecords, MicroSites microSites) :
            base(logParam, totalRecords, microSites)
        {
        }

        public static LogViewModelForTesting FromLogViewModel(
            LogViewModel logViewModel,
            LogParam logParam,
            int totalRecords,
            MicroSites microSites)
        {
            if (logViewModel == null)
            {
                return null;
            }

            var logViewModelForTesting = new LogViewModelForTesting(logParam, totalRecords, microSites);
            logViewModelForTesting.HasViewLogPermission = logViewModel.HasViewLogPermission;
            logViewModelForTesting.IsSubAccount = logViewModel.IsSubAccount;
            logViewModelForTesting.RoleId = logViewModel.RoleId;
            logViewModelForTesting.ShowOldValuesCookieName = logViewModel.ShowOldValuesCookieName;
            logViewModelForTesting.ActionTypeId = logViewModel.ActionTypeId;
            logViewModelForTesting.ShowOldValue = logViewModel.ShowOldValue;
            logViewModelForTesting.TotalRecords = logViewModel.TotalRecords;
            logViewModelForTesting.PageSize = logViewModel.PageSize;
            logViewModelForTesting.PageIndex = logViewModel.PageIndex;
            logViewModelForTesting.DayInMonth = logViewModel.DayInMonth;
            logViewModelForTesting.FromDate = logViewModel.FromDate;
            logViewModelForTesting.ToDate = logViewModel.ToDate;
            logViewModelForTesting.MinReportDate = logViewModel.MinReportDate;
            logViewModelForTesting.MaxReportDate = logViewModel.MaxReportDate;
            logViewModelForTesting.Username = logViewModel.Username;

            logViewModelForTesting.LogItems = logViewModel.LogItems;

            return logViewModelForTesting;
        }

        public override bool Equals(object obj)
        {
            var compareObject = (LogViewModel)obj;

            var isEqual = (LogItems == null && compareObject.LogItems == null) ||
                (LogItems != null && compareObject.LogItems != null);

            if (isEqual)
            {
                foreach (var log in LogItems)
                {
                    var compareLog = LogItemViewModelForTesting.FromLogItemViewModel(compareObject.LogItems.Where(l => l.LogId == log.LogId).First());
                    if (compareLog == null || !compareLog.Equals(log))
                    {
                        isEqual = false;
                        break;
                    }
                }
            }

            isEqual &= HasViewLogPermission == compareObject.HasViewLogPermission
                && IsSubAccount == compareObject.IsSubAccount
                && RoleId == compareObject.RoleId
                && UserLevel == compareObject.UserLevel
                && ShowOldValuesCookieName == compareObject.ShowOldValuesCookieName
                && UsernameAutoSuggestionPath == compareObject.UsernameAutoSuggestionPath
                && ViewIPInfoPath == compareObject.ViewIPInfoPath
                && ActionTypeId == compareObject.ActionTypeId
                && HasLogs == compareObject.HasLogs
                && ShowOldValue == compareObject.ShowOldValue
                && TotalRecords == compareObject.TotalRecords
                && PageSize == compareObject.PageSize
                && PageIndex == compareObject.PageIndex
                && DayInMonth == compareObject.DayInMonth
                && FromDate == compareObject.FromDate
                && ToDate == compareObject.ToDate
                && MinReportDate == compareObject.MinReportDate
                && MaxReportDate == compareObject.MaxReportDate
                && Username == compareObject.Username;

            return isEqual;
        }

        public override int GetHashCode()
        {
            return HasViewLogPermission.GetHashCode()
                ^ IsSubAccount.GetHashCode()
                ^ RoleId.GetHashCode()
                ^ UserLevel.GetHashCode()
                ^ ShowOldValuesCookieName.GetHashCode()
                ^ UsernameAutoSuggestionPath.GetHashCode()
                ^ ViewIPInfoPath.GetHashCode()
                ^ ActionTypeId.GetHashCode()
                ^ HasLogs.GetHashCode()
                ^ ShowOldValue.GetHashCode()
                ^ TotalRecords.GetHashCode()
                ^ PageSize.GetHashCode()
                ^ PageIndex.GetHashCode()
                ^ LogItems.GetHashCode()
                ^ DayInMonth.GetHashCode()
                ^ FromDate.GetHashCode()
                ^ ToDate.GetHashCode()
                ^ MinReportDate.GetHashCode()
                ^ MaxReportDate.GetHashCode()
                ^ Username.GetHashCode();
        }

        public override string ToString()
        {
            var result = new StringBuilder();

            result.AppendLine("HasViewLogPermission: " + HasViewLogPermission);
            result.AppendLine("IsSubAccount: " + IsSubAccount);
            result.AppendLine("RoleId: " + RoleId);
            result.AppendLine("UserLevel: " + UserLevel);
            result.AppendLine("ShowOldValuesCookieName: " + ShowOldValuesCookieName);
            result.AppendLine("UsernameAutoSuggestionPath: " + UsernameAutoSuggestionPath);
            result.AppendLine("ViewIPInfoPath: " + ViewIPInfoPath);
            result.AppendLine("ActionTypeId: " + ActionTypeId);
            result.AppendLine("HasLogs: " + HasLogs);
            result.AppendLine("ShowOldValue: " + ShowOldValue);
            result.AppendLine("TotalRecords: " + TotalRecords);
            result.AppendLine("PageSize: " + PageSize);
            result.AppendLine("PageIndex: " + PageIndex);
            result.AppendLine("DayInMonth: " + DayInMonth);
            result.AppendLine("FromDate: " + FromDate);
            result.AppendLine("ToDate: " + ToDate);
            result.AppendLine("MinReportDate: " + MinReportDate);
            result.AppendLine("MaxReportDate: " + MaxReportDate);
            result.AppendLine("Username: " + Username);

            if (LogItems == null)
            {
                result.AppendLine("LogItems: " + LogItems);
            }
            else
            {
                foreach (var log in LogItems)
                {
                    result.AppendLine("Log: " + LogItemViewModelForTesting.FromLogItemViewModel(log));
                }
            }

            result.AppendLine(base.ToString());
            return result.ToString();
        }
    }
}
