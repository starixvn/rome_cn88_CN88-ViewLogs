namespace Alpha.ViewLogs.Site.Tests.Handlers
{
    using System;
    using System.Collections.Generic;
    using Helpers;
    using NSubstitute;
    using Services;
    using Site.Helpers;
    using Sunplus.Agent.Model.Entities;
    using ViewLogs.Helpers;

    public class ConcreteLogHandlerTests
    {
        protected const string InvalidPageIndex = "invalidPageIndex";
        protected const string ValidLogActionName = "ValidLogActionName";
        protected const string ValidIP = "ValidIP";
        protected const byte ValidRoleId = 4;

        protected LogService LogService { get; set; }

        protected LogFormatter LogFormatter { get; set; }

        protected IDateTime DateTimeWrapper { get; set; }

        protected IUserSession CurrentUserSession { get; set; }

        protected MicroSites MicroSites { get; set; }

        protected Dictionary<int, bool> ActionIdVisibilities { get; set; }

        protected Dictionary<string, bool> CustStatuses { get; set; }

        protected Dictionary<string, int> ResourcesByBetTypeCacheRef { get; set; }

        protected Dictionary<string, int> ResourcesByProductCacheRef { get; set; }

        public virtual void SetUp()
        {
            LogService = Substitute.For<LogService>();
            LogFormatter = Substitute.For<LogFormatter>();

            DateTimeWrapper = Substitute.For<IDateTime>();
            var toDate = new DateTime(2016, 10, 1);
            DateTimeWrapper.Now.Returns(toDate);

            CurrentUserSession = Substitute.For<IUserSession>();

            MicroSites = Substitute.For<MicroSites>();
            MicroSites.OldMainPath(Arg.Any<string>()).Returns(string.Empty);
            MicroSites.ViewLogsPath(Arg.Any<string>()).Returns(string.Empty);

            ActionIdVisibilities = new Dictionary<int, bool>();
            CustStatuses = new Dictionary<string, bool>();
            ResourcesByBetTypeCacheRef = new Dictionary<string, int>();
            ResourcesByProductCacheRef = new Dictionary<string, int>();

            LogService.GetLogActionName(Arg.Any<int>(), Arg.Any<string>()).Returns(ValidLogActionName);
            LogFormatter.FormatIP(Arg.Any<string>()).Returns(ValidIP);
        }
    }
}
