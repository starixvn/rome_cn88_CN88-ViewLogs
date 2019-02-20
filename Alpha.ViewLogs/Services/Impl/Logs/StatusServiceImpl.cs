namespace Alpha.ViewLogs.Services.Impl.Logs
{
    using System;
    using Criteria;
    using Criteria.Status;
    using DataAccesses;
    using Fanex.Data.Repository;
    using Helpers;
    using Params;

    public class StatusServiceImpl : BaseService, LogService
    {
        private const int ActionTypeId = 5;

        public StatusServiceImpl(
            IDynamicRepository dynamicRepository,
            CustomerService customerService,
            LogDataAccess baseDataAccess) :
            base(dynamicRepository, customerService, baseDataAccess)
        {
        }

        internal override ICriteria CreateGetLogsCriteria(LogParam logParam)
        {
            if (logParam == null)
            {
                throw new ArgumentNullException("logParam");
            }

            var fromDate = GetGreaterDate(logParam.FromDate, logParam.LimitedDateToShowInfo);

            return new GetStatusLogsCriteria(
                logParam.CustId,
                logParam.Username,
                ActionTypeId,
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
                ActionTypeId,
                DateTimeHelper.GetBeginningOfDate(fromDate),
                DateTimeHelper.GetEndingOfDate(logParam.ToDate),
                logParam.IsInternal);
        }
    }
}
