namespace Alpha.ViewLogs.Tests.Services.Logs
{
    using NUnit.Framework;
    using ViewLogs.Services.Impl.Logs;

    public class CreditServiceImplTests : ConcreteLogServiceImplTests
    {
        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            LogService = new CreditServiceImpl(DynamicRepository, CustomerService, BaseDataAccess, Session);
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
    }
}
