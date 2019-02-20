namespace Alpha.ViewLogs.Services
{
    using Params;

    public interface TransformService
    {
        string TransformTrackingValue(string trackingValue, int logActionId, TransformParam transformParam);
    }
}
