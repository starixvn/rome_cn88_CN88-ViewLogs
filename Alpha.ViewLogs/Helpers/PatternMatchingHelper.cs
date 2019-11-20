namespace Alpha.ViewLogs.Helpers
{
    using System.Resources;
    using System.Text.RegularExpressions;
    using AppResources;

    public static class PatternMatchingHelper
    {
        private static readonly ResourceManager Resource = new ResourceManager(typeof(DetailsResources));
        private static readonly Regex CompiledRegex = new Regex(@"\{(.+?)\}", RegexOptions.Compiled);

        public static string Replace(string text)
            => string.IsNullOrWhiteSpace(text)
            ? text
            : CompiledRegex.Replace(text, match => GetResourceValue(match, Resource.GetString(match.Groups[1].Value)));

        private static string GetResourceValue(Match match, string resourceValue)
            => string.IsNullOrWhiteSpace(resourceValue) ? match.Groups[0].Value : resourceValue;
    }
}
