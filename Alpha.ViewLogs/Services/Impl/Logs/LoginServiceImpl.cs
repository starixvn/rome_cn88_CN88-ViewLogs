namespace Alpha.ViewLogs.Services.Impl.Logs
{
    using System;
    using System.Collections.Generic;
    using Constants;
    using Criteria.Login;
    using DataAccesses;
    using Fanex.Data.Repository;
    using Helpers;
    using Models;
    using Params;

    public class LoginServiceImpl : BaseService, LogService
    {
        public LoginServiceImpl(
            IDynamicRepository dynamicRepository,
            CustomerService customerService,
            LogDataAccess logDataAccess) :
            base(dynamicRepository, customerService, logDataAccess)
        {
        }

        protected override IEnumerable<Log> GetLogs(LogParam logParam)
        {
            var criteria = CreateGetLogsCriteria(logParam);

            return LogDataAccess.GetLogs(criteria);
        }

        internal override ICriteria CreateGetLogsCriteria(LogParam logParam)
        {
            if (logParam == null)
            {
                throw new ArgumentNullException("logParam");
            }

            var fromDate = GetGreaterDate(logParam.FromDate, logParam.LimitedDateToShowInfo);

            return new GetLoginLogsCriteria(
                logParam.CustId,
                logParam.Username,
                DateTimeHelper.GetBeginningOfDate(fromDate),
                DateTimeHelper.GetEndingOfDate(logParam.ToDate),                
                logParam.IsInternal ? LoginLogType.InternalAlpha : LoginLogType.Alpha,
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

            return new GetTotalLoginLogsCriteria(
                logParam.CustId,
                logParam.Username,
                DateTimeHelper.GetBeginningOfDate(fromDate),
                DateTimeHelper.GetEndingOfDate(logParam.ToDate),                
                logParam.IsInternal ? LoginLogType.InternalAlpha : LoginLogType.Alpha);
        }
    }
}
