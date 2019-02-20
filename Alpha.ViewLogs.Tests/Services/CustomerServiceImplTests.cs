namespace Alpha.ViewLogs.Tests.Services
{
    using Fanex.Data.Repository;
    using ModelsForTesting;
    using NSubstitute;
    using NUnit.Framework;
    using ViewLogs.Criteria;
    using ViewLogs.Models;
    using ViewLogs.Services.Impl;

    [TestFixture]
    public class CustomerServiceImplTests
    {
        private const string ValidUsername = "ValidUsername";
        private const int ValidCustId = 1;

        private IDynamicRepository dynamicRepository;
        private CustomerServiceImpl customerService;

        [SetUp]
        public void SetUp()
        {
            dynamicRepository = Substitute.For<IDynamicRepository>();

            customerService = new CustomerServiceImpl(dynamicRepository);
        }

        [TestCase("")]
        [TestCase(null)]
        public void GetDownlineInfoByUsername_NullOrEmptyUsername_ReturnsDefaultCustomerInfo(string username)
        {
            // Arrange 
            var expectedCustomerInfo = new CustomerInfo(0, false);

            // Act 
            var actualCustomerInfo = customerService.GetDownlineInfoByUsername(username, 0);

            // Assert
            Assert.AreEqual(
                CustomerInfoForTesting.FromCustomerInfo(expectedCustomerInfo),
                CustomerInfoForTesting.FromCustomerInfo(actualCustomerInfo));
        }

        [Test]
        public void GetDownlineInfoByUsername_ValidUsername_ReturnCustomerInfoFromDataAccess()
        {
            // Arrange
            var criteria = new GetCustInfoByUsernameCommand
            {
                Username = ValidUsername,
                UplineId = 0
            };

            var expectedCustomerInfo = new CustomerInfo(ValidCustId, true);

            dynamicRepository.When(d => d.Execute(Arg.Is<GetCustInfoByUsernameCommand>(x => x.Username == criteria.Username && x.CustId == criteria.CustId)))
                .Do(c =>
                {
                    var custInfo = c[0] as GetCustInfoByUsernameCommand;
                    custInfo.CustId = ValidCustId;
                    custInfo.IsSubAcc = true;
                });

            // Act 
            var actualCustomerInfo = customerService.GetDownlineInfoByUsername(ValidUsername, 0);

            // Assert
            Assert.AreEqual(
                CustomerInfoForTesting.FromCustomerInfo(expectedCustomerInfo),
                CustomerInfoForTesting.FromCustomerInfo(actualCustomerInfo));
        }
    }
}
