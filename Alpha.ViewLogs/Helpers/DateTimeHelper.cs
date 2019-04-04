namespace Alpha.ViewLogs.Helpers
{
    using System;
    using AppResources;

    public static class DateTimeHelper
    {
        private const string LongDateWithSlash = "yyyy-MM-dd HH:mm:ss";
        private const string HtmlLongDateWithSlash = "yyyy-MM-dd<br/>HH:mm:ss";

        public static string[] DaysInWeek()
        {
            return new[]
            {
                Resources.Sun,
                Resources.Mon,
                Resources.Tue,
                Resources.Wed,
                Resources.Thurs,
                Resources.Fri,
                Resources.Sat
            };
        }

        public static DateTime GetBeginningOfDate(DateTime dateTime)
        {
            var beginningDateTime = dateTime.ToShortDateString() + " 00:00:00 AM";

            return Convert.ToDateTime(beginningDateTime);
        }

        public static DateTime GetEndingOfDate(DateTime dateTime)
        {
            var endingDateTime = dateTime.ToShortDateString() + " 23:59:59 PM";

            return Convert.ToDateTime(endingDateTime);
        }

        public static string ToLongDateWithSlash(DateTime dateTime)
        {
            return dateTime.ToString(LongDateWithSlash);
        }

        public static string ToHtmlLongDateWithSlash(DateTime dateTime)
        {
            return dateTime.ToString(HtmlLongDateWithSlash);
        }
    }
}
