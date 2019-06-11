namespace Alpha.ViewLogs.Criteria.Setting
{
    using System;
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class GetSettingLogsCriteria : CriteriaBase
    {
        public GetSettingLogsCriteria(
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

        public int CustId { get; }

        public string CustName { get; }

        [SpParam(SqlParamName = "LoginUserId")]
        public int ActorId { get; }

        public int ActionTypeId { get; }

        public DateTime FromDate { get; }

        public DateTime ToDate { get; }

        public bool IsInternal { get; }

        public int PageIndex { get; }

        public int PageSize { get; }

        public override string GetSettingKey()
        {
            return "CS_UserLog_RunSPSel";
        }

        public override bool IsValid()
        {
            return FromDate <= ToDate && CustId > 0 && !string.IsNullOrEmpty(CustName);
        }
    }
}
