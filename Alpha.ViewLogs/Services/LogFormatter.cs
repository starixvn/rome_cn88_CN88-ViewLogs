namespace Alpha.ViewLogs.Services
{
    using Alpha.ViewLogs.Params;

    public interface LogFormatter
    {
        void Init(TransformParam transformParam);

        string FormatIP(string ip);

        string FormatCountryCode(string ip, string country);

        bool IsVisible(string key);

        string GetLogResource(string key);

        string GetSportName(int sportType);

        string GetBetTypeName(int betType);

        string GetProductName(int productId);

        string TransferConditionFromCode(string dayCodesWithDiff);

        string FormatErrCode(string code);

        string FormatErrMsg(string message);

        string FormatMemberWinLimit(string value);

        string FormatUnlimited(int unlimited);

        string FormatLanguage(string value);

        bool IsShowMyanmarOdds();

        bool IsShowPokerGames();

        bool IsVisibleKeno(string hideKeno);

        string GetDisabledKenoBetTypeName(string disableBetType);

        bool IsRNGCasino(int sportType);

        bool IsGoldDeluxe(int sportType);

        string RenderCustStatus(string name, string value);

        string GetDisabledPokerGames();

        string GetDisabledPokerGamesValue(
            string disableValue1,
            string disableValue2,
            string disableValue3,
            string disableValue4,
            string disableValue5);

        string FormatChatbotSelfDestructionPeriod(string value);

        string GetPlatformOfSearchAndCopyFunction(string value);
    }
}