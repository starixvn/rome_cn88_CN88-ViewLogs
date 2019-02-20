namespace Alpha.ViewLogs.Models
{
    public class LoginLog : Log
    {
        public string Browser { get; set; }

        public string Domain { get; set; }

        public string Country { get; set; }
    }
}
