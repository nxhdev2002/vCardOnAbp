using System;

namespace VCardOnAbp.ApiServices.Vcc51;
public class Vcc51Const
{
    public static readonly Uri SERVICE_URL = new Uri("https://www.51vcc.com/websystem/jcsj/ka_Manage.aspx");
    public const string REGEX_GET_TRANSACTION_PATTERN = @"<td>\s+(?<Time>[0-9]{4}\/[0-9]{1,2}\/[0-9]{1,2} [0-9]{1,2}:[0-9]{1,2}:[0-9]{1,2})\s+<\/td>\s+<td>\s+(?<TransNo>[A-Za-z0-9]+)\s+<\/td>\s+<td>\s+(?<CardNo>[A-Za-z0-9]+)\s+<\/td>\s+<td>\s+(?<User>.*?)\s+<\/td>\s+<td>\s+(?<Merchant>.*?)\s+<\/td>\s+<td>\s+(?<Status>.*?)\s+<\/td>\s+<td>\s+(?<Amount>[-0-9A-Z,\. ]+)\s+<\/td>\s+<td>\s+(?<AuthorizationAmount>[-0-9A-Z,\. ]+)\s+<\/td>\s+<td>\s+(?<RefundAmount>[-0-9A-Z,\. ]+)\s+<\/td>\s+<td>\s+(?<Description>.*?)\s+<\/td>";
    public const string REGEX_GET_CARD_INFO_PATTERN = @"<span id=""lbl([a-z]+)"">(.*?)<\/span>";

    public const string CardNoKey = "kahao";
    public const string CvvKey = "cvv";
    public const string TimeKey = "time";
    public const string CurrencyKey = "bizhong";
    public const string StatusKey = "zt";
    public const string AmountKey = "amt";
    public const string UsedAmountKey = "usedamt";
}
