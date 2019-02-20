namespace Alpha.ViewLogs.Site.Helpers
{
    using System.Web;
    using Configurations;

    public class MicroSitesImpl : MicroSites
    {
        public string OldMainPath(string virtualPath)
        {
            return AppPathHelper.GetAppPath(AppSettings.OldMainAppPath, virtualPath);
        }

        public string ViewLogsPath(string virtualPath)
        {
            return AppPathHelper.GetAppPath(HttpRuntime.AppDomainAppVirtualPath, virtualPath);
        }
    }
}
