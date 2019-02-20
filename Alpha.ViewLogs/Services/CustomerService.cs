namespace Alpha.ViewLogs.Services
{
    using Alpha.ViewLogs.Models;

    public interface CustomerService
    {
        CustomerInfo GetDownlineInfoByUsername(string downlineUsername, int uplineId);
    }
}
