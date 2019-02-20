namespace Alpha.ViewLogs.Tests.ModelsForTesting
{
    using System.Text;
    using ViewLogs.Models;

    public class LogForTesting : Log
    {
        public static LogForTesting FromLog(Log log)
        {
            if (log == null)
            {
                return null;
            }

            return new LogForTesting
            {
                ActionId = log.ActionId,
                ActionName = log.ActionName,
                ActionTypeId = log.ActionTypeId,
                CustId = log.CustId,
                CustName = log.CustName,
                IsInternal = log.IsInternal,
                LogDate = log.LogDate,
                LogId = log.LogId,
                StoreProcedureName = log.StoreProcedureName,
                UserId = log.UserId,
                Username = log.Username,
                TrackingValue = log.TrackingValue,
                TrackingValueOld = log.TrackingValueOld,
                IP = log.IP,
                Details = log.Details
            };
        }

        public override bool Equals(object obj)
        {
            var compareObject = (Log)obj;

            var isEqual = ActionId == compareObject.ActionId &&
                ActionName == compareObject.ActionName &&
                ActionTypeId == compareObject.ActionTypeId &&
                CustId == compareObject.CustId &&
                CustName == compareObject.CustName &&
                IsInternal == compareObject.IsInternal &&
                LogDate == compareObject.LogDate &&
                LogId == compareObject.LogId &&
                StoreProcedureName == compareObject.StoreProcedureName &&
                UserId == compareObject.UserId &&
                Username == compareObject.Username &&
                TrackingValue == compareObject.TrackingValue &&
                TrackingValueOld == compareObject.TrackingValueOld &&
                IP == compareObject.IP &&
                Details == compareObject.Details;

            return isEqual;
        }

        public override int GetHashCode()
        {
            return ActionId.GetHashCode() ^
                ActionName.GetHashCode() ^
                ActionTypeId.GetHashCode() ^
                CustId.GetHashCode() ^
                CustName.GetHashCode() ^
                IsInternal.GetHashCode() ^
                LogDate.GetHashCode() ^
                LogId.GetHashCode() ^
                StoreProcedureName.GetHashCode() ^
                UserId.GetHashCode() ^
                Username.GetHashCode() ^
                TrackingValue.GetHashCode() ^
                TrackingValueOld.GetHashCode() ^
                IP.GetHashCode() ^
                Details.GetHashCode();
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
            result.AppendLine("SPName: " + StoreProcedureName);
            result.AppendLine("UserId: " + UserId);
            result.AppendLine("Username: " + Username);
            result.AppendLine("TrackingValue: " + TrackingValue);
            result.AppendLine("TrackingValueOld: " + TrackingValueOld);
            result.AppendLine("IP: " + IP);
            result.AppendLine("Details: " + Details);

            result.AppendLine(base.ToString());
            return result.ToString();
        }
    }
}
