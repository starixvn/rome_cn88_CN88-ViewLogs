namespace Alpha.ViewLogs.Services.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text.RegularExpressions;
    using AppResources;
    using Constants;
    using Criteria;
    using DataAccesses;
    using Fanex.Caching;
    using Fanex.Data.Repository;
    using Helpers;
    using Models;
    using Params;
    using Sunplus.Agent.Model.Base;
    using Sunplus.Agent.Model.Entities;

    public abstract class BaseService
    {
        private const string LogActionCacheKey = "Alpha.ViewLogs.Setting.LogAction";

        private readonly IDynamicRepository dynamicRepository;
        private readonly CustomerService customerService;
        private readonly LogDataAccess logDataAccess;
        private readonly IUserSession session;

        protected BaseService(IDynamicRepository dynamicRepository, CustomerService customerService, LogDataAccess logDataAccess, IUserSession session = null)
        {
            this.dynamicRepository = dynamicRepository;
            this.customerService = customerService;
            this.logDataAccess = logDataAccess;
            this.session = session ?? new UserSessionExt();
        }

        protected LogDataAccess LogDataAccess
        {
            get
            {
                return logDataAccess;
            }
        }

        internal abstract ICriteria CreateGetLogsCriteria(LogParam logParam);

        internal abstract ICriteria CreateGetTotalLogsCriteria(LogParam logParam);

        public string GetLogActionName(int actionId, string language)
        {
            var logAction = GetLogAction(actionId);
            switch (language)
            {
                case LanguageCode.ChineseTraditional:
                    return logAction.ChineseTraditionalName;

                case LanguageCode.ChineseSimplified:
                    return logAction.ChineseSimplifiedName;

                case LanguageCode.NewChineseSimplified:
                    return logAction.NewChineseSimplifiedName;

                case LanguageCode.Japanese:
                    return logAction.JapaneseName;

                case LanguageCode.Thai:
                    return logAction.ThaiName;

                case LanguageCode.Korean:
                    return logAction.KoreanName;

                case LanguageCode.Vietnamese:
                    return logAction.VietnameseName;

                default:
                    return logAction.EnglishName;
            }
        }

        public IEnumerable<LogActionType> GetLogActionTypes()
        {
            const int PossessId = 1;
            const int CommissionId = 2;
            const int BetLimitId = 3;
            const int GeneralInfoId = 4;
            const int ProblemAccountId = 6;

            return new List<LogActionType>
            {
                GetLogActionType(PossessId, "Possess"),
                GetLogActionType(CommissionId, "Commission"),
                GetLogActionType(BetLimitId, "BetLimit"),
                GetLogActionType(GeneralInfoId, "GeneralInfo"),
                GetLogActionType(ProblemAccountId, "ProblemAccount")
            };
        }

        private static LogActionType GetLogActionType(int id, string name)
            => new LogActionType
            {
                Id = id,
                ChineseTraditionalName = GetFilterLogAction(name, "zh-TW"),
                NewChineseSimplifiedName = GetFilterLogAction(name, "zh-Hans"),
                EnglishName = GetFilterLogAction(name, "en-US")
            };

        private static string GetFilterLogAction(string name, string language)
            => Resources.ResourceManager.GetString(name, new CultureInfo(language));

        public LogAction GetLogAction(int actionId)
        {
            var logActionDictionary = ConfiguredCacheService.Get<Dictionary<int, LogAction>>(LogActionCacheKey);

            if (logActionDictionary == null
                || !logActionDictionary.Any())
            {
                logActionDictionary = dynamicRepository
                    .Fetch<LogAction>(new GetLogActionsCriteria())
                    .ToDictionary(logAction => logAction.Id);

                ConfiguredCacheService.Set(LogActionCacheKey, logActionDictionary, DefaultVariables.CacheItemOptions);
            }

            return logActionDictionary.ContainsKey(actionId) ?
                logActionDictionary[actionId] :
                new LogAction
                {
                    Id = actionId
                };
        }

        public PagingLogs<Log> GetPagingLogs(LogParam logParam)
        {
            var pagingLogs = new PagingLogs<Log>();
            if (logParam == null)
            {
                return pagingLogs;
            }

            var downlineInfo = customerService.GetDownlineInfoByUsername(logParam.Username, logParam.UplineId, logParam.SubAccId);

            // If search username is not belong to searcher, then return
            if (downlineInfo.CustId == 0)
            {
                return pagingLogs;
            }

            logParam.CustId = downlineInfo.CustId;

            pagingLogs.Logs = GetLogs(logParam);
            pagingLogs.TotalRecords = GetTotalLogs(logParam);

            foreach (var log in pagingLogs.Logs)
            {
                var isHideNickname = IsHideNickName(IsSubAccFromLog(log), downlineInfo.RoleId, downlineInfo.CustId, SubAccNameFromLog(log));
                log.IsHideNickname = isHideNickname;
                log.Details = PatternMatchingHelper.Replace(log.Details);
            }

            return pagingLogs;
        }

        protected DateTime GetGreaterDate(DateTime fromDate, DateTime limitedDate)
            => fromDate < limitedDate ? limitedDate : fromDate;

        protected virtual IEnumerable<Log> GetLogs(LogParam logParam)
        {
            var criteria = CreateGetLogsCriteria(logParam);

            return dynamicRepository.Fetch<Log>(criteria);
        }

        private int GetTotalLogs(LogParam logParam)
        {
            var criteria = CreateGetTotalLogsCriteria(logParam);

            return LogDataAccess.GetTotalRecordsOfLogs(criteria);
        }

        private bool IsHideNickName(bool isSubAccount, int roleId, int custId, string subUsername)
            => IsSubAccViewMainAcc(isSubAccount, custId)
                || IsUplineViewDownlineSubAcc(isSubAccount, roleId)
                || IsSubAccViewOtherSubAcc(isSubAccount, subUsername);

        private bool IsSubAccViewMainAcc(bool isSubAccount, int custId)
            => session.IsSubAcc && session.CustId == custId && !isSubAccount;

        private bool IsSubAccViewOtherSubAcc(bool isSubAccount, string subUsername)
            => session.IsSubAcc && isSubAccount && !session.SubUserName.Equals(subUsername, StringComparison.OrdinalIgnoreCase);

        private bool IsUplineViewDownlineSubAcc(bool isSubAccount, int roleId)
            => isSubAccount && session.RoleId - roleId > 0;

        private static bool IsSubAccFromLog(Log log)
            => Enum.GetValues(typeof(NicknameActions)).Cast<int>().Any(actionId => actionId == log.ActionId)
                && (new Regex($"(?:SubAccName=)(?<paramValue>[^&|^\\s]+)").IsMatch(log.TrackingValue)
                    || new Regex($"(?:SubAccount=)(?<paramValue>[^&|^\\s]+)").IsMatch(log.TrackingValue));

        private static string SubAccNameFromLog(Log log)
        {
            var subAccName = new Regex($"(?:SubAccName=)(?<paramValue>[^&|^\\s]+)")
                .Match(log.TrackingValue ?? string.Empty).Groups[1].Value;

            subAccName = !string.IsNullOrWhiteSpace(subAccName) ? subAccName :
                new Regex($"(?:SubAccount=)(?<paramValue>[^&|^\\s]+)")
                    .Match(log.TrackingValue ?? string.Empty).Groups[1].Value;

            subAccName = subAccName ?? string.Empty;

            return subAccName.Replace("\"", string.Empty);
        }

        private enum NicknameActions
        {
            CreateMainAccNickname = 687,
            CreateSubAccNickname = 690
        }
    }
}