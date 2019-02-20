namespace Alpha.ViewLogs.Models
{
    public class CustomerInfo
    {
        public CustomerInfo(int custId, bool isSubAcc)
        {
            CustId = custId;
            IsSubAcc = isSubAcc;
        }

        public int CustId { get; }

        public bool IsSubAcc { get; }
    }
}
