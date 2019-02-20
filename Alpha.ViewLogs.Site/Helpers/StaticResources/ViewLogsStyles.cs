namespace Alpha.ViewLogs.Site.Helpers.StaticResources
{
    using System.Web;

    public class ViewLogsStyles : BaseStaticResources
    {
        public static string GetAbsolutePath(string relativePath)
        {
            return VirtualPathUtility.ToAbsolute($"{relativePath}{MinText}.css?v={StaticResourcesVersion}");
        }
    }
}