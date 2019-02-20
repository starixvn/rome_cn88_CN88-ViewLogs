namespace Alpha.ViewLogs.Site.Helpers.StaticResources
{
    using System.Web;

    public class ViewLogsScripts : BaseStaticResources
    {
        public static string GetAbsolutePath(string relativePath)
        {
            return VirtualPathUtility.ToAbsolute($"{relativePath}{MinText}.js?v={StaticResourcesVersion}");
        }
    }
}