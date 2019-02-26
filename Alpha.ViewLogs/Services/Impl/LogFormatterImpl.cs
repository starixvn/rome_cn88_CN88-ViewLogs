namespace Alpha.ViewLogs.Services.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Resources;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Xml.Linq;
    using Alpha.ViewLogs.Models;
    using AppResources;
    using Constants;
    using Fanex.Common;
    using Helpers;
    using Params;
    using Sunplus.Agent.Model.Definitions;

    public class LogFormatterImpl : LogFormatter
    {
        private readonly ViewLogIPService ipService;
        private readonly RemotingRefWrapper remotingRef;

        private Dictionary<string, bool> custStatuses;
        private Dictionary<string, int> resourcesByBetTypeCacheRef;
        private Dictionary<string, int> resourcesByProductCacheRef;

        private bool isShowMyanmarOdds;
        private bool isShowPokerGames;

        private static readonly IDictionary<int, string> CN88Sporttype = new Dictionary<int, string>
         {
                { 1, "CN88Soccer" },
                { 2, "CN88Basketball" },
                { 99, "CN88OtherSports" },
                { 9999, "CN88AllSports" }
         };

        private static readonly IEnumerable<CN88SportBettype> CN88SportBettypeMatrix = new List<CN88SportBettype>
            {
                new CN88SportBettype(1, 1, false, "CN88SoccerHandicap"),
                new CN88SportBettype(1, 1, true, "CN88SoccerLiveHandicap"),
                new CN88SportBettype(1, 5, false, "CN88Soccer1X2"),
                new CN88SportBettype(1, 9999, false, "CN88SoccerOtherBettype"),
                new CN88SportBettype(1, 9999, true, "CN88SoccerLiveOtherBettype"),

                new CN88SportBettype(2, 1, false,"CN88BasketballHandicap"),
                new CN88SportBettype(2, 1, true, "CN88BasketballLiveHandicap"),
                new CN88SportBettype(2, 5, false, "CN88Basketball1X2"),
                new CN88SportBettype(2, 9999, false, "CN88BasketballOtherBettype"),

                new CN88SportBettype(99, 1, false, "CN88OtherSporstHandicap"),
                new CN88SportBettype(99, 1, true, "CN88OtherSporstLiveHandicap"),
                new CN88SportBettype(99, 5, false, "CN88OtherSporst1X2"),
                new CN88SportBettype(99, 9999, false, "CN88OtherSportsOtherBettype"),

                new CN88SportBettype(9999, 10, false, "CN88AllSportsBettype")
            };

        public LogFormatterImpl(
            ViewLogIPService ipService,
            RemotingRefWrapper remotingRef)
        {
            this.ipService = ipService;
            this.remotingRef = remotingRef;
        }

        public ResourceManager ResourceManager { get; set; }

        /// <summary>
        /// Need be reviewed code here, AssignParams should not in formatter class
        /// </summary>
        /// <param name="transformParam"></param>
        public void Init(TransformParam transformParam)
        {
            if (transformParam == null)
            {
                return;
            }

            custStatuses = transformParam.CustStatuses;
            resourcesByBetTypeCacheRef = transformParam.ResourcesByBetTypeCacheRef;
            resourcesByProductCacheRef = transformParam.ResourcesProductCacheRef;

            isShowMyanmarOdds = transformParam.IsShowMyanmarOdds;
            isShowPokerGames = transformParam.IsShowPokerGames;
        }

        public string FormatIP(string ip)
        {
            return ipService.FormatIP(ip);
        }

        public string FormatCountryCode(string ip, string country)
        {
            return ipService.FormatCountryCode(ip, country);
        }

        public bool IsVisible(string key)
        {
            return custStatuses.ContainsKey(key) && custStatuses[key];
        }

        public string GetLogResource(string key)
        {
            ResourceManager.IgnoreCase = true;
            return ResourceManager.GetString(key) ?? string.Empty;
        }

        public string GetSportName(int sportType)
        {
            return remotingRef.GetSportName(sportType);
        }

        public string GetBetTypeName(int betType)
        {
            if (betType == BetTypeId.AllBetNormalGames)
            {
                return TrackingValueResources.AllbetNormalGames;
            }

            return remotingRef.GetBetType(betType);
        }

        public string GetProductName(int productId)
        {
            return remotingRef.GetProductName(productId);
        }

        public string TransferConditionFromCode(string dayCodesWithDiff)
        {
            if (string.IsNullOrWhiteSpace(dayCodesWithDiff))
            {
                return string.Empty;
            }

            var codes = XElement.Parse("<root>" + dayCodesWithDiff + "</root>").Elements().ToList();
            var nodeCount = codes.Count;

            var newValue = string.Empty;
            var oldValue = string.Empty;

            var nodeCountIncaseOfHavingNewOldDivs = 2;

            if (nodeCount >= nodeCountIncaseOfHavingNewOldDivs)
            {
                newValue = codes.First().Value;

                // remove char ( at the beginning of oldValue
                // 7 days => 7 characters
                oldValue = codes.Last().Value.Substring(1, 8);

                dayCodesWithDiff = dayCodesWithDiff.Replace(newValue, CovertCodeCondition(newValue))
                                                   .Replace(oldValue, CovertCodeCondition(oldValue));
                return dayCodesWithDiff;
            }

            var nodeCountIncaseOfUnchangeValuesOrNotShowOldValues = 1;

            if (nodeCount == nodeCountIncaseOfUnchangeValuesOrNotShowOldValues)
            {
                newValue = codes.First().Value;

                // title: "{oldValue} --> {newValue}" or empty
                // old value = Split()[0]
                oldValue = codes.First().Attribute("title").Value.Split()[0];

                dayCodesWithDiff = dayCodesWithDiff.Replace(newValue, CovertCodeCondition(newValue))
                                                   .Replace(oldValue, CovertCodeCondition(oldValue));
                return dayCodesWithDiff;
            }

            return CovertCodeCondition(dayCodesWithDiff);
        }

        public string FormatCodeSite(string code)
        {
            var condition = string.Empty;

            if (string.IsNullOrWhiteSpace(code))
            {
                return condition;
            }

            condition = TrackingValueResources.ResourceManager.GetString("Site_" + code) ?? string.Empty;

            return condition;
        }

        public string FormatErrCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return "0";
            }

            return code;
        }

        public string FormatErrMsg(string message)
        {
            if (string.Equals(message, "no information is available", StringComparison.OrdinalIgnoreCase))
            {
                message = string.Empty;
            }

            return message;
        }

        public string FormatMemberWinLimit(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? string.Empty : value.Replace("x", "X").Replace("unlimited", Resources.Unlimited);
        }

        public string FormatUnlimited(int unlimited)
        {
            return unlimited == -1 ? Resources.Unlimited : unlimited.ToString();
        }

        public bool IsVisibleKeno(string hideKeno)
        {
            return hideKeno == "0";
        }

        public string GetDisabledKenoBetTypeName(string disableBetType)
        {
            return string.Format("{0} {1}", Resources.Disabled, remotingRef.GetBetTypeName(disableBetType));
        }

        public bool IsShowMyanmarOdds()
        {
            return isShowMyanmarOdds;
        }

        public bool IsShowPokerGames()
        {
            return isShowPokerGames;
        }

        public bool IsRNGCasino(int sportType)
        {
            return sportType == SportId.RNGGame ||
                sportType == SportId.RNGMiniGame ||
                sportType == SportId.RNGMobile;
        }

        public bool IsGoldDeluxe(int sportType)
        {
            return sportType == SportId.GoldDeluxe;
        }

        public string RenderCustStatus(string name, string value)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return string.Empty;
            }

            string statusHtmlFormat = "<tr><td colspan=\"3\" class=\"colW1\">{0}</td><td colspan=\"3\" class=\"colW2\">{1}</td></tr>";
            var lowerCaseResourceName = name.ToLower();

            var resourceValue = string.Empty;
            if (custStatuses.ContainsKey(lowerCaseResourceName)
                && custStatuses[lowerCaseResourceName]
                && resourcesByProductCacheRef.ContainsKey(lowerCaseResourceName))
            {
                resourceValue = $"{TrackingValueResources.disabled} {GetProductName(resourcesByProductCacheRef[lowerCaseResourceName])}";
            }
            else if (resourcesByBetTypeCacheRef != null &&
                resourcesByBetTypeCacheRef.ContainsKey(lowerCaseResourceName))
            {
                resourceValue = $"{TrackingValueResources.disabled} {GetBetTypeName(resourcesByBetTypeCacheRef[lowerCaseResourceName])}";
            }
            else if (custStatuses.ContainsKey(lowerCaseResourceName)
                && custStatuses[lowerCaseResourceName])
            {
                resourceValue = GetLogResource(name);
            }

            return string.IsNullOrEmpty(resourceValue) ? string.Empty : string.Format(statusHtmlFormat, resourceValue, value);
        }

        public string GetDisabledPokerGames()
        {
            return string.Format("{0} {1}", Resources.Disabled, remotingRef.GetProductName(ProductIdConstants.PokerGames));
        }

        public string GetDisabledPokerGamesValue(
            string disableValue1,
            string disableValue2,
            string disableValue3,
            string disableValue4,
            string disableValue5)
        {
            var disableValues = new[] { disableValue1, disableValue2, disableValue3, disableValue4, disableValue5 };

            return GetDisabledPokerGamesValue(disableValues);
        }

        private string GetDisabledPokerGamesValue(params string[] disableValues)
        {
            return disableValues.Any(disableValue => disableValue == "0") ? "0" : "1";
        }

        private string CovertCodeCondition(string dayCodes)
        {
            char[] codes = dayCodes.Trim().ToCharArray();
            string condition = string.Empty;

            var indicatorOfWeekly = 7;

            if (!codes.Any()
                || codes.Length < indicatorOfWeekly)
            {
                return condition;
            }

            if (codes[indicatorOfWeekly].ToString() == "0")
            {
                return Resources.Daily;
            }

            var selectedDays = new List<string>();
            var daysInWeek = DateTimeHelper.DaysInWeek();
            var numberOfDaysInWeek = daysInWeek.Count();

            for (var i = 0; i < numberOfDaysInWeek; i++)
            {
                if (codes[i] == '1')
                {
                    selectedDays.Add(daysInWeek[i]);
                }
            }

            return Resources.Weekly + " (" + string.Join(" - ", selectedDays) + ")";
        }

        public string FormatLanguage(string value)
            => value.Replace(LanguageCode.English.ToLowerInvariant(), GetLogResource(LanguageCode.English))
                    .Replace(LanguageCode.ChineseSimplified.ToLowerInvariant(), GetLogResource(LanguageCode.ChineseSimplified))
                    .Replace(LanguageCode.ChineseTraditional.ToLowerInvariant(), GetLogResource(LanguageCode.ChineseTraditional))
                    .Replace(LanguageCode.NewChineseSimplified.ToLowerInvariant(), GetLogResource(LanguageCode.NewChineseSimplified))
                    .Replace(LanguageCode.Japanese.ToLowerInvariant(), GetLogResource(LanguageCode.Japanese))
                    .Replace(LanguageCode.Korean.ToLowerInvariant(), GetLogResource(LanguageCode.Korean))
                    .Replace(LanguageCode.Thai.ToLowerInvariant(), GetLogResource(LanguageCode.Thai))
                    .Replace(LanguageCode.Vietnamese.ToLowerInvariant(), GetLogResource(LanguageCode.Vietnamese));

        public string FormatChatbotSelfDestructionPeriod(string value)
            => value.Replace(SelfdestructionPeriod.FifteenMinutes.ToLowerInvariant(), GetLogResource(nameof(SelfdestructionPeriod.FifteenMinutes)))
                    .Replace(SelfdestructionPeriod.ThirtyMinutes.ToLowerInvariant(), GetLogResource(nameof(SelfdestructionPeriod.ThirtyMinutes)))
                    .Replace(SelfdestructionPeriod.OneHour.ToLowerInvariant(), GetLogResource(nameof(SelfdestructionPeriod.OneHour)))
                    .Replace(SelfdestructionPeriod.ThreeHours.ToLowerInvariant(), GetLogResource(nameof(SelfdestructionPeriod.ThreeHours)))
                    .Replace(SelfdestructionPeriod.SixHours.ToLowerInvariant(), GetLogResource(nameof(SelfdestructionPeriod.SixHours)))
                    .Replace(SelfdestructionPeriod.TwelveHours.ToLowerInvariant(), GetLogResource(nameof(SelfdestructionPeriod.TwelveHours)))
                    .Replace(SelfdestructionPeriod.OneDay.ToLowerInvariant(), GetLogResource(nameof(SelfdestructionPeriod.OneDay)))
                    .Replace(SelfdestructionPeriod.ThreeDays.ToLowerInvariant(), GetLogResource(nameof(SelfdestructionPeriod.ThreeDays)))
                    .Replace(SelfdestructionPeriod.SevenDays.ToLowerInvariant(), GetLogResource(nameof(SelfdestructionPeriod.SevenDays)))
                    .Replace(SelfdestructionPeriod.FifteenDays.ToLowerInvariant(), GetLogResource(nameof(SelfdestructionPeriod.FifteenDays)))
                    .Replace(SelfdestructionPeriod.ThirtyDays.ToLowerInvariant(), GetLogResource(nameof(SelfdestructionPeriod.ThirtyDays)));

        public string GetPlatformOfSearchAndCopyFunction(string value)
            => string.IsNullOrWhiteSpace(value)
            ? string.Empty
            : GetLogResource($"SystemId_{value}");

        public string FormatDecimal(string value, string formatSpecifier)
        {
            StringBuilder result = new StringBuilder(value);
            string[] numbers = Regex.Split(value, @"\D+");
            foreach (string number in numbers)
            {
                if (!string.IsNullOrEmpty(number))
                {
                    result.Replace(number, Converter.ToDecimal(number).ToString(formatSpecifier));
                }
            }

            return result.ToString();
        }

        public string GetCN88BettypeName(int sporttype, int bettype, string isLive)
        {
            var resource = CN88SportBettypeMatrix.FirstOrDefault(sportBettype =>
                sportBettype.Sporttype.Equals(sporttype) &&
                sportBettype.Bettype.Equals(bettype) &&
                sportBettype.IsLive.Equals(isLive.Equals("1")));

            return resource == null ? string.Empty : GetLogResource(resource.ResourceKey);
        }

        public string GetCN88SportName(int sporttype)
            => GetLogResource(CN88Sporttype[sporttype]);
    }
}