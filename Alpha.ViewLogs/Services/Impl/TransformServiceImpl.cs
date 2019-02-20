namespace Alpha.ViewLogs.Services.Impl
{
    using System.IO;
    using System.Text;
    using System.Xml.XPath;
    using System.Xml.Xsl;
    using Constants;
    using Fanex.Caching;
    using Params;

    public class TransformServiceImpl : TransformService
    {
        private const string TransformObjectCacheKey = "Alpha.ViewLogs.TransformObject_{0}";

        private readonly LogFormatter logFormatter;

        public TransformServiceImpl(LogFormatter logFormatter)
        {
            this.logFormatter = logFormatter;
        }

        public string TransformTrackingValue(
            string trackingValue,
            int logActionId,
            TransformParam transformParam)
        {
            if (transformParam == null ||
                string.IsNullOrWhiteSpace(trackingValue)
                || logActionId < 0)
            {
                return string.Empty;
            }

            var compiledTransform = GetCompiledTransformTemplate(logActionId, transformParam.TrackingValueTransformTemplatePath);

            if (compiledTransform == null)
            {
                return string.Empty;
            }

            // Need be reviewed code here, AssignParams should not in formatter class
            logFormatter.Init(transformParam);

            var xsltArguments = new XsltArgumentList();
            xsltArguments.AddExtensionObject("urn:LogFormat", logFormatter);

            // Transform xml must be lower case
            using (MemoryStream stream = new MemoryStream(ASCIIEncoding.UTF8.GetBytes(trackingValue.ToLower())))
            {
                var document = new XPathDocument(stream);
                using (var writer = new StringWriter())
                {
                    compiledTransform.Transform(document, xsltArguments, writer);
                    return writer.ToString();
                }
            }
        }

        private XslCompiledTransform GetCompiledTransformTemplate(int actionId, string trackingValueTransformTemplatePath)
        {
            var cacheKey = string.Format(TransformObjectCacheKey, actionId);
            var cachedCompiledTransform = ConfiguredCacheService.Get<XslCompiledTransform>(cacheKey);

            if (cachedCompiledTransform != null)
            {
                return cachedCompiledTransform;
            }

            var compiledTransform = new XslCompiledTransform();
            var path = FindTemplatePathInDirectoryByActionId(actionId, trackingValueTransformTemplatePath);

            if (File.Exists(path))
            {
                compiledTransform.Load(path);
            }

            if (compiledTransform.OutputSettings == null
                || compiledTransform.TemporaryFiles == null)
            {
                return null;
            }

            ConfiguredCacheService.Set(cacheKey, compiledTransform, DefaultVariables.CacheItemOptions);

            return compiledTransform;
        }

        private string FindTemplatePathInDirectoryByActionId(int actionId, string directory)
        {
            if (!Directory.Exists(directory))
            {
                return string.Empty;
            }

            var path = Path.Combine(directory, $"action_{actionId}.xslt");

            if (File.Exists(path))
            {
                return path;
            }

            var subFolders = Directory.GetDirectories(directory);
            foreach (var subFolder in subFolders)
            {
                path = FindTemplatePathInDirectoryByActionId(actionId, Path.Combine(directory, subFolder));

                if (!string.IsNullOrWhiteSpace(path))
                {
                    return path;
                }
            }

            return string.Empty;
        }
    }
}