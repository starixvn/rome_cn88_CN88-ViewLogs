namespace Alpha.ViewLogs.Tests.ModelsForTesting
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ViewLogs.Models;

    public class PagingLogsForTesting<LogType> : PagingLogs<LogType> where LogType : Log
    {
        public static PagingLogsForTesting<LogType> FromPagingLogs(PagingLogs<LogType> pagingLogs)
        {
            if (pagingLogs == null)
            {
                return null;
            }

            return new PagingLogsForTesting<LogType>()
            {
                TotalRecords = pagingLogs.TotalRecords,
                Logs = (IEnumerable<LogType>)pagingLogs.Logs.Select(log => LogForTesting.FromLog(log))
            };
        }

        /// <summary>
        /// Equals: used for compare two object (Unit test)
        /// </summary>
        public override bool Equals(object obj)
        {
            var compareObject = (PagingLogs<LogType>)obj;
            if ((Logs == null && compareObject.Logs != null) ||
                (Logs != null && compareObject.Logs == null) ||
                (Logs.Count() != compareObject.Logs.Count()))
            {
                return false;
            }

            var isEqual = true;
            foreach (var log in Logs)
            {
                var compareLog = compareObject.Logs.Where(l => l.LogId == log.LogId).First();
                if (compareLog == null || !compareLog.Equals(log))
                {
                    isEqual = false;
                    break;
                }
            }

            isEqual &= TotalRecords == compareObject.TotalRecords;

            return isEqual;
        }

        public override int GetHashCode()
        {
            return Logs.GetHashCode() ^ TotalRecords.GetHashCode();
        }

        /// <summary>
        /// ToString: use for displaying compare result two object (Unit test)
        /// </summary>
        public override string ToString()
        {
            var result = new StringBuilder();

            result.AppendLine("Data:");
            foreach (var log in Logs)
            {
                result.AppendLine("Log: " + log);
            }

            result.AppendLine();
            result.AppendLine("TotalRecords: " + TotalRecords);

            result.AppendLine(base.ToString());
            return result.ToString();
        }
    }
}
