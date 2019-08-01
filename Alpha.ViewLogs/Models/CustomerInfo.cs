namespace Alpha.ViewLogs.Models
{
    public class CustomerInfo
    {
        public CustomerInfo(int custId, int roleId = 0, int subAccId = 0)
        {
            CustId = custId;
            RoleId = roleId;
            SubAccId = subAccId;
        }

        public int CustId { get; }

        public int RoleId { get; }

        public int SubAccId { get; }
    }
}
