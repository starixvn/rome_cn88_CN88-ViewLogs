namespace Alpha.ViewLogs.Site.ViewModels
{
    using Helpers;

    public class TabViewModel
    {
        private readonly BrowserHelper browserHelper;

        public TabViewModel(string selectedTabName, BrowserHelper browserHelper)
        {
            SelectedTabName = selectedTabName;
            this.browserHelper = browserHelper;
        }

        public string SelectedTabName { get; }

        public bool IsDevice => browserHelper.IsMobile(isOnlyTablet: false);
    }
}