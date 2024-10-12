namespace VCardOnAbp;

public static class VCardOnAbpDomainErrorCodes
{
    /* You can add your business exception error codes here, as constants */
    public const string CardNameSpace = "VCard";
    public const string CardNotFound = CardNameSpace + ":00001";
    public const string BinNotFound = CardNameSpace + ":10002";
    public const string BinNotActive = CardNameSpace + ":10003";
    public const string CurrencyNotFound = CardNameSpace + ":20004";
    public const string AmountMustBePositive = CardNameSpace + ":30005";
}
