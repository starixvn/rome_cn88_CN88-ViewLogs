namespace Alpha.ViewLogs.Services.Impl.Logs
{
    using System;
    using Criteria.Credit;
    using DataAccesses;
    using Fanex.Data.Repository;
    using Helpers;
    using Params;

    public class CreditServiceImpl : BaseService, LogService
    {
        public CreditServiceImpl(
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

            return new GetCreditLogsCriteria(
                logParam.CustId,
                logParam.Username,
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

            return new GetTotalCreditLogsCriteria(
                logParam.CustId,
                DateTimeHelper.GetBeginningOfDate(fromDate),
                DateTimeHelper.GetEndingOfDate(logParam.ToDate),
                logParam.IsInternal);
        }
    }
}
