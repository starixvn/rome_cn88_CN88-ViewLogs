namespace Alpha.ViewLogs.Site.Tests.Helpers
{
    using System.Diagnostics.CodeAnalysis;
    using System.Xml;
    using Models;
    using NSubstitute;
    using NUnit.Framework;
    using Site.Helpers;

    [TestFixture]
    public class TrackingValueFormatterTests
    {
        private TrackingValueFormatterImpl trackingValueFormatter;

        [SetUp]
        public void SetUp()
        {
            trackingValueFormatter = new TrackingValueFormatterImpl();
        }

        [Test]
        public void GetFormattedDiffTrackingValue_NullLog_ReturnsEmpty()
        {
            // Arrange
            Log nullLog = null;

            // Action
            var actualTrackingValue = trackingValueFormatter.GetFormattedDiffTrackingValue(nullLog, Arg.Any<bool>());

            // Assert
            Assert.IsEmpty(actualTrackingValue);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("  ")]
        public void GetFormattedDiffTrackingValue_LogWithNullOrWhiteSpaceTrackingValue_ReturnsEmpty(string trackingValue)
        {
            // Arrange
            var log = new Log
            {
                TrackingValue = trackingValue
            };

            // Action
            var actualTrackingValue = trackingValueFormatter.GetFormattedDiffTrackingValue(log, Arg.Any<bool>());

            // Assert
            Assert.IsEmpty(actualTrackingValue);
        }

        [TestCase("<></>")]
        [TestCase("<div>")]
        [TestCase("</div>")]
        [TestCase("<div></tag>")]
        [TestCase("<div>content</tag>")]
        public void GetFormattedDiffTrackingValue_LogWithWrongXMLFormattedTrackingValue_ThrowsXmlException(string trackingValue)
        {
            // Arrange
            var log = new Log
            {
                TrackingValue = trackingValue
            };

            // Action & Assert
            Assert.Throws<XmlException>(new TestDelegate(() => trackingValueFormatter.GetFormattedDiffTrackingValue(log, Arg.Any<bool>())));
        }

        [TestCase("<div>content</div>", "<div>content</div>")]
        [TestCase("<div ip=\"ValidIP\"></div>", "<div ip=\"ValidIP\"></div>")]
        [TestCase("<div loginip=\"ValidIP\"></div>", "<div loginip=\"ValidIP\"></div>")]
        [TestCase("<div stakelevel1=\"ValidStakelevel\"></div>", "<div stakelevel1=\"ValidStakelevel\"></div>")]
        [TestCase("<div formatAttribute=\"\"></div>", "<div formatAttribute=\"\"></div>")]
        [TestCase("<div formatAttribute=\" \"></div>", "<div formatAttribute=\" \"></div>")]
        [TestCase("<div formatAttribute=\"  \"></div>", "<div formatAttribute=\"  \"></div>")]
        [TestCase("<div><subnode>content</subnode></div>", "<div><subnode>content</subnode></div>")]
        [TestCase("<div permissionAttribute=\"[A][B][H]\"></div>", "<div permissionAttribute=\"[Member information: Full Control]\"></div>")] // PemissionAbbriations
        [TestCase("<div permissionAttribute=\"[A][B]\"></div>", "<div permissionAttribute=\"[Member information: Full Control]\"></div>")] // PemissionAbbriations
        [TestCase("<div permissionAttribute=\"[B][H]\"></div>", "<div permissionAttribute=\"[Member information: Update]\"></div>")] // PemissionAbbriations
        [TestCase("<div permissionAttribute=\"[A]\"></div>", "<div permissionAttribute=\"[Member information: Full Control]\"></div>")] // Single Permission
        [TestCase("<div permissionAttribute=\"[B]\"></div>", "<div permissionAttribute=\"[Member information: Update]\"></div>")] // Single Permission
        [TestCase("<div permissionAttribute=\"[C]\"></div>", "<div permissionAttribute=\"[Transfer]\"></div>")] // Single Permission
        [TestCase("<div permissionAttribute=\"[G]\"></div>", "<div permissionAttribute=\"[View Log]\"></div>")] // Single Permission
        [TestCase("<div permissionAttribute=\"[H]\"></div>", "<div permissionAttribute=\"[Member information: View]\"></div>")] // Single Permission
        [TestCase("<div permissionAttribute=\"[E]\"></div>", "<div permissionAttribute=\"[Reports]\"></div>")] // Single Permission
        [TestCase("<div permissionAttribute=\"[D]\"></div>", "<div permissionAttribute=\"[Bets &amp;amp; Forecast]\"></div>")] // Single Permission
        [TestCase(
            "<div permissionAttribute=\"[A][H][B]\"></div>",
            "<div permissionAttribute=\"[Member information: Full Control][Member information: View][Member information: Update]\"></div>")] // Multi Permission
        [TestCase(
            "<div permissionAttribute=\"[B][A]\"></div>",
            "<div permissionAttribute=\"[Member information: Update][Member information: Full Control]\"></div>")] // Multi Permission
        [TestCase(
            "<div permissionAttribute=\"[H][B]\"></div>",
            "<div permissionAttribute=\"[Member information: View][Member information: Update]\"></div>")] // Multi Permission
        [TestCase("<div permissionAttribute=\"[a]\"></div>", "<div permissionAttribute=\"[Member information: Full Control]\"></div>")] // Lower Case Permission
        [TestCase(
            "<div permissionAttribute=\"[a][A]\"></div>",
            "<div permissionAttribute=\"[Member information: Full Control]" +
                "[Member information: Full Control]\"></div>")] // Duplicate Permission
        public void GetFormattedDiffTrackingValue_LogWithXMLFormattedTrackingValueAndNotHaveTrackingValueOld_ReturnsExpectedTrackingValue(
            string trackingValue,
            string expectedTrackingValue)
        {
            // Arrange
            var log = new Log
            {
                TrackingValue = trackingValue
            };

            // Action
            var actualTrackingValue = trackingValueFormatter.GetFormattedDiffTrackingValue(log, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(expectedTrackingValue, actualTrackingValue);
        }

        [TestCase("<></>")]
        [TestCase("<div>")]
        [TestCase("</div>")]
        [TestCase("<div></tag>")]
        [TestCase("<div>content</tag>")]
        public void GetFormattedDiffTrackingValue_HasInvalidXMLFormattedTrackingValueOld_ThowsXMLException(string trackingValueOld)
        {
            // Arrange
            var validXMLFormattedTrackingValue = "<div>content</div>";
            var log = new Log
            {
                TrackingValue = validXMLFormattedTrackingValue,
                TrackingValueOld = trackingValueOld
            };

            // Action & Assert
            Assert.Throws<XmlException>(new TestDelegate(() => trackingValueFormatter.GetFormattedDiffTrackingValue(log, Arg.Any<bool>())));
        }

        [TestCase("<div>content</div>", "<div>content</div>", "<div>content</div>")] // not change value
        [TestCase("<div>content</div>", "<div>contentOld</div>", "<div>content</div>")] // change value
        [TestCase("<div value=\"newValue\">content</div>", "<div value2=\"oldValue\">contentOld</div>", "<div value=\"newValue\">content</div>")] // not change value
        [TestCase(
           "<div value=\"newValue\">content</div>",
           "<div value=\"oldValue\">contentOld</div>",
           "<div value=\"&lt;span class=&quot;valuechanged&quot; " +
                "title=&quot;oldValue --&gt; newValue&quot;&gt;newValue&lt;/span&gt;\">content</div>")] // change attribute value
        public void GetFormattedDiffTrackingValue_HasValidXMLFormattedTrackingValueNewOldAndNotShowOldValue_ThowsXMLException(
           string trackingValue,
           string trackingValueOld,
           string expectedTrackingValue)
        {
            // Arrange
            var log = new Log
            {
                TrackingValue = trackingValue,
                TrackingValueOld = trackingValueOld
            };

            var isShowOldValue = false;

            // Action
            var actualTrackingValue = trackingValueFormatter.GetFormattedDiffTrackingValue(log, isShowOldValue);

            // Assert
            Assert.AreEqual(expectedTrackingValue, actualTrackingValue);
        }

        [TestCase("<div>content</div>", "<div>content</div>", "<div>content</div>")] // not change value
        [TestCase("<div>content</div>", "<div>contentOld</div>", "<div>content</div>")] // change value
        [TestCase("<div value=\"newValue\">content</div>", "<div value2=\"oldValue\">contentOld</div>", "<div value=\"newValue\">content</div>")] // not change value
        [TestCase(
            "<div value=\"newValue\">content</div>",
            "<div value=\"oldValue\">contentOld</div>",
            "<div value=\"&lt;span class=&quot;valuechanged&quot;&gt;newValue&lt;/span&gt;" +
                "&lt;span class=&quot;valueold&quot;&gt;(oldValue)&lt;/span&gt;\">content</div>")] // change attribute value
        public void GetFormattedDiffTrackingValue_HasValidXMLFormattedTrackingValueNewOldAndShowOldValue_ThowsXMLException(
            string trackingValue,
            string trackingValueOld,
            string expectedTrackingValue)
        {
            // Arrange
            var log = new Log
            {
                TrackingValue = trackingValue,
                TrackingValueOld = trackingValueOld
            };

            var isShowOldValue = true;

            // Action
            var actualTrackingValue = trackingValueFormatter.GetFormattedDiffTrackingValue(log, isShowOldValue);

            // Assert
            Assert.AreEqual(expectedTrackingValue, actualTrackingValue);
        }
    }
}
