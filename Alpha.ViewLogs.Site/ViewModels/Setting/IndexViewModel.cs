namespace Alpha.ViewLogs.Site.ViewModels.Setting
{
    using System.Collections.Generic;
    using Helpers;
    using Params;

    public class IndexViewModel : LogViewModel
    {
        public IndexViewModel(LogParam logParam, int totalRecords, BrowserHelper browserHelper) : base(logParam, totalRecords, browserHelper)
        {
        }

        public IEnumerable<LogActionTypeViewModel> LogActionTypes { get; set; }
    }
}