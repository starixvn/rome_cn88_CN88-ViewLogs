namespace Alpha.ViewLogs.Tests.Services.Logs
{
    using System.Collections.Generic;
    using Fanex.Data.Repository;
    using NSubstitute;
    using NUnit.Framework;
    using ViewLogs.Models;
    using ViewLogs.Services.Impl.Logs;

    [TestFixture]
    public class LoginServiceImplTests : ConcreteLogServiceImplTests
    {
        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            LogService = new LoginServiceImpl(DynamicRepository, CustomerService, BaseDataAccess, Session);
        }

        [Test]
        public override void GetPagingLogs_ValidDownline_ReturnsDataFromDataAccess()
        {
            base.GetPagingLogs_ValidDownline_ReturnsDataFromDataAccess();
        }

        [Test]
        public override void CreateGetLogsCriteria_NullLogParam_ThrowsArgumentNullException()
        {
            base.CreateGetLogsCriteria_NullLogParam_ThrowsArgumentNullException();
        }

        [Test]
        public override void CreateGetTotalLogsCriteria_NullLogParam_ThrowsArgumentNullException()
        {
            base.CreateGetTotalLogsCriteria_NullLogParam_ThrowsArgumentNullException();
        }

        protected override void SubstituteGetLogsFromDataAccess(IEnumerable<Log> logs)
        {
            BaseDataAccess.GetLogs(Arg.Any<ICriteria>()).Returns(logs);
        }
    }
}
