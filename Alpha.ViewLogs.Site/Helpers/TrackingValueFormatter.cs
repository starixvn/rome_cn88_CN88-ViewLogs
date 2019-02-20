namespace Alpha.ViewLogs.Site.Helpers
{
    using Models;

    public interface TrackingValueFormatter
    {
        string GetFormattedDiffTrackingValue(Log log, bool isShowOldValue);
    }
}