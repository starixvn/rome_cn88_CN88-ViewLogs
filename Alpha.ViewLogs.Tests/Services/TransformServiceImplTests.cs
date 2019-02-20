namespace Alpha.ViewLogs.Tests.Services
{
    using System.Globalization;
    using System.Threading;
    using System.Xml;
    using System.Xml.Xsl;
    using Fanex.Caching;
    using Helpers;
    using NSubstitute;
    using NUnit.Framework;
    using Params;
    using ViewLogs.Constants;
    using ViewLogs.Services;
    using ViewLogs.Services.Impl;

    [TestFixture]
    public class TransformServiceImplTests
    {
        private const int InvalidActionId = -1;
        private const int ValidActionId = 10;
        private const int NoCompiledTransform = int.MaxValue;

        private LogFormatter logFormatter;
        private TransformServiceImpl transformService;
        private Cache cacheForTesting;

        [SetUp]
        public void SetUp()
        {
            cacheForTesting = Substitute.For<Cache>();
            ConfiguredCacheService.SwitchCacheStorage(cacheForTesting);

            Thread.CurrentThread.CurrentCulture = new CultureInfo(LanguageCode.English);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            logFormatter = Substitute.For<LogFormatter>();
            transformService = new TransformServiceImpl(logFormatter);
        }

        [TestCase(InvalidActionId)]
        [TestCase(0)]
        [TestCase(1)]
        public void TransformTrackingValue_NullTransformParam_ReturnsEmpty(int logActionId)
        {
            // Arrange
            TransformParam transformParam = null;

            // Act 
            var actualTrackingValue = transformService.TransformTrackingValue(Arg.Any<string>(), logActionId, transformParam);

            // Assert
            Assert.IsEmpty(actualTrackingValue);
        }

        [TestCase(InvalidActionId)]
        [TestCase(0)]
        [TestCase(1)]
        public void TransformTrackingValue_NotNullTransformParamAndIsNullOrWhiteSpaceTrackingValue_ReturnsEmpty(int logActionId)
        {
            // Arrange
            TransformParam transformParam = new TransformParam();

            // Act 
            var actualTrackingValue = transformService.TransformTrackingValue(Arg.Any<string>(), logActionId, transformParam);

            // Assert
            Assert.IsEmpty(actualTrackingValue);
        }

        [Test]
        public void TransformTrackingValue_NotNullTransformParamAndInvalidActionId_ReturnsEmpty()
        {
            // Arrange
            TransformParam transformParam = new TransformParam();

            // Act 
            var actualTrackingValue = transformService.TransformTrackingValue(Arg.Any<string>(), InvalidActionId, transformParam);

            // Assert
            Assert.IsEmpty(actualTrackingValue);
        }

        [TestCase("a", null)]
        [TestCase("a", "")]
        [TestCase("a", " ")]
        [TestCase("a", "  ")]
        [TestCase("a", @"\")]
        [TestCase("a", @"\NotExistPath")]
        [TestCase("a", @"\TestData\TrackingValueTransformTemplates")]
        public void TransformTrackingValue_NoCompiledTransformInCacheAndFiles_ReturnsEmpty(
            string trackingValue,
            string trackingValueTransformTemplatePath)
        {
            // Arrange
            TransformParam transformParam = new TransformParam
            {
                TrackingValueTransformTemplatePath = FileHelper.GetFilePath(trackingValueTransformTemplatePath)
            };

            var actionId = NoCompiledTransform;
            XslCompiledTransform compiledTransformInCache = null;

            cacheForTesting.TryGet($"Alpha.ViewLogs.TransformObject_{actionId}", out compiledTransformInCache).Returns(false);

            // Act 
            var actualTrackingValue = transformService.TransformTrackingValue(trackingValue, actionId, transformParam);

            // Assert
            Assert.IsEmpty(actualTrackingValue);
        }

        [TestCase("a", @"\TestData\TrackingValueTransformTemplates", 1)]
        [TestCase("a", @"\TestData\TrackingValueTransformTemplates", 300)]
        [TestCase("<div>", @"\TestData\TrackingValueTransformTemplates", 1)]
        [TestCase("<div></span>", @"\TestData\TrackingValueTransformTemplates", 1)]
        public void TransformTrackingValue_HasTransformFilesAndInvalidXMLTrackingValue_ThrowsXMLException(
            string trackingValue,
            string trackingValueTransformTemplatePath,
            int actionId)
        {
            // Arrange
            TransformParam transformParam = new TransformParam
            {
                TrackingValueTransformTemplatePath = FileHelper.GetFilePath(trackingValueTransformTemplatePath)
            };

            XslCompiledTransform compiledTransformInCache = null;

            cacheForTesting.TryGet($"Alpha.ViewLogs.TransformObject_{actionId}", out compiledTransformInCache).Returns(false);

            // Act & Assert
            Assert.Throws<XmlException>(new TestDelegate(() => transformService.TransformTrackingValue(trackingValue, actionId, transformParam)));
        }

        [TestCase("<div></div>", @"\TestData\TrackingValueTransformTemplates", 1)]
        [TestCase("<div></div>", @"\TestData\TrackingValueTransformTemplates", 300)]
        [TestCase("<div />", @"\TestData\TrackingValueTransformTemplates", 1)]
        public void TransformTrackingValue_HasTransformFilesAndNotMatchTrackingValue_ReturnsEmpty(
            string trackingValue,
            string trackingValueTransformTemplatePath,
            int actionId)
        {
            // Arrange
            TransformParam transformParam = new TransformParam
            {
                TrackingValueTransformTemplatePath = FileHelper.GetFilePath(trackingValueTransformTemplatePath)
            };

            XslCompiledTransform compiledTransformInCache = null;

            cacheForTesting.TryGet($"Alpha.ViewLogs.TransformObject_{actionId}", out compiledTransformInCache).Returns(false);

            // Act 
            var actualTrackingValue = transformService.TransformTrackingValue(trackingValue, actionId, transformParam);

            // Assert
            Assert.IsEmpty(actualTrackingValue);
        }

        [TestCase(
            "<storeprocedure_name></storeprocedure_name>",
            @"\TestData\TrackingValueTransformTemplates",
            "<div xmlns:xslLogFormat=\"urn:LogFormat\"></div>")]
        [TestCase(
            "<storeprocedure_name />",
            @"\TestData\TrackingValueTransformTemplates",
            "<div xmlns:xslLogFormat=\"urn:LogFormat\"></div>")]
        [TestCase(
            "<storeprocedure_name attribute_value=\"0\"></storeprocedure_name>",
            @"\TestData\TrackingValueTransformTemplates",
            "<div xmlns:xslLogFormat=\"urn:LogFormat\">0</div>")]
        [TestCase(
            "<storeprocedure_name attribute_value=\"1\" />",
            @"\TestData\TrackingValueTransformTemplates",
            "<div xmlns:xslLogFormat=\"urn:LogFormat\">1</div>")]
        public void TransformTrackingValue_XMLTrackingValueMatchWithTemplateFile_ReturnsCorrectTrackingValue(
            string trackingValue,
            string trackingValueTransformTemplatePath,
            string expectedTrackingValue)
        {
            // Arrange
            var actionId = 1;

            TransformParam transformParam = new TransformParam
            {
                TrackingValueTransformTemplatePath = FileHelper.GetFilePath(trackingValueTransformTemplatePath)
            };

            XslCompiledTransform compiledTransformInCache = null;

            cacheForTesting.TryGet($"Alpha.ViewLogs.TransformObject_{actionId}", out compiledTransformInCache).Returns(false);

            // Act 
            var actualTrackingValue = transformService.TransformTrackingValue(trackingValue, actionId, transformParam);

            // Assert
            Assert.AreEqual(expectedTrackingValue, actualTrackingValue);
        }

        [TestCase("<storeprocedure_name></storeprocedure_name>", "<div xmlns:xslLogFormat=\"urn:LogFormat\"></div>")]
        [TestCase("<storeprocedure_name />", "<div xmlns:xslLogFormat=\"urn:LogFormat\"></div>")]
        [TestCase("<storeprocedure_name attribute_value=\"0\"></storeprocedure_name>", "<div xmlns:xslLogFormat=\"urn:LogFormat\">0</div>")]
        [TestCase("<storeprocedure_name attribute_value=\"1\" />", "<div xmlns:xslLogFormat=\"urn:LogFormat\">1</div>")]
        public void TransformTrackingValue_XMLTrackingValueMatchInCache_ReturnsCorrectTrackingValue(
           string trackingValue,
           string expectedTrackingValue)
        {
            // Arrange
            var actionId = 1;

            TransformParam transformParam = new TransformParam
            {
                TrackingValueTransformTemplatePath = Arg.Any<string>()
            };

            XslCompiledTransform compiledTransformInCache = null;

            cacheForTesting
                .TryGet($"Alpha.ViewLogs.TransformObject_{actionId}", out compiledTransformInCache)
                .Returns(x =>
                {
                    var compileTransform = new XslCompiledTransform();
                    compileTransform.Load(FileHelper.GetFilePath(@"\TestData\TrackingValueTransformTemplates\action_1.xslt"));

                    x[1] = compileTransform;

                    return true;
                });

            // Act 
            var actualTrackingValue = transformService.TransformTrackingValue(trackingValue, actionId, transformParam);

            // Assert
            Assert.AreEqual(expectedTrackingValue, actualTrackingValue);
        }
    }
}
