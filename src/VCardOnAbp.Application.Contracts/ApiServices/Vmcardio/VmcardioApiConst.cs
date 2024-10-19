namespace VCardOnAbp.ApiServices.Vmcardio;

public static class VmcardioApiConst
{
    public const string BaseUrl = "https://ms.vmcardio.com/web";
    public const string CreateCard = BaseUrl + "/createCard";
    public const string GetUserInfo = BaseUrl + "/getUserInfo";
    public const string GetCards = BaseUrl + "/getCardList";
    public const string GetCard = BaseUrl + "/getCardDetails";
    public const string GetCardTransactions = BaseUrl + "/getCardTransaction";
    public const string FundCard = BaseUrl + "/rechargeCard";
}
