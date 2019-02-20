namespace Alpha.ViewLogs.Site.ViewModels.Setting
{
    using System.Collections.Generic;
    using Params;

    public class IndexViewModel : LogViewModel
    {
        public IndexViewModel(LogParam logParam, int totalRecords) : base(logParam, totalRecords)
        {
        }

        public IEnumerable<LogActionTypeViewModel> LogActionTypes { get; set; }
    }
}