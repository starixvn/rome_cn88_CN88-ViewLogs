namespace Alpha.ViewLogs.Site.ViewModels.Login
{
    using Params;

    public class IndexViewModel : LogViewModel
    {
        public IndexViewModel(LogParam logParam, int totalRecords) : base(logParam, totalRecords)
        {
        }

        public bool IsAgent { get; set; }

        public string PreviousUsername { get; set; }
    }
}