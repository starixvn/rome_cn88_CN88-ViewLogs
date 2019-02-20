namespace Alpha.ViewLogs.Tests.DataAccesses
{
    using System.Collections.Generic;
    using Fanex.Data.Repository;
    using NSubstitute;
    using NUnit.Framework;
    using ViewLogs.DataAccesses.Impl;
    using ViewLogs.Models;

    [TestFixture]
    public class LogDataAccessImplTests
    {
        private IDynamicRepository dynamicRepository;
        private LogDataAccessImpl logDataAccess;

        [SetUp]
        public void SetUp()
        {
            dynamicRepository = Substitute.For<IDynamicRepository>();

            logDataAccess = new LogDataAccessImpl(dynamicRepository);
        }

        [Test]
        public void GetTotalRecordsOfLogs_AnyCriteria_CallsDynamicRepository()
        {
            // Arrange
            ICriteria criteria = null;

            // Act
            var actualTotalRecords = logDataAccess.GetTotalRecordsOfLogs(criteria);

            // Assert
            dynamicRepository.Received().Count(criteria);
        }

        [Test]
        public void GetTotalRecordsOfLogs_AnyCriteria_ReturnsDataFromDynamicRepository()
        {
            // Arrange
            var criteria = Arg.Any<ICriteria>();
            var expectedTotalRecords = 1;
            dynamicRepository.Count(criteria).Returns(expectedTotalRecords);

            // Act
            var actualTotalRecords = logDataAccess.GetTotalRecordsOfLogs(criteria);

            // Assert
            Assert.AreEqual(expectedTotalRecords, actualTotalRecords);
        }

        [Test]
        public void GetLogs_AnyCriteria_CallsDynamicRepository()
        {
            // Arrange
            ICriteria criteria = null;

            // Act
            var actualLogs = logDataAccess.GetLogs(criteria);

            // Assert
            dynamicRepository.Received().Fetch<Log>(criteria);
        }

        [Test]
        public void GetLogs_AnyCriteria_ReturnsDataFromDynamicRepository()
        {
            // Arrange
            var criteria = Arg.Any<ICriteria>();
            var expectedLogs = new List<Log>()
            {
                new Log { LogId = 1 },
                new Log { LogId = 2 }
            };

            dynamicRepository.Fetch<Log>(criteria).Returns(expectedLogs);

            // Act
            var actualLogs = logDataAccess.GetLogs(criteria);

            // Assert
            Assert.AreEqual(expectedLogs, actualLogs);
        }
    }
}
