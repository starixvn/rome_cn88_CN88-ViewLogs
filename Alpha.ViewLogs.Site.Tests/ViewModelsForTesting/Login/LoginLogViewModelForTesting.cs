namespace Alpha.ViewLogs.Site.Tests.ViewModelsForTesting.Login
{
    using System.Linq;
    using Params;
    using Site.Helpers;
    using ViewModels;

    public class LoginLogViewModelForTesting : LogViewModelForTesting
    {
        public LoginLogViewModelForTesting(LogParam logParam, int totalRecords, MicroSites microSites) :
            base(logParam, totalRecords, microSites)
        {
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
                    var compareLog = LoginLogItemViewModelForTesting.FromLoginLogItemViewModel(compareObject.LogItems.Where(l => l.LogId == log.LogId).First());
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
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
