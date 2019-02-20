namespace Alpha.ViewLogs.Services
{
    public interface ViewLogIPService
    {
        string FormatIP(string ip);

        string FormatCountryCode(string ip, string country);
    }
}
