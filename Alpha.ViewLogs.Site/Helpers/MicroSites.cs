namespace Alpha.ViewLogs.Site.Helpers
{
    public interface MicroSites
    {
        string OldMainPath(string virtualPath);

        string ViewLogsPath(string virtualPath);
    }
}