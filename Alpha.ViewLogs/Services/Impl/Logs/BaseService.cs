namespace Alpha.ViewLogs.Services.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Constants;
    using Criteria;
    using DataAccesses;
    using Fanex.Caching;
    using Fanex.Data.Repository;
    using Models;
    using Params;
    using AppResources;
    using System.Globalization;

    public abstract class BaseService
    {
        private const string LogActionCacheKey = "Alpha.ViewLogs.Setting.LogAction";

        private readonly IDynamicRepository dynamicRepository;
        private readonly CustomerService customerService;
        private readonly LogDataAccess logDataAccess;

        protected BaseService(IDynamicRepository dynamicRepository, CustomerService customerService, LogDataAccess logDataAccess)
        {
            this.dynamicRepository = dynamicRepository;
            this.customerService = customerService;
            this.logDataAccess = logDataAccess;
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
            IEnumerable<LogActionType> logActionTypes = new List<LogActionType>
            {
                new LogActionType
                {
                    Id = 1,
                    ChineseTraditionalName = GetFilterLogAction("Possess", "zh-TW"),
                    NewChineseSimplifiedName = GetFilterLogAction("Possess", "zh-Hans"),
                    EnglishName = GetFilterLogAction("Possess", "en-US")
                },
                new LogActionType
                {
                    Id = 2,
                    ChineseTraditionalName = GetFilterLogAction("Commission", "zh-TW"),
                    NewChineseSimplifiedName = GetFilterLogAction("Commission", "zh-Hans"),
                    EnglishName = GetFilterLogAction("Commission", "en-US")
                },
                new LogActionType
                {
                    Id = 3,
                    ChineseTraditionalName = GetFilterLogAction("BetLimit", "zh-TW"),
                    NewChineseSimplifiedName = GetFilterLogAction("BetLimit", "zh-Hans"),
                    EnglishName = GetFilterLogAction("BetLimit", "en-US")
                },
                new LogActionType
                {
                    Id = 4,
                    ChineseTraditionalName = GetFilterLogAction("GeneralInfo", "zh-TW"),
                    NewChineseSimplifiedName = GetFilterLogAction("GeneralInfo", "zh-Hans"),
                    EnglishName = GetFilterLogAction("GeneralInfo", "en-US")
                }
            };

            return logActionTypes;
        }

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

            var downlineInfo = customerService.GetDownlineInfoByUsername(logParam.Username, logParam.UplineId);

            // If search username is not belong to searcher, then return
            if (downlineInfo.CustId == 0)
            {
                return pagingLogs;
            }

            logParam.CustId = downlineInfo.CustId;

            pagingLogs.Logs = GetLogs(logParam);
            pagingLogs.TotalRecords = GetTotalLogs(logParam);

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
    }
}