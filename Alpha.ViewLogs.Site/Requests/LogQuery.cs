namespace Alpha.ViewLogs.Site.Requests
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using Configurations;
    using Constants;
    using Fanex.Common;
    using Sunplus.Agent.Model.Definitions;
    using Sunplus.Agent.Model.Entities;
    using Sunplus.Agent.Model.Entities.Permission;
    using ViewLogs.Helpers;
    using ViewLogs.Helpers.Impl;

    public class LogQuery
    {
        public const string CookieShowOldValue = "Alpha.ViewLogs.ShowOldValuesCookie";

        private readonly DateTime maxReportDate;

        private readonly DateTime minReportDate;

        private readonly DateTime limitedDateToShowInfo;

        private readonly Dictionary<int, bool> actionIdVisibilities;

        private readonly Dictionary<string, bool> custStatuses;

        private readonly Dictionary<string, int> resourcesByBetTypeCacheRef;

        private readonly Dictionary<string, int> resourceByProductCacheRef;

        public LogQuery() : this(null, null, null)
        {
        }

        public LogQuery(
            IUserSession currentUserSession,
            IDateTime dateTimeWrap,
            bool? isShowOldValue,
            Dictionary<int, bool> actionIdVisibilities = null,
            Dictionary<string, bool> custStatuses = null,
            Dictionary<string, int> resourcesByBetTypeCacheRef = null,
            Dictionary<string, int> resourceByProductCacheRef = null,
            bool? isShowMyanmarOdds = null,
            bool? isShowPokerGames = null)
        {
            currentUserSession = currentUserSession ?? UserSession.Current;
            dateTimeWrap = dateTimeWrap ?? new DateTimeWrap();

            IsShowOldValue = isShowOldValue ?? GetShowOldValueFromCookie();
            this.actionIdVisibilities = actionIdVisibilities;
            this.custStatuses = custStatuses;
            this.resourcesByBetTypeCacheRef = resourcesByBetTypeCacheRef;
            this.resourceByProductCacheRef = resourceByProductCacheRef;

            // Limit time range for view log in 2 years (start from beginning of last year)
            maxReportDate = dateTimeWrap.Now;
            minReportDate = new DateTime(dateTimeWrap.Now.AddYears(-1).Year, 1, 1);

            limitedDateToShowInfo = currentUserSession.GetProperty<DateTime>(SessionKey.CustomerDateLimit);

            RoleId = currentUserSession.RoleId;
            HasViewLogPermission = currentUserSession.SubPageAccess != null && currentUserSession.SubPageAccess.ViewLog;
            Language = currentUserSession.Language;
            IsInternal = false;
            UplineId = currentUserSession.CustId;
            IsShowMyanmarOdds = isShowMyanmarOdds ?? UserPermission.IsShowMyanmarOdds;
            IsShowPokerGames = isShowPokerGames ?? UserPermission.HasPermissionOnProduct(ProductIdConstants.PokerGames);
            PreviousUsername = Username;
        }

        public string PreviousUsername { get; set; }

        public string Username { get; set; }

        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public string ActionTypeId { get; set; }

        public int UplineId { get; set; }

        public byte RoleId { get; set; }

        public int SubAccId { get; set; }

        public bool HasViewLogPermission { get; set; }

        public bool IsInternal { get; set; }

        public bool IsShowOldValue { get; set; }

        public string Language { get; set; }

        public string PageIndex { get; set; }

        public string PageSize { get; set; }

        public Dictionary<int, bool> ActionIdVisibilities
        {
            get
            {
                return actionIdVisibilities ?? GetActionIdVisibilities();
            }
        }

        public Dictionary<string, bool> SportBetTypeVisibilities
        {
            get
            {
                return custStatuses ?? GetCustStatuses();
            }
        }

        public Dictionary<string, int> ResourcesByProductCacheRef
        {
            get
            {
                return resourceByProductCacheRef ?? AppSettings.StatusMappings;
            }
        }

        public Dictionary<string, int> ResourcesByBetTypeCacheRef
        {
            get
            {
                return resourcesByBetTypeCacheRef ?? AppSettings.ResourcesByBetTypeCacheRef;
            }
        }

        public bool IsShowMyanmarOdds { get; }

        public bool IsShowPokerGames { get; }

        public DateTime MaxReportDate
        {
            get
            {
                return maxReportDate;
            }
        }

        public DateTime MinReportDate
        {
            get
            {
                return minReportDate;
            }
        }

        public DateTime LimitedDateForShowInfo => limitedDateToShowInfo;

        private Dictionary<int, bool> GetActionIdVisibilities()
        {
            var visibilities = new Dictionary<int, bool>();

            AddActionVisibilities(visibilities, AppSettings.ActionIds_Racing_151_31, UserPermission.IsShowRacing);
            AddActionVisibilities(visibilities, AppSettings.ActionIds_Racing_151_41, UserPermission.IsShowRacing);
            AddActionVisibilities(visibilities, AppSettings.ActionIds_Racing_151_51, UserPermission.IsShowRacing);
            AddActionVisibilities(visibilities, AppSettings.ActionIds_Racing_152_41, UserPermission.IsShowRacing);
            AddActionVisibilities(visibilities, AppSettings.ActionIds_Racing_152_51, UserPermission.IsShowRacing);
            AddActionVisibilities(visibilities, AppSettings.ActionIds_Racing_153_51, UserPermission.IsShowRacing);

            return visibilities;
        }

        private void AddActionVisibilities(
            Dictionary<int, bool> actionVisibilities,
            IEnumerable<int> actionIds,
            bool visibility)
        {
            foreach (var actionId in actionIds)
            {
                if (!actionVisibilities.ContainsKey(actionId))
                {
                    actionVisibilities.Add(actionId, visibility);
                }
            }
        }

        private Dictionary<string, bool> GetCustStatuses()
        {
            var custStatusDict = new Dictionary<string, bool>
            {
                { "151_31", UserPermission.IsShowRacing },
                { "151_41", UserPermission.IsShowRacing},
                { "151_51", UserPermission.IsShowRacing},
                { "152_41", UserPermission.IsShowRacing},
                { "152_51", UserPermission.IsShowRacing},
                { "153_51", UserPermission.IsShowRacing},
                { "161_90", UserPermission.IsShowNumberGroup },
                { "disablevirtualsports", UserPermission.IsEnableProduct(ProductIdConstants.VirtualSports) },
                { "disablegolddeluxe", UserPermission.IsShowGoldDeluxeSettingForCredit },
                { "disablecolossusbet", UserPermission.IsShowColossusBets },
                { "44", UserPermission.IsShowSportsbook2 },
            };

            foreach (var statusMapping in AppSettings.StatusMappings)
            {
                //// Key: Status Name, Value: Product Id
                custStatusDict.Add(statusMapping.Key, UserPermission.IsEnableProduct(statusMapping.Value));
            }

            return custStatusDict;
        }

        private bool GetShowOldValueFromCookie()
        {
            var cookie = HttpContext.Current.Request.Cookies.Get(CookieShowOldValue);

            bool showOldValue = cookie == null || Converter.ToInt(cookie.Value) == 1;

            return showOldValue;
        }
    }
}