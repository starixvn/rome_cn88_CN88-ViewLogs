namespace Alpha.ViewLogs.Models
{
    public class CN88SportBettype
    {
        public CN88SportBettype(int sporttype, int bettype, bool isLive, string resourceKey)
        {
            Sporttype = sporttype;
            Bettype = bettype;
            IsLive = isLive;
            ResourceKey = resourceKey;
        }

        public int Sporttype { get; }

        public int Bettype { get; }

        public bool IsLive { get; }

        public string ResourceKey { get; }
    }
}
