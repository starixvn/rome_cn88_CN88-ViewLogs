namespace Alpha.ViewLogs.Models
{
    public class CustomerInfo
    {
        public CustomerInfo(int custId, bool isSubAcc, int roleId = 0)
        {
            CustId = custId;
            IsSubAcc = isSubAcc;
            RoleId = roleId;
        }

        public int CustId { get; }

        public bool IsSubAcc { get; }

        public int RoleId { get; }
    }
}
