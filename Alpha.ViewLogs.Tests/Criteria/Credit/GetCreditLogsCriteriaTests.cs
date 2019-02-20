namespace Alpha.ViewLogs.Tests.Criteria.Credit
{
    using System;
    using NUnit.Framework;
    using ViewLogs.Criteria.Credit;

    [TestFixture]
    public class GetCreditLogsCriteriaTests
    {
        [Test]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var getCreditLogsCriteria = new GetCreditLogsCriteria(
                1,
                "CustName",
                DateTime.Now,
                DateTime.Now,
                true,
                1,
                1);
            var expectedSettingKey = "Acc_UserLog_CreditSel";

            // Act
            var actualSettingKey = getCreditLogsCriteria.GetSettingKey();

            // Assert
            Assert.AreEqual(expectedSettingKey, actualSettingKey);
        }

        [Test]
        public void IsValid_InvalidCustId_ReturnsFalse()
        {
            // Arrange
            var getCreditLogsCriteria = new GetCreditLogsCriteria(
                0,
                string.Empty,
                DateTime.Now,
                DateTime.Now,
                false,
                0,
                0);

            // Act
            var actualIsValid = getCreditLogsCriteria.IsValid();

            // Assert
            Assert.IsFalse(actualIsValid);
        }

        [Test]
        public void IsValid_ToDateLessThanFromDate_ReturnsFalse()
        {
            // Arrange
            var getCreditLogsCriteria = new GetCreditLogsCriteria(
                1,
                string.Empty,
                DateTime.Now.AddDays(1),
                DateTime.Now,
                false,
                0,
                0);

            // Act
            var actualIsValid = getCreditLogsCriteria.IsValid();

            // Assert
            Assert.IsFalse(actualIsValid);
        }

        [Test]
        public void IsValid_ValidCustIdAndToDateNotLessThanFromDate_ReturnsFalse()
        {
            // Arrange
            var getCreditLogsCriteria = new GetCreditLogsCriteria(
                1,
                "CustName",
                DateTime.Now,
                DateTime.Now,                
                true,
                1,
                1);

            // Act
            var actualIsValid = getCreditLogsCriteria.IsValid();

            // Assert
            Assert.IsTrue(actualIsValid);
        }
    }
}
