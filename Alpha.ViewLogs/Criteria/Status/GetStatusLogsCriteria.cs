namespace Alpha.ViewLogs.Criteria.Status
{
    using System;
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class GetStatusLogsCriteria : CriteriaBase
    {
        public GetStatusLogsCriteria(
            int custId,
            string custName,
            int actorId,
            int actionTypeId,
            DateTime fromDate,
            DateTime toDate,
            bool isInternal,
            int pageIndex,
            int pageSize)
        {
            CustId = custId;
            CustName = custName;
            ActorId = actorId;
            ActionTypeId = actionTypeId;
            FromDate = fromDate;
            ToDate = toDate;
            IsInternal = isInternal;
            PageSize = pageSize;
            PageIndex = pageIndex;
        }

        public int CustId { get; set; }

        public string CustName { get; set; }

        [SpParam(SqlParamName = "LoginUserId")]
        public int ActorId { get; }

        public int ActionTypeId { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public bool IsInternal { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public override string GetSettingKey()
        {
            return "CS_UserLog_RunSPSel";
        }

        public override bool IsValid()
        {
            return FromDate <= ToDate && CustId > 0;
        }
    }
}
