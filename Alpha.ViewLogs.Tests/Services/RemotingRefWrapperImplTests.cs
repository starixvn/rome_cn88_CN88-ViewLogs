namespace Alpha.ViewLogs.Tests.Services
{
    using System.Globalization;
    using System.Threading;
    using Constants;
    using Fanex.RemotingRef;
    using NSubstitute;
    using NUnit.Framework;
    using ViewLogs.Services.Impl;

    [TestFixture]
    public class RemotingRefWrapperImplTests
    {
        private const string DefaultLanguage = "en-US";

        private IRemotingRefService remotingRefService;
        private RemotingRefWrapperImpl remotingRefWrapper;

        [SetUp]
        public void SetUp()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(LanguageCode.English);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            remotingRefService = Substitute.For<IRemotingRefService>();
            remotingRefWrapper = new RemotingRefWrapperImpl(remotingRefService);
        }

        [Test]
        public void GetBetType_ValidBetTypeId_ReturnsCorrectBetTypeName()
        {
            // Arrange
            var expectedResult = "VIP Baccarat";

            remotingRefService
                .GetBetTypeName("2402", "en-US", string.Empty, string.Empty)
                .Returns(expectedResult);

            // Act
            var actualResult = remotingRefWrapper.GetBetType(2402, null);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void GetBetType_NullBetTypeId_ReturnsCorrectBetTypeName()
        {
            // Arrange
            var expectedResult = string.Empty;

            // Act
            var actualResult = remotingRefWrapper.GetBetType(null, null);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void GetBetType_ValidBetId_ReturnsCorrectBetTypeName()
        {
            // Arrange
            var expectedResult = "VIP Baccarat";

            remotingRefService
                .GetBetTypeName("2402", "en-US", string.Empty, "12")
                .Returns(expectedResult);

            // Act
            var actualResult = remotingRefWrapper.GetBetType(2402, "112");

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void GetBetTypeName_ValidBetTypeId_ReturnsCorrectBetTypeName()
        {
            // Arrange
            var expectedResult = "VIP Baccarat";

            remotingRefService
                .GetBetTypeName("2402", "en-US")
                .Returns(expectedResult);

            // Act
            var actualResult = remotingRefWrapper.GetBetTypeName(2402);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void GetBetTypeName_NullBetTypeId_ReturnsEmptyString()
        {
            // Arrange
            var expectedResult = string.Empty;

            // Act
            var actualResult = remotingRefWrapper.GetBetTypeName(null);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void GetSportName_ValidSportTypeId_ReturnsCorrectSportName()
        {
            // Arrange
            var expectedResult = "Virtual Sports";

            remotingRefService
                .GetSportName("5", DefaultLanguage, string.Empty)
                .Returns(expectedResult);

            // Act
            var actualResult = remotingRefWrapper.GetSportName(5);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCase(int.MinValue)]
        [TestCase(-1)]
        [TestCase(0)]
        public void GetSportName_InvalidSportTypeId_ReturnsEmptyString(object sportId)
        {
            // Arrange
            var expectedResult = string.Empty;

            remotingRefService
                .GetSportName(sportId.ToString(), DefaultLanguage, string.Empty)
                .Returns(expectedResult);

            // Act
            var actualResult = remotingRefWrapper.GetSportName(sportId);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void GetSportName_NullSportTypeId_ReturnsEmptyString()
        {
            // Arrange
            var expectedResult = string.Empty;

            // Act
            var actualResult = remotingRefWrapper.GetSportName(null);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void GetProductName_ValidProductId_ReturnsCorrectProductName()
        {
            // Arrange
            var expectedResult = "Bullfighting";

            remotingRefService
                .GetResourceDataByRefIdAndResourceId("Product", "30", DefaultLanguage, string.Empty)
                .Returns(expectedResult);

            // Act
            var actualResult = remotingRefWrapper.GetProductName(30);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCase(int.MinValue)]
        [TestCase(-1)]
        [TestCase(0)]
        public void GetProductName_InvalidProductId_ReturnsEmptyString(int productId)
        {
            // Arrange
            var expectedResult = string.Empty;

            remotingRefService
                .GetResourceDataByRefIdAndResourceId("Product", productId.ToString(), DefaultLanguage, string.Empty)
                .Returns(expectedResult);

            // Act
            var actualResult = remotingRefWrapper.GetProductName(productId);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
