namespace Alpha.ViewLogs.Criteria
{
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class GetCustInfoByUsernameCommand : NonQueryCommand
    {
        public string Username { get; set; }

        [SpParam(SqlParamName = "SessionCustID")]
        public int UplineId { get; set; }

        public int SubAccountId { get; set; }

        public int CustId { get; set; }

        public int RoleId { get; set; }

        public override string GetSettingKey()
            => "CS_CustomerSelCheckBelonging_ByUsername";

        public override bool IsValid()
            => !string.IsNullOrWhiteSpace(Username);
    }
}
