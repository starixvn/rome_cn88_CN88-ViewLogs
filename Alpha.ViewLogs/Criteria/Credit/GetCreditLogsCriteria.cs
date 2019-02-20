namespace Alpha.ViewLogs.Criteria.Credit
{
    using System;
    using Fanex.Data.Repository;

    public class GetCreditLogsCriteria : CriteriaBase
    {
        public GetCreditLogsCriteria(
            int custId,
            string custName,
            DateTime fromDate,
            DateTime toDate,
            bool isInternal,
            int pageIndex,
            int pageSize)
        {
            CustId = custId;
            CustName = custName;
            FromDate = fromDate;
            ToDate = toDate;
            IsInternal = isInternal;
            PageSize = pageSize;
            PageIndex = pageIndex;
        }

        public int CustId { get; }

        public string CustName { get; }

        public DateTime FromDate { get; }

        public DateTime ToDate { get; }

        public bool IsInternal { get; }

        public int PageIndex { get; }

        public int PageSize { get; }

        public override string GetSettingKey()
        {
            return "Acc_UserLog_CreditSel";
        }

        public override bool IsValid()
        {
            return FromDate <= ToDate && CustId > 0;
        }
    }
}
