namespace Alpha.ViewLogs.Params
{
    using System;
    using System.Collections.Generic;

    public class LogParam
    {
        public int CustId { get; set; }

        public int UplineId { get; set; }

        public int SubAccId { get; set; }

        public byte RoleId { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string Username { get; set; }

        public bool HasViewLogPermission { get; set; }

        public bool IsInternal { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public string Language { get; set; }

        public bool IsShowOldValue { get; set; }

        public Dictionary<int, bool> ActionIdVisibilities { get; set; }

        public DateTime MaxReportDate { get; set; }

        public DateTime MinReportDate { get; set; }

        public DateTime LimitedDateToShowInfo { get; set; }
    }
}
