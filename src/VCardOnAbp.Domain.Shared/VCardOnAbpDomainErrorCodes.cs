namespace VCardOnAbp;

public static class VCardOnAbpDomainErrorCodes
{
    /* You can add your business exception error codes here, as constants */
    public const string CardNameSpace = "VCard";
    public const string CardNotFound = CardNameSpace + ":00001";
    public const string BinNotFound = CardNameSpace + ":00002";
    public const string CurrencyNotFound = CardNameSpace + ":00003";
    public const string AmountMustBePositive = CardNameSpace + ":00004";
}
