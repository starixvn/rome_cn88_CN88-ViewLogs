namespace Alpha.ViewLogs.Services.Impl
{
    using System.Threading;
    using Fanex.RemotingRef;

    public class RemotingRefWrapperImpl : RemotingRefWrapper
    {
        private readonly IRemotingRefService remotingRefService;

        public RemotingRefWrapperImpl(IRemotingRefService remotingRefService)
        {
            this.remotingRefService = remotingRefService;
        }

        public string GetBetType(object betTypeId, object betId = null)
        {
            // resourceId remove first character
            string resourceId = string.Empty;

            if (betId != null && betId.ToString().Length > 1)
            {
                resourceId = betId.ToString().Substring(1);
            }

            return betTypeId == null ?
                string.Empty :
                remotingRefService.GetBetTypeName(betTypeId.ToString(), LanguageCode, string.Empty, resourceId);
        }

        public string GetBetTypeName(object bettypeId)
        {
            return bettypeId != null
                ? remotingRefService.GetBetTypeName(bettypeId.ToString(), LanguageCode)
                : string.Empty;
        }

        public string GetSportName(object sportId)
        {
            return sportId == null ?
                string.Empty :
                remotingRefService.GetSportName(sportId.ToString(), LanguageCode, string.Empty);
        }

        public string GetProductName(int productId)
        {
            return remotingRefService.GetResourceDataByRefIdAndResourceId(
                "Product",
                productId.ToString(),
                LanguageCode,
                string.Empty);
        }

        private static string LanguageCode
            => Thread.CurrentThread.CurrentCulture.Name;
    }
}
