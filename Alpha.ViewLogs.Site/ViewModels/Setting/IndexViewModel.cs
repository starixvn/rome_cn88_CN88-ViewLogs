namespace Alpha.ViewLogs.Site.ViewModels.Setting
{
    using System.Collections.Generic;
    using CN88.Web.Helpers;
    using Params;

    public class IndexViewModel : LogViewModel
    {
        public IndexViewModel(LogParam logParam, int totalRecords, BrowserHelper browserHelper)
            : base(logParam, totalRecords, browserHelper)
        {
        }

        public IEnumerable<LogActionTypeViewModel> LogActionTypes { get; set; }
    }
}