namespace Alpha.ViewLogs.Services
{
    public interface RemotingRefWrapper
    {
        string GetBetType(object betTypeId, object betId = null);

        string GetBetTypeName(object bettypeId);

        string GetSportName(object sportId);

        string GetProductName(int productId);
    }
}
