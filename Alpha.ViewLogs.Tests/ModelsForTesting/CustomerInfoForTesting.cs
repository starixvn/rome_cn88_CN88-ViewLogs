namespace Alpha.ViewLogs.Tests.ModelsForTesting
{
    using System.Text;
    using ViewLogs.Models;

    public class CustomerInfoForTesting : CustomerInfo
    {
        public CustomerInfoForTesting(int custId) : base(custId)
        {
        }

        public static CustomerInfoForTesting FromCustomerInfo(CustomerInfo customerInfo)
        {
            if (customerInfo == null)
            {
                return null;
            }

            return new CustomerInfoForTesting(customerInfo.CustId);
        }

        public override bool Equals(object obj)
        {
            var compareObject = (CustomerInfo)obj;

            var isEqual = CustId == compareObject.CustId;

            return isEqual;
        }

        public override int GetHashCode()
        {
            return CustId.GetHashCode();
        }

        public override string ToString()
        {
            var result = new StringBuilder();

            result.AppendLine("CustId: " + CustId);
            result.AppendLine(base.ToString());

            return result.ToString();
        }
    }
}
