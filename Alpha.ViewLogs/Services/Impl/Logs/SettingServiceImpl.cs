namespace Alpha.ViewLogs.Services.Impl.Logs
{
    using System;
    using System.Runtime.CompilerServices;
    using Criteria;
    using Criteria.Setting;
    using DataAccesses;
    using Fanex.Data.Repository;
    using Helpers;
    using Params;
    using Sunplus.Agent.Model.Entities;

    public class SettingServiceImpl : BaseService, LogService
    {
        public SettingServiceImpl(
             IDynamicRepository dynamicRepository,
             CustomerService customerService,
             LogDataAccess baseDataAccess,
             IUserSession session) :
            base(dynamicRepository, customerService, baseDataAccess, session)
        {
        }

        internal override ICriteria CreateGetLogsCriteria(LogParam logParam)
        {
            if (logParam == null)
            {
                throw new ArgumentNullException("logParam");
            }

            var fromDate = GetGreaterDate(logParam.FromDate, logParam.LimitedDateToShowInfo);

            return new GetSettingLogsCriteria(
                logParam.CustId,
                logParam.Username,
                ((SettingParam)logParam).ActionTypeId,
                DateTimeHelper.GetBeginningOfDate(fromDate),
                DateTimeHelper.GetEndingOfDate(logParam.ToDate),
                logParam.IsInternal,
                logParam.PageIndex,
                logParam.PageSize);
        }

        internal override ICriteria CreateGetTotalLogsCriteria(LogParam logParam)
        {
            if (logParam == null)
            {
                throw new ArgumentNullException("logParam");
            }

            var fromDate = GetGreaterDate(logParam.FromDate, logParam.LimitedDateToShowInfo);

            return new GetTotalSettingOrStatusLogsCriteria(
                logParam.CustId,
                ((SettingParam)logParam).ActionTypeId,
                DateTimeHelper.GetBeginningOfDate(fromDate),
                DateTimeHelper.GetEndingOfDate(logParam.ToDate),
                logParam.IsInternal);
        }
    }
}
