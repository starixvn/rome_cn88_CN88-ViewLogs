namespace Alpha.ViewLogs.Tests.ModelsForTesting
{
    using System.Text;
    using ViewLogs.Models;

    public class CustomerInfoForTesting : CustomerInfo
    {
        public CustomerInfoForTesting(int custId, bool isSubAcc) :
            base(custId, isSubAcc)
        {
        }

        public static CustomerInfoForTesting FromCustomerInfo(CustomerInfo customerInfo)
        {
            if (customerInfo == null)
            {
                return null;
            }

            return new CustomerInfoForTesting(customerInfo.CustId, customerInfo.IsSubAcc);
        }

        public override bool Equals(object obj)
        {
            var compareObject = (CustomerInfo)obj;

            var isEqual = CustId == compareObject.CustId &&
                IsSubAcc == compareObject.IsSubAcc;

            return isEqual;
        }

        public override int GetHashCode()
        {
            return CustId.GetHashCode() ^ IsSubAcc.GetHashCode();
        }

        public override string ToString()
        {
            var result = new StringBuilder();

            result.AppendLine("CustId: " + CustId);
            result.AppendLine("IsSubAcc: " + IsSubAcc);
            result.AppendLine(base.ToString());

            return result.ToString();
        }
    }
}
