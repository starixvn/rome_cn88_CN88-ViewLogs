namespace Alpha.ViewLogs.Site.Tests.ViewModelsForTesting.Login
{
    using System.Text;
    using ViewModels;
    using ViewModels.Login;

    public class LoginLogItemViewModelForTesting : LoginLogItemViewModel
    {
        public static LoginLogItemViewModelForTesting FromLoginLogItemViewModel(LogItemViewModel logItemViewModel)
        {
            if (logItemViewModel == null)
            {
                return null;
            }

            var loginLogItemViewModel = (LoginLogItemViewModel)logItemViewModel;

            return new LoginLogItemViewModelForTesting
            {
                ActionId = loginLogItemViewModel.ActionId,
                ActionName = loginLogItemViewModel.ActionName,
                ActionTypeId = loginLogItemViewModel.ActionTypeId,
                CustId = loginLogItemViewModel.CustId,
                CustName = loginLogItemViewModel.CustName,
                IsInternal = loginLogItemViewModel.IsInternal,
                LogDate = loginLogItemViewModel.LogDate,
                LogId = loginLogItemViewModel.LogId,
                SPName = loginLogItemViewModel.SPName,
                UserId = loginLogItemViewModel.UserId,
                Username = loginLogItemViewModel.Username,
                TrackingValue = loginLogItemViewModel.TrackingValue,
                TrackingValueOld = loginLogItemViewModel.TrackingValueOld,
                Details = loginLogItemViewModel.Details,
                IP = loginLogItemViewModel.IP,

                Domain = loginLogItemViewModel.Domain,
                Browser = loginLogItemViewModel.Browser,
                Country = loginLogItemViewModel.Country
            };
        }

        public override bool Equals(object obj)
        {
            var compareObject = (LoginLogItemViewModel)obj;

            return ActionId == compareObject.ActionId
                && ActionName == compareObject.ActionName
                && ActionTypeId == compareObject.ActionTypeId
                && CustId == compareObject.CustId
                && CustName == compareObject.CustName
                && IsInternal == compareObject.IsInternal
                && LogDate == compareObject.LogDate
                && LogId == compareObject.LogId
                && SPName == compareObject.SPName
                && UserId == compareObject.UserId
                && Username == compareObject.Username
                && TrackingValue == compareObject.TrackingValue
                && TrackingValueOld == compareObject.TrackingValueOld
                && Details == compareObject.Details
                && IP == compareObject.IP
                && Domain == compareObject.Domain
                && Browser == compareObject.Browser
                && Country == compareObject.Country;
        }

        public override int GetHashCode()
        {
            return ActionId.GetHashCode()
                ^ ActionName.GetHashCode()
                ^ ActionTypeId.GetHashCode()
                ^ CustId.GetHashCode()
                ^ CustName.GetHashCode()
                ^ IsInternal.GetHashCode()
                ^ LogDate.GetHashCode()
                ^ ActionTypeId.GetHashCode()
                ^ LogId.GetHashCode()
                ^ SPName.GetHashCode()
                ^ UserId.GetHashCode()
                ^ Username.GetHashCode()
                ^ TrackingValue.GetHashCode()
                ^ TrackingValueOld.GetHashCode()
                ^ Details.GetHashCode()
                ^ IP.GetHashCode()
                ^ Domain.GetHashCode()
                ^ Browser.GetHashCode()
                ^ Country.GetHashCode();
        }

        public override string ToString()
        {
            var result = new StringBuilder();

            result.AppendLine("ActionId: " + ActionId);
            result.AppendLine("ActionName: " + ActionName);
            result.AppendLine("ActionTypeId: " + ActionTypeId);
            result.AppendLine("CustId: " + CustId);
            result.AppendLine("CustName: " + CustName);
            result.AppendLine("IsInternal: " + IsInternal);
            result.AppendLine("LogDate: " + LogDate);
            result.AppendLine("LogId: " + LogId);
            result.AppendLine("SPName: " + SPName);
            result.AppendLine("UserId: " + UserId);
            result.AppendLine("Username: " + Username);
            result.AppendLine("TrackingValue: " + TrackingValue);
            result.AppendLine("TrackingValueOld: " + TrackingValueOld);
            result.AppendLine("Details: " + Details);
            result.AppendLine("IP: " + IP);
            result.AppendLine("Domain: " + Domain);
            result.AppendLine("Browser: " + Browser);
            result.AppendLine("Country: " + Country);

            result.AppendLine(base.ToString());
            return result.ToString();
        }
    }
}
