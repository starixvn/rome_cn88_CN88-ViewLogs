namespace Alpha.ViewLogs.Tests.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading;
    using NUnit.Framework;
    using ViewLogs.Constants;
    using ViewLogs.Helpers;

    [TestFixture]
    public class DateTimeHelperTests
    {
        private Dictionary<string, string[]> daysInWeekByCulture;

        [SetUp]
        public void SetUp()
        {
            daysInWeekByCulture = new Dictionary<string, string[]>();
            daysInWeekByCulture.Add(LanguageCode.English, new string[] { "Sun", "Mon", "Tue", "Wed", "Thurs", "Fri", "Sat" });
            daysInWeekByCulture.Add(LanguageCode.Japanese, new string[] { "Sun", "Mon", "Tue", "Wed", "Thurs", "Fri", "Sat" });
            daysInWeekByCulture.Add(LanguageCode.Korean, new string[] { "일", "월", "화", "수요일", "목요일", "금", "토" });
            daysInWeekByCulture.Add(LanguageCode.Thai, new string[] { "วันอาทิตย์", "วันจันทร์", "วันอังคาร", "วันพุธ", "วันพฤหัส", "วันศุกร์", "วันเสาร์" });
            daysInWeekByCulture.Add(LanguageCode.Vietnamese, new string[] { "Chủ nhật", "Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7" });
            daysInWeekByCulture.Add(LanguageCode.ChineseSimplified, new string[] { "日", "一", "二", "三", "四", "五", "六" });
            daysInWeekByCulture.Add(LanguageCode.ChineseTraditional, new string[] { "日", "一", "二", "三", "四", "五", "六" });
        }

        [TestCase(2016, 8, 23, 0, 0, 0)] // Beginning of Date
        [TestCase(2016, 8, 23, 1, 1, 1)]
        [TestCase(2016, 8, 23, 12, 0, 0)] // Mid-date
        [TestCase(2016, 8, 23, 15, 1, 1)]
        [TestCase(2016, 8, 23, 23, 59, 59)] // Ending of date
        public void GetBeginningOfDate_DateTimeInDay_ReturnsBeginningOfDate(
            int year,
            int month,
            int day,
            int hour,
            int minute,
            int second)
        {
            // Arrange
            var inputDateTime = new DateTime(year, month, day, hour, minute, second);
            var expectedDateTime = new DateTime(year, month, day, 0, 0, 0);

            // Act 
            var actualDateTime = DateTimeHelper.GetBeginningOfDate(inputDateTime);

            // Assert
            Assert.AreEqual(expectedDateTime, actualDateTime);
        }

        [TestCase(2016, 8, 23, 0, 0, 0)] // Beginning of Date
        [TestCase(2016, 8, 23, 1, 1, 1)]
        [TestCase(2016, 8, 23, 12, 0, 0)] // Mid-date
        [TestCase(2016, 8, 23, 15, 1, 1)]
        [TestCase(2016, 8, 23, 23, 59, 59)] // Ending of date
        public void GetEndingOfDate_DateTimeInDay_ReturnsEndingOfDate(
            int year,
            int month,
            int day,
            int hour,
            int minute,
            int second)
        {
            // Arrange
            var inputDateTime = new DateTime(year, month, day, hour, minute, second);
            var expectedDateTime = new DateTime(year, month, day, 23, 59, 59);

            // Act 
            var actualDateTime = DateTimeHelper.GetEndingOfDate(inputDateTime);

            // Assert
            Assert.AreEqual(expectedDateTime, actualDateTime);
        }

        [TestCase(2016, 8, 23, 0, 0, 0, "08/23/2016 12:00:00 AM")] // Beginning of Date
        [TestCase(2016, 8, 23, 1, 1, 1, "08/23/2016 01:01:01 AM")]
        [TestCase(2016, 8, 23, 12, 0, 0, "08/23/2016 12:00:00 PM")] // Mid-date
        [TestCase(2016, 8, 23, 15, 1, 1, "08/23/2016 03:01:01 PM")]
        [TestCase(2016, 8, 23, 23, 59, 59, "08/23/2016 11:59:59 PM")] // Ending of date
        public void ToLongDateWithSlash_DateTimeInDay_ReturnsLongDateWithSlash(
            int year,
            int month,
            int day,
            int hour,
            int minute,
            int second,
            string expectedValue)
        {
            // Arrange
            var inputDateTime = new DateTime(year, month, day, hour, minute, second);

            // Act 
            var actualDateTime = DateTimeHelper.ToLongDateWithSlash(inputDateTime);

            // Assert
            Assert.AreEqual(expectedValue, actualDateTime);
        }

        [TestCase(2016, 8, 23, 0, 0, 0, "08/23/2016<br/>12:00:00 AM")] // Beginning of Date
        [TestCase(2016, 8, 23, 1, 1, 1, "08/23/2016<br/>01:01:01 AM")]
        [TestCase(2016, 8, 23, 12, 0, 0, "08/23/2016<br/>12:00:00 PM")] // Mid-date
        [TestCase(2016, 8, 23, 15, 1, 1, "08/23/2016<br/>03:01:01 PM")]
        [TestCase(2016, 8, 23, 23, 59, 59, "08/23/2016<br/>11:59:59 PM")] // Ending of date
        public void ToHtmlLongDateWithSlash_DateTimeInDay_ReturnsHtmlLongDateWithSlash(
            int year,
            int month,
            int day,
            int hour,
            int minute,
            int second,
            string expectedValue)
        {
            // Arrange
            var inputDateTime = new DateTime(year, month, day, hour, minute, second);

            // Act 
            var actualDateTime = DateTimeHelper.ToHtmlLongDateWithSlash(inputDateTime);

            // Assert
            Assert.AreEqual(expectedValue, actualDateTime);
        }

        [TestCase(LanguageCode.English)]
        [TestCase(LanguageCode.ChineseTraditional)]
        [TestCase(LanguageCode.ChineseSimplified)]
        [TestCase(LanguageCode.Japanese)]
        [TestCase(LanguageCode.Thai)]
        [TestCase(LanguageCode.Korean)]
        [TestCase(LanguageCode.Vietnamese)]
        public void DaysInWeek_ByCultureName_ReturnsCorrectResources(string cultureName)
        {
            // Arrange
            Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            // Act 
            var actualDaysInWeek = DateTimeHelper.DaysInWeek();

            // Assert
            Assert.IsTrue(daysInWeekByCulture.ContainsKey(cultureName));
            Assert.AreEqual(daysInWeekByCulture[cultureName], actualDaysInWeek);
        }
    }
}
