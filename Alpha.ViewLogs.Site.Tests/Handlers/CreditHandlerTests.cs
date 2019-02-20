namespace Alpha.ViewLogs.Site.Tests.Handlers
{
    using System;
    using System.Collections.Generic;
    using Fanex.Common;
    using Models;
    using NSubstitute;
    using NUnit.Framework;
    using Params;
    using Requests.Credit;
    using Services;
    using Site.Handlers;
    using ViewLogs.Constants;
    using ViewModels;
    using ViewModelsForTesting;

    [TestFixture]
    public class CreditHandlerTests : ConcreteLogHandlerTests
    {
        private CreditHandler creditHandler;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            creditHandler = new CreditHandler(LogService, LogFormatter, DateTimeWrapper);
        }

        [Test]
        public void HandleIndexQuery_NullIndexQuery_ThrowsNullReferenceException()
        {
            // Arrange
            IndexQuery indexQuery = null;

            // Act & Assert
            Assert.Throws<NullReferenceException>(new TestDelegate(() => creditHandler.Handle(indexQuery)));
        }

        [Test]
        public void HandleIndexQuery_NullFromToDateAndTodayIsBeginningOfMonth_GetsPagingLogsFromBeginningOfMonthToBeginningOfMonth()
        {
            // Arrange
            var indexQuery = new IndexQuery(
                CurrentUserSession,
                DateTimeWrapper,
                true,
                ActionIdVisibilities,
                CustStatuses,
                ResourcesByBetTypeCacheRef,
                ResourcesByProductCacheRef,
                false);

            var toDate = new DateTime(2016, 10, 1);
            DateTimeWrapper.Now.Returns(toDate);

            LogService.GetPagingLogs(Arg.Any<LogParam>()).Returns(new PagingLogs<Log>());

            // Act 
            var actualLogViewModel = creditHandler.Handle(indexQuery);

            // Assert
            LogService.Received().GetPagingLogs(Arg.Is<LogParam>(logParam => logParam.FromDate == toDate && logParam.ToDate == toDate));
        }

        [Test]
        public void HandleIndexQuery_NullFromToDateAndTodayIsNotBeginningOfMonth_GetsPagingLogsFromYesterdayToToday()
        {
            // Arrange
            var indexQuery = new IndexQuery(
                CurrentUserSession,
                DateTimeWrapper,
                true,
                ActionIdVisibilities,
                CustStatuses,
                ResourcesByBetTypeCacheRef,
                ResourcesByProductCacheRef,
                false);

            var toDate = new DateTime(2016, 10, 2);
            var yesterday = new DateTime(2016, 10, 1);
            DateTimeWrapper.Now.Returns(toDate);

            LogService.GetPagingLogs(Arg.Any<LogParam>()).Returns(new PagingLogs<Log>());

            // Act 
            var actualLogViewModel = creditHandler.Handle(indexQuery);

            // Assert
            LogService.Received().GetPagingLogs(Arg.Is<LogParam>(logParam => logParam.FromDate == yesterday && logParam.ToDate == toDate));
        }

        [TestCase("2016-10-09", "2016-10-09")] // today-today
        [TestCase("2016-10-08", "2016-10-09")] // yesterday-today
        [TestCase("2016-10-01", "2016-10-09")] // beginOfMonth-today
        [TestCase("2016-10-31", "2016-10-09")] // endOfMonth-today
        [TestCase("2016-10-01", "2016-10-01")] // beginOfMonth-beginOfMonth
        [TestCase("2016-10-31", "2016-10-31")] // endOfMonth-endOfMonth
        public void HandleIndexQuery_HasFromToDate_GetsPagingLogsWithFromAnToDateRange(string fromDateString, string toDateString)
        {
            // Arrange
            var fromDate = Converter.ToDateTime(fromDateString);
            var toDate = Converter.ToDateTime(toDateString);

            var indexQuery = new IndexQuery(
                CurrentUserSession,
                DateTimeWrapper,
                true,
                ActionIdVisibilities,
                CustStatuses,
                ResourcesByBetTypeCacheRef,
                ResourcesByProductCacheRef,
                false);

            indexQuery.FromDate = fromDateString;
            indexQuery.ToDate = toDateString;

            LogService.GetPagingLogs(Arg.Any<LogParam>()).Returns(new PagingLogs<Log>());

            // Act 
            var actualLogViewModel = creditHandler.Handle(indexQuery);

            // Assert
            LogService.Received().GetPagingLogs(Arg.Is<LogParam>(logParam => logParam.FromDate == fromDate && logParam.ToDate == toDate));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("  ")]
        [TestCase("0")]
        public void HandleIndexQuery_NotHavePageSizeAndPageIndex_GetsPagingLogsWithDefaultPagingOptions(string pageSize)
        {
            // Arrange
            var indexQuery = new IndexQuery(
                CurrentUserSession,
                DateTimeWrapper,
                true,
                ActionIdVisibilities,
                CustStatuses,
                ResourcesByBetTypeCacheRef,
                ResourcesByProductCacheRef,
                false);

            indexQuery.PageSize = pageSize;

            LogService.GetPagingLogs(Arg.Any<LogParam>()).Returns(new PagingLogs<Log>());

            // Act 
            var actualLogViewModel = creditHandler.Handle(indexQuery);

            // Assert
            LogService.Received().GetPagingLogs(Arg.Is<LogParam>(logParam => logParam.PageSize == DefaultVariables.PageSize
                && logParam.PageIndex == 0));
        }

        [TestCase("50", 50)]
        [TestCase("1", 1)]
        [TestCase("-1", -1)]
        [TestCase("2147483647", int.MaxValue)]
        [TestCase("-2147483648", int.MinValue)]
        [TestCase(InvalidPageIndex, DefaultVariables.PageSize)]
        public void HandleIndexQuery_HasPageSize_GetsPagingLogsWithInputPageSize(string pageSize, int expectedPageSize)
        {
            // Arrange
            var indexQuery = new IndexQuery(
                CurrentUserSession,
                DateTimeWrapper,
                true,
                ActionIdVisibilities,
                CustStatuses,
                ResourcesByBetTypeCacheRef,
                ResourcesByProductCacheRef,
                false);

            indexQuery.PageSize = pageSize;

            LogService.GetPagingLogs(Arg.Any<LogParam>()).Returns(new PagingLogs<Log>());

            // Act 
            var actualLogViewModel = creditHandler.Handle(indexQuery);

            // Assert
            LogService.Received().GetPagingLogs(Arg.Is<LogParam>(logParam => logParam.PageSize == expectedPageSize));
        }

        [TestCase("0", 0)]
        [TestCase("1", 1)]
        [TestCase("-1", -1)]
        [TestCase("2147483647", int.MaxValue)]
        [TestCase("-2147483648", int.MinValue)]
        [TestCase(InvalidPageIndex, 0)]
        public void HandleIndexQuery_HasPageIndex_GetsPagingLogsWithInputPageIndex(string pageIndex, int expectedPageIndex)
        {
            // Arrange
            var indexQuery = new IndexQuery(
                CurrentUserSession,
                DateTimeWrapper,
                true,
                ActionIdVisibilities,
                CustStatuses,
                ResourcesByBetTypeCacheRef,
                ResourcesByProductCacheRef,
                false);

            indexQuery.PageIndex = pageIndex;

            LogService.GetPagingLogs(Arg.Any<LogParam>()).Returns(new PagingLogs<Log>());

            // Act 
            var actualLogViewModel = creditHandler.Handle(indexQuery);

            // Assert
            LogService.Received().GetPagingLogs(Arg.Is<LogParam>(logParam => logParam.PageIndex == expectedPageIndex));
        }

        [TestCase("1/1/0001", "01/01/0001")] // MinDateTime-MinDateTime
        [TestCase("1/1/2014", "01/01/2014")] // LessThanMinSupportedDate-LessThanMinSupportedDate
        [TestCase("1/1/2015", "01/01/2015")] // BeginOfSupportedDate-BeginOfSupportedDate
        [TestCase("10/1/2016", "10/01/2016")] // today-today
        [TestCase("10/1/2017", "10/01/2017")] // GreaterThanToday-GreaterThanToday
        public void HandleIndexQuery_HasLogsAndNotHideAnyLog_ReturnsLogViewModel(
            string fromDate,
            string toDate)
        {
            // Arrange
            var actionIdVisibilities = new Dictionary<int, bool>();
            actionIdVisibilities.Add(1, true);

            CurrentUserSession.RoleId.Returns(ValidRoleId);
            var indexQuery = new IndexQuery(
                CurrentUserSession,
                DateTimeWrapper,
                true,
                actionIdVisibilities,
                CustStatuses,
                ResourcesByBetTypeCacheRef,
                ResourcesByProductCacheRef,
                false);

            indexQuery.PageIndex = string.Empty;
            indexQuery.PageSize = DefaultVariables.PageSize.ToString();
            indexQuery.FromDate = fromDate;
            indexQuery.ToDate = toDate;

            var pagingLogs = new PagingLogs<Log>
            {
                TotalRecords = 2,
                Logs = new List<Log>
                {
                    new Log() { ActionId = 1, LogId = 1 },
                    new Log() { ActionId = 2, LogId = 2 }
                }
            };

            var logParam = GetLogParam(indexQuery);

            LogService.GetPagingLogs(Arg.Any<LogParam>()).Returns(pagingLogs);

            var expectedLogViewModel = new LogViewModel(logParam, pagingLogs.TotalRecords, MicroSites);
            var expectedLogViewModelForTesting = LogViewModelForTesting.FromLogViewModel(expectedLogViewModel, logParam, pagingLogs.TotalRecords, MicroSites);

            var validLogItemViewModels = ValidLogItemViewModels();
            expectedLogViewModelForTesting.LogItems = new List<LogItemViewModel>() { validLogItemViewModels[0], validLogItemViewModels[1] };

            // Act 
            var actualLogViewModel = creditHandler.Handle(indexQuery);

            // Assert
            Assert.AreEqual(
                expectedLogViewModelForTesting,
                LogViewModelForTesting.FromLogViewModel(actualLogViewModel, logParam, pagingLogs.TotalRecords, MicroSites));
        }

        [TestCase("1/1/0001", "01/01/0001")] // MinDateTime-MinDateTime
        [TestCase("1/1/2014", "01/01/2014")] // LessThanMinSupportedDate-LessThanMinSupportedDate
        [TestCase("1/1/2015", "01/01/2015")] // BeginOfSupportedDate-BeginOfSupportedDate
        [TestCase("10/1/2016", "10/01/2016")] // today-today
        [TestCase("10/1/2017", "10/01/2017")] // GreaterThanToday-GreaterThanToday
        public void HandleIndexQuery_HasLogsAndHideOneLog_ReturnsLogViewModel(
            string fromDate,
            string toDate)
        {
            // Arrange
            var actionIdVisibilities = new Dictionary<int, bool>();
            actionIdVisibilities.Add(1, false);

            CurrentUserSession.RoleId.Returns(ValidRoleId);
            var indexQuery = new IndexQuery(
                CurrentUserSession,
                DateTimeWrapper,
                true,
                actionIdVisibilities,
                CustStatuses,
                ResourcesByBetTypeCacheRef,
                ResourcesByProductCacheRef,
                false);

            indexQuery.PageIndex = string.Empty;
            indexQuery.PageSize = DefaultVariables.PageSize.ToString();
            indexQuery.FromDate = fromDate;
            indexQuery.ToDate = toDate;

            var pagingLogs = new PagingLogs<Log>
            {
                TotalRecords = 2,
                Logs = new List<Log>
                {
                    new Log() { ActionId = 1, LogId = 1 },
                    new Log() { ActionId = 2, LogId = 2 }
                }
            };

            var logParam = GetLogParam(indexQuery);

            LogService.GetPagingLogs(Arg.Any<LogParam>()).Returns(pagingLogs);

            var expectedLogViewModel = new LogViewModel(logParam, pagingLogs.TotalRecords, MicroSites);
            var expectedLogViewModelForTesting = LogViewModelForTesting.FromLogViewModel(expectedLogViewModel, logParam, pagingLogs.TotalRecords, MicroSites);
            expectedLogViewModelForTesting.TotalRecords = 1;
            var validLogItemViewModels = ValidLogItemViewModels();
            expectedLogViewModelForTesting.LogItems = new List<LogItemViewModel>() { validLogItemViewModels[1] };

            // Act 
            var actualLogViewModel = creditHandler.Handle(indexQuery);

            // Assert
            Assert.AreEqual(
                expectedLogViewModelForTesting,
                LogViewModelForTesting.FromLogViewModel(actualLogViewModel, logParam, pagingLogs.TotalRecords, MicroSites));
        }

        private List<LogItemViewModel> ValidLogItemViewModels()
        {
            return new List<LogItemViewModel>()
            {
                new LogItemViewModel
                {
                    LogId = 1,
                    LogDate = "01/01/0001 12:00:00 AM",
                    ActionId = 1,
                    ActionName = ValidLogActionName,
                    TrackingValue = "0",
                    TrackingValueOld = "0",
                    IP = ValidIP
                },
                new LogItemViewModel
                {
                    LogId = 2,
                    LogDate = "01/01/0001 12:00:00 AM",
                    ActionId = 2,
                    ActionName = ValidLogActionName,
                    TrackingValue = "0",
                    TrackingValueOld = "0",
                    IP = ValidIP
                }
            };
        }

        private LogParam GetLogParam(IndexQuery indexQuery)
        {
            return new LogParam
            {
                RoleId = 4,
                MinReportDate = indexQuery.MinReportDate,
                MaxReportDate = indexQuery.MaxReportDate,
                IsShowOldValue = true,
                PageSize = 50,
                FromDate = Converter.ToDateTime(indexQuery.FromDate),
                ToDate = Converter.ToDateTime(indexQuery.ToDate)
            };
        }
    }
}
