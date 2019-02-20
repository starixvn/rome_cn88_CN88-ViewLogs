namespace Alpha.ViewLogs.Tests.Services
{
    using NSubstitute;
    using NUnit.Framework;
    using Sunplus.Agent.Model.Entities;
    using Utilities.IP;
    using ViewLogs.Services.Impl;

    [TestFixture]
    public class IPServiceImplTests
    {
        private const string ValidCountry = "ValidCountry";

        private IUserSession userSession;
        private ViewLogIPServiceImpl viewLogIPService;
        private IPService ipService;
        
        [SetUp]
        public void SetUp()
        {
            ipService = Substitute.For<IPService>();            
            userSession = Substitute.For<IUserSession>();
            viewLogIPService = new ViewLogIPServiceImpl(userSession, ipService);            
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("  ")]
        [TestCase("#")]
        [TestCase("1.1.1.1.1")]
        public void FormatIP_InvalidIP_ReturnsInputIP(string ip)
        {
            // Arrange
            var expectedIP = ip;

            // Act 
            var actualIP = viewLogIPService.FormatIP(ip);

            // Assert
            Assert.AreEqual(expectedIP, actualIP);
        }

        [TestCase("1")]
        [TestCase("1.1")]
        [TestCase("1.1.1")]
        [TestCase("::1")]
        public void FormatIP_ValidIPButNotMatchRegex_ReturnsIPLinkWithoutOpenIPInfo(string ip)
        {
            // Arrange
            var expectedIP = string.Format("<a class=\"iplink\" href=\"javascript:;\">{0}</a>", ip);

            // Act 
            var actualIP = viewLogIPService.FormatIP(ip);

            // Assert
            Assert.AreEqual(expectedIP, actualIP);
        }

        [TestCase("1.1.1.1", true, "<a class=\"iplink\" href=\"javascript:OpenIPInfo('1.1.1.1');\">1.1.1.1</a>")]
        [TestCase("1.1.1.1", false, "")]
        public void FormatIP_IsVNIP_ReturnsCorrectIP(string ip, bool canViewVNIP, string expectedIP)
        {
            // Arrange
            userSession.IsAdminCanViewVNIP.Returns(canViewVNIP);
            ipService.IsVNIP(Arg.Any<string>()).Returns(true);

            // Act 
            var actualIP = viewLogIPService.FormatIP(ip);

            // Assert
            Assert.AreEqual(expectedIP, actualIP);
        }

        [TestCase("1.1.1.1", true, "<a class=\"iplink\" href=\"javascript:OpenIPInfo('1.1.1.1');\">1.1.1.1</a>")]
        [TestCase("1.1.1.1", false, "<a class=\"iplink\" href=\"javascript:OpenIPInfo('1.1.1.1');\">1.1.1.1</a>")]
        public void FormatIP_MatchIPValidationAndNotVNIP_ReturnsCorrectIP(string ip, bool canViewVNIP, string expectedIP)
        {  
            // Arrange
            userSession.IsAdminCanViewVNIP.Returns(canViewVNIP);
            ipService.IsVNIP(Arg.Any<string>()).Returns(false);

            // Act 
            var actualIP = viewLogIPService.FormatIP(ip);

            // Assert
            Assert.AreEqual(expectedIP, actualIP);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("  ")]
        [TestCase("#")]
        [TestCase("1.1.1.1.1")]
        public void FormatCountryCode_InvalidIP_ReturnsEmpty(string ip)
        {
            // Arrange
            var country = ValidCountry;

            // Act 
            var actualCountry = viewLogIPService.FormatCountryCode(ip, country);

            // Assert
            Assert.IsEmpty(actualCountry);
        }

        [TestCase("1.1.1.0")]
        [TestCase("1.1.1.1")]
        [TestCase("1.1.1.255")]
        public void FormatCountryCode_VNIPAndUserCannotViewVNIP_ReturnsEmpty(string ip)
        {
            // Arrange
            var country = ValidCountry;
            ipService.IsVNIP(Arg.Any<string>()).Returns(true);
            userSession.IsAdminCanViewVNIP.Returns(false);

            // Act 
            var actualCountry = viewLogIPService.FormatCountryCode(ip, country);

            // Assert
            Assert.IsEmpty(actualCountry);
        }

        [TestCase("1.1.1.0", ValidCountry)]
        [TestCase("1.1.1.1", ValidCountry)]
        [TestCase("1.1.1.255", ValidCountry)]
        public void FormatCountryCode_VNIPAndUserCanViewVNIP_ReturnsCountry(string ip, string country)
        {
            // Arrange
            var expectedCountry = country;
            ipService.IsVNIP(Arg.Any<string>()).Returns(true);

            userSession.IsAdminCanViewVNIP.Returns(true);

            // Act 
            var actualCountry = viewLogIPService.FormatCountryCode(ip, country);

            // Assert
            Assert.AreEqual(expectedCountry, actualCountry);
        }

        [TestCase("2.1.1.0", true, ValidCountry)]
        [TestCase("2.1.1.1", true, ValidCountry)]
        [TestCase("2.1.1.255", true, ValidCountry)]
        [TestCase("2.1.1.0", false, ValidCountry)]
        [TestCase("2.1.1.1", false, ValidCountry)]
        [TestCase("2.1.1.255", false, ValidCountry)]
        public void FormatCountryCode_ValidIPButNotVNIP_ReturnsCountry(string ip, bool canViewVNIP, string country)
        {
            // Arrange
            var expectedCountry = country;
            ipService.IsVNIP(Arg.Any<string>()).Returns(false);

            userSession.IsAdminCanViewVNIP.Returns(canViewVNIP);

            // Act 
            var actualCountry = viewLogIPService.FormatCountryCode(ip, country);

            // Assert
            Assert.AreEqual(expectedCountry, actualCountry);
        }
    }
}
