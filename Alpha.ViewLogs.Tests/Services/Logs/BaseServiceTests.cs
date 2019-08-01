namespace Alpha.ViewLogs.Tests.Services.Logs
{
    using System.Collections.Generic;
    using Constants;
    using Fanex.Caching;
    using Fanex.Data.Repository;
    using ModelsForTesting;
    using NSubstitute;
    using NUnit.Framework;
    using Params;
    using ViewLogs.Criteria;
    using ViewLogs.DataAccesses;
    using ViewLogs.Models;
    using ViewLogs.Services;
    using Sunplus.Agent.Model.Entities;

    [TestFixture]
    public class BaseServiceTests
    {
        private const string LogActionCacheKey = "Alpha.ViewLogs.Setting.LogAction";
        private const string LogActionTypesCacheKey = "Alpha.ViewLogs.Setting.LogActionTypes";

        private BaseServiceForTesting baseService;

        private IDynamicRepository dynamicRepository;
        private CustomerService customerService;
        private LogDataAccess logDataAccess;
        private IUserSession session;

        private Cache cacheForTesting;

        [SetUp]
        public void SetUp()
        {
            cacheForTesting = Substitute.For<Cache>();
            ConfiguredCacheService.SwitchCacheStorage(cacheForTesting);

            dynamicRepository = Substitute.For<IDynamicRepository>();
            customerService = Substitute.For<CustomerService>();
            logDataAccess = Substitute.For<LogDataAccess>();
            session = Substitute.For<IUserSession>();

            baseService = new BaseServiceForTesting(dynamicRepository, customerService, logDataAccess, session);
        }

        [Test]
        public void GetPagingLogs_NullLogParam_ReturnsEmptyPagingLogs()
        {
            // Arrange
            LogParam nullLogParam = null;
            var emptyPagingLogs = new PagingLogs<Log>();

            // Act 
            var actualPagingLogs = baseService.GetPagingLogs(nullLogParam);

            // Assert
            Assert.AreEqual(
                PagingLogsForTesting<Log>.FromPagingLogs(emptyPagingLogs),
                PagingLogsForTesting<Log>.FromPagingLogs(actualPagingLogs));
        }

        [Test]
        public void GetPagingLogs_NotNullLogParam_CallGetDownlineInfoByUsername()
        {
            // Arrange
            LogParam logParam = new LogParam();
            var downlineInfo = new CustomerInfo(0);
            customerService.GetDownlineInfoByUsername(logParam.Username, logParam.UplineId, 0).Returns(downlineInfo);

            // Act 
            baseService.GetPagingLogs(logParam);

            // Assert
            customerService.Received().GetDownlineInfoByUsername(logParam.Username, logParam.UplineId, 0);
        }

        [Test]
        public void GetPagingLogs_InvalidDownline_ReturnsEmptyPagingLogs()
        {
            // Arrange
            LogParam logParam = new LogParam();

            var invalidDownlineInfo = new CustomerInfo(0);

            customerService.GetDownlineInfoByUsername(logParam.Username, logParam.UplineId, 0).Returns(invalidDownlineInfo);

            var emptyPagingLogs = new PagingLogs<Log>();

            // Act 
            var actualPagingLogs = baseService.GetPagingLogs(logParam);

            // Assert
            Assert.AreEqual(
                PagingLogsForTesting<Log>.FromPagingLogs(emptyPagingLogs),
                PagingLogsForTesting<Log>.FromPagingLogs(actualPagingLogs));
        }

        [Test]
        public void GetPagingLogs_ValidDownline_ReturnsDataFromDataAccess()
        {
            // Arrange
            LogParam logParam = new LogParam
            {
                Language = LanguageCode.English
            };

            var validDownlineInfo = new CustomerInfo(1);

            customerService.GetDownlineInfoByUsername(logParam.Username, logParam.UplineId, 0).Returns(validDownlineInfo);

            var logs = new List<Log>
            {
                new Log { ActionId = 1 }
            };

            dynamicRepository.Fetch<Log>(Arg.Any<ICriteria>()).Returns(logs);

            var expectedPagingLogs = new PagingLogs<Log> { Logs = logs };

            // Act 
            var actualPagingLogs = baseService.GetPagingLogs(logParam);

            // Assert
            Assert.AreEqual(
                PagingLogsForTesting<Log>.FromPagingLogs(expectedPagingLogs),
                PagingLogsForTesting<Log>.FromPagingLogs(actualPagingLogs));
        }

        [TestCase(1)]
        [TestCase(2)]
        public void GetLogAction_HasDataInCache_ReturnsDataInCache(int actionId)
        {
            // Arrange
            var expectedActionDictionary = new Dictionary<int, LogAction>
            {
                { 1, new LogAction() { Id = 1, EnglishName = "Action 1" } },
                { 2, new LogAction() { Id = 2, EnglishName = "Action 2" } }
            };

            var expectedAction = expectedActionDictionary[actionId];

            Dictionary<int, LogAction> logActionInCache;
            cacheForTesting.TryGet(LogActionCacheKey, out logActionInCache).Returns(x =>
            {
                x[1] = expectedActionDictionary;

                return true;
            });

            // Act 
            var actualLogAction = baseService.GetLogAction(actionId);

            // Assert
            Assert.AreEqual(expectedAction, actualLogAction);
        }

        [TestCase(-1, null)]
        [TestCase(3, null)]
        [TestCase(int.MaxValue, null)]
        [TestCase(int.MinValue, null)]
        public void GetLogAction_NotMatchDataInCache_ReturnsDefaultAction(int actionId, string expectedActionName)
        {
            // Arrange
            var expectedActionDictionary = new Dictionary<int, LogAction>
            {
                { 1, new LogAction() { Id = 1, EnglishName = "Action 1" } },
                { 2, new LogAction() { Id = 2, EnglishName = "Action 2" } }
            };

            var expectedAction = new LogAction() { Id = actionId, EnglishName = expectedActionName };

            Dictionary<int, LogAction> logActionInCache;
            cacheForTesting.TryGet(LogActionCacheKey, out logActionInCache).Returns(x =>
            {
                x[1] = expectedActionDictionary;

                return true;
            });

            // Act 
            var actualLogAction = baseService.GetLogAction(actionId);

            // Assert
            Assert.IsTrue(actualLogAction.Id == expectedAction.Id && actualLogAction.EnglishName == expectedAction.EnglishName);
        }

        [TestCase(-1, null)]
        [TestCase(3, null)]
        [TestCase(int.MaxValue, null)]
        [TestCase(int.MinValue, null)]
        public void GetLogAction_NoDataInCache_ReturnsDefaultAction(int actionId, string expectedActionName)
        {
            // Arrange
            var expectedAction = new LogAction()
            {
                Id = actionId,
                EnglishName = expectedActionName
            };

            Dictionary<int, LogAction> logActionInCache;
            cacheForTesting.TryGet(LogActionCacheKey, out logActionInCache).Returns(false);

            // Act 
            var actualLogAction = baseService.GetLogAction(actionId);

            // Assert
            Assert.IsTrue(actualLogAction.Id == expectedAction.Id && actualLogAction.EnglishName == expectedAction.EnglishName);
        }

        [TestCase(-1, null, LanguageCode.ChineseTraditional)]
        [TestCase(-1, null, LanguageCode.ChineseSimplified)]
        [TestCase(-1, null, LanguageCode.NewChineseSimplified)]
        [TestCase(-1, null, LanguageCode.Japanese)]
        [TestCase(-1, null, LanguageCode.Thai)]
        [TestCase(-1, null, LanguageCode.Korean)]
        [TestCase(-1, null, LanguageCode.Vietnamese)]
        [TestCase(-1, null, LanguageCode.English)]
        [TestCase(0, null, LanguageCode.English)]
        [TestCase(int.MaxValue, null, LanguageCode.English)]
        [TestCase(int.MinValue, null, LanguageCode.English)]
        [TestCase(1, "Action Name 1 - ChineseTraditional", LanguageCode.ChineseTraditional)]
        [TestCase(1, "Action Name 1 - ChineseSimplified", LanguageCode.ChineseSimplified)]
        [TestCase(1, "Action Name 1 - ChineseSimplified", LanguageCode.NewChineseSimplified)]
        [TestCase(1, "Action Name 1 - Japanese", LanguageCode.Japanese)]
        [TestCase(1, "Action Name 1 - Thai", LanguageCode.Thai)]
        [TestCase(1, "Action Name 1 - Korean", LanguageCode.Korean)]
        [TestCase(1, "Action Name 1 - Vietnamese", LanguageCode.Vietnamese)]
        [TestCase(1, "Action Name 1 - English", LanguageCode.English)]
        public void GetLogActionName_AnyActionId_ReturnsCorrectName(
            int actionId,
            string expectedActionName,
            string language)
        {
            // Arrange
            var logAction = new LogAction()
            {
                Id = 1,
                ChineseTraditionalName = "Action Name 1 - ChineseTraditional",
                ChineseSimplifiedName = "Action Name 1 - ChineseSimplified",
                NewChineseSimplifiedName = "Action Name 1 - ChineseSimplified",
                JapaneseName = "Action Name 1 - Japanese",
                ThaiName = "Action Name 1 - Thai",
                KoreanName = "Action Name 1 - Korean",
                VietnameseName = "Action Name 1 - Vietnamese",
                EnglishName = "Action Name 1 - English",

                ActionTypeId = 0,
                IsInternal = false,
                SPName = string.Empty
            };

            var expectedActionDictionary = new Dictionary<int, LogAction>
            {
                { 1, logAction }
            };

            Dictionary<int, LogAction> logActionInCache;
            cacheForTesting.TryGet(LogActionCacheKey, out logActionInCache).Returns(x =>
            {
                x[1] = expectedActionDictionary;

                return true;
            });

            // Act 
            var actualLogActionName = baseService.GetLogActionName(actionId, language);

            // Assert
            Assert.AreEqual(expectedActionName, actualLogActionName);
        }
    }
}
