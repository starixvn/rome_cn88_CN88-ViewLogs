namespace Alpha.ViewLogs.Site.ViewModels.Login
{
    using Helpers;
    using Params;

    public class IndexViewModel : LogViewModel
    {
        public IndexViewModel(LogParam logParam, int totalRecords, BrowserHelper browserHelper) : base(logParam, totalRecords, browserHelper)
        {
        }

        public bool IsAgent { get; set; }

        public string PreviousUsername { get; set; }
    }
}