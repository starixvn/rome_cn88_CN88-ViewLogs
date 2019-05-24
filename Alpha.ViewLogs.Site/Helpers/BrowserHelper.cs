namespace Alpha.ViewLogs.Site.Helpers
{
    public interface BrowserHelper
    {
        bool IsMobile();

        bool IsTablet();

        BrowserInfo GetBrowserInfo();
    }
}