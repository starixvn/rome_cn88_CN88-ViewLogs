namespace Alpha.ViewLogs.Criteria.Credit
{
    using System;
    using Fanex.Data.Repository;

    public class GetTotalCreditLogsCriteria : CriteriaBase
    {
        public GetTotalCreditLogsCriteria(
            int custId,
            DateTime fromDate,
            DateTime toDate,
            bool isInternal)
        {
            CustId = custId;
            FromDate = fromDate;
            ToDate = toDate;
            IsInternal = isInternal;
        }

        public int CustId { get; }

        public DateTime FromDate { get; }

        public DateTime ToDate { get; }

        public bool IsInternal { get; }

        public override string GetSettingKey()
        {
            return "Acc_UserLog_CreditSelTotalRecords";
        }

        public override bool IsValid()
        {
            return FromDate <= ToDate && CustId > 0;
        }
    }
}
