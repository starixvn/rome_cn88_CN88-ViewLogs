namespace Alpha.ViewLogs.Tests.Extensions
{
    using NUnit.Framework;
    using ViewLogs.Extensions;

    [TestFixture]
    public class DecimalExtensionTests
    {
        [TestCase(0.0, "0")]
        [TestCase(0.1, "0.1")]
        [TestCase(0.10, "0.1")]
        [TestCase(0.11, "0.11")]
        [TestCase(0.110, "0.11")]
        [TestCase(0.111, "0.11")]
        public void ToNumericString_NumberOnlyHasFractionalPart_Return(decimal value, string expectedValue)
        {
            // Arrange

            // Act 
            var actualValue = value.ToNumericString();

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}
