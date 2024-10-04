namespace VCardOnAbp.ApiServices.Vmcardio
{
    public static class VmcardioApiConst
    {
        public const string BaseUrl = "https://ms.vmcardio.com/web";
        public const string GetUserInfo = BaseUrl + "/getUserInfo";
        public const string GetCards = BaseUrl + "/getCardList";
        public const string GetCard = BaseUrl + "/getCardDetails";
        public const string GetCardTransaction = BaseUrl + "/getCardTransaction";
    }
}
