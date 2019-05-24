namespace Alpha.ViewLogs.Models
{
    public class CustomerInfo
    {
        public CustomerInfo(int custId, bool isSubAcc, int roleId = 0, int subAccId = 0)
        {
            CustId = custId;
            IsSubAcc = isSubAcc;
            RoleId = roleId;
            SubAccId = subAccId;
        }

        public int CustId { get; }

        public bool IsSubAcc { get; }

        public int RoleId { get; }

        public int SubAccId { get; }
    }
}
