namespace Alpha.ViewLogs.Services.Impl
{
    using Criteria;
    using Fanex.Data.Repository;
    using Models;

    public class CustomerServiceImpl : CustomerService
    {
        private readonly IDynamicRepository dynamicRepository;

        public CustomerServiceImpl(IDynamicRepository dynamicRepository)
        {
            this.dynamicRepository = dynamicRepository;
        }

        public CustomerInfo GetDownlineInfoByUsername(string downlineUsername, int uplineId, int subAccountId)
        {
            var criteria = new GetCustInfoByUsernameCommand
            {
                Username = downlineUsername,
                UplineId = uplineId,
                SubAccountId = subAccountId
            };

            dynamicRepository.Execute(criteria);

            var customerInfo = new CustomerInfo(criteria.CustId, criteria.RoleId, criteria.SubAccountId);

            return customerInfo;
        }
    }
}
