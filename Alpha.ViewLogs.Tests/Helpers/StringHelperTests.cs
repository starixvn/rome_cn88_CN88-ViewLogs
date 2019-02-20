namespace Alpha.ViewLogs.Tests.Helpers
{
    using NUnit.Framework;
    using ViewLogs.Helpers;

    [TestFixture]
    public class StringHelperTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")] // 1 whitespace
        [TestCase("  ")] // multi-whitespaces
        public void RemoveTrailingZeros_NullOrWhiteSpaceValue_ReturnsInputValue(string inputValue)
        {
            // Arrange
            var expectedValue = inputValue;

            // Act 
            var actualValue = StringHelper.RemoveTrailingZeros(inputValue);

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestCase("0a")] // leading with number and having non-numeric characters
        [TestCase("1a")] // leading with number and having non-numeric characters
        [TestCase("a")] // non-numeric character
        [TestCase("ab")] // non-numeric characters
        [TestCase("ab0")] // has non-numeric characters and numbers
        [TestCase("a0")] // has non-numeric characters and numbers
        [TestCase(".a")] // start with dot, remain is non-numeric characters
        [TestCase(".0")] // start with dot, remain is zeros
        [TestCase(".1")] // start with dot, remain is numbers
        [TestCase("01")] // leading zeros and having numbers
        [TestCase("010")] // leading/ending zeros and having numbers
        [TestCase("0100")] // leading zeros, ending with multi-zeros and having numbers
        [TestCase("00")] // only contains 2+ zeros
        public void RemoveTrailingZeros_NotANumberValue_ReturnsInputValue(string inputValue)
        {
            // Arrange
            var expectedValue = inputValue;

            // Act 
            var actualValue = StringHelper.RemoveTrailingZeros(inputValue);

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestCase("0", "0")] // only 1 zero
        [TestCase("0.1", "0.1")] // start with zeros and decimal part is numbers
        [TestCase("1.1", "1.1")] // not start with zeros and decimal part is numbers
        [TestCase("1.0", "1")] // not start with zeros and decimal part is zeros
        [TestCase("1.10", "1.1")] // not start with zeros and decimal part ends with zeros
        [TestCase("0.00", "0")] // start with zeros and decimal part ends with zeros
        [TestCase("0.", "0")] // start with zeros and not have decimal part
        [TestCase("1.00", "1")] // not start with zeros and decimal part is zeros
        [TestCase("1.1100", "1.11")] // not start with zeros and decimal part ends with zeros
        [TestCase("1.", "1")] // not start with zeros and not have decimal part
        [TestCase("10.0", "10")] // not start with zeros, real part ends with zero and decimal part is zero
        [TestCase("10.1", "10.1")] // not start with zeros, real part ends with zero and decimal part isn't zero
        [TestCase("10.10", "10.1")] // not start with zeros, real part ends with zero and decimal partends with zero
        [TestCase("10.100", "10.1")] // not start with zeros, real part ends with zero and decimal partends with zeros
        [TestCase("10", "10")] // not start with zeros, real part ends with zero and not have decimal part
        [TestCase("100", "100")] // not start with zeros, real part ends with zeros and not have decimal part
        public void RemoveTrailingZeros_NumberValue_ReturnsTrailingZerosValue(string inputValue, string expectedValue)
        {
            // Act 
            var actualValue = StringHelper.RemoveTrailingZeros(inputValue);

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}
