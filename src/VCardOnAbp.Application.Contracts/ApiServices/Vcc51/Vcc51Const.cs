using System;

namespace VCardOnAbp.ApiServices.Vcc51;
public class Vcc51Const
{
    public static readonly Uri SERVICE_URL = new("https://www.51vcc.com/websystem/jcsj/ka_Manage.aspx");
    public static readonly Uri CREATEEDIT_URL = new("https://www.51vcc.com/websystem/jcsj/Kaoci_AddEdit.aspx?id=0");
    public static readonly Uri CLIENT_URL = new("https://www.51vcc.com/websystem/jcsj/Kaoci_Manage.aspx");

    public const string REGEX_GET_TRANSACTION_PATTERN = @"<td>\s+(?<Time>[0-9]{4}\/[0-9]{1,2}\/[0-9]{1,2} [0-9]{1,2}:[0-9]{1,2}:[0-9]{1,2})\s+<\/td>\s+<td>\s+(?<TransNo>[A-Za-z0-9]+)\s+<\/td>\s+<td>\s+(?<CardNo>[A-Za-z0-9]+)\s+<\/td>\s+<td>\s+(?<User>.*?)\s+<\/td>\s+<td>\s+(?<Merchant>.*?)\s+<\/td>\s+<td>\s+(?<Status>.*?)\s+<\/td>\s+<td>\s+(?<Amount>[-0-9A-Z,\. ]+)\s+<\/td>\s+<td>\s+(?<AuthorizationAmount>[-0-9A-Z,\. ]+)\s+<\/td>\s+<td>\s+(?<RefundAmount>[-0-9A-Z,\. ]+)\s+<\/td>\s+<td>\s+(?<Description>.*?)\s+<\/td>";
    public const string REGEX_GET_CARD_INFO_PATTERN = @"<span id=""lbl([a-z]+)"">(.*?)<\/span>";
    public const string REGEX_GET_CREATE_CARDS_PAYLOAD_PATTERN = @"id=""(?<key>.*?)"".*?value=""(?<value>.*?)""";
    public const string REGEX_GET_TOTAL_CARD_PATTERN = @"<b>(?<TotalCard>[0-9]+)<\/b><\/font>条";
    public const string REGEX_GET_POST_DATA_PATTERN = @"id=""(?<key>[__A-Z]+)"" value=""(?<value>.*?)""";
    public const string REGEX_GET_CREDIT_CARDS_PATTERN = @"<td>\s+(?<Time>[0-9]{4}\/[0-9]{1,2}\/[0-9]{1,2} [0-9]{1,2}:[0-9]{1,2}:[0-9]{1,2})\s+<\/td>\s+<td>\s+(?<CardNo>[0-9]{16})\s+<\/td>\s+<td>\s+(?<Cvv>[0-9]{3})\s+<\/td>\s+<td>\s+(?<EXP>(.*?){8})\s+<\/td>\s+<td>\s+(?<Currency>[A-Z]{3})\s+<\/td>\s+<td>\s+(?<Status>\p{L}+)\s+<\/td>\s+<td>\s+(?<TotalAmount>.*?)\s+<\/td>\s+<td>\s+(?<RemainAmount>.*?)\s+<\/td>\s+<td>\s+<\/td>\s+<td.*?>\s+<.+tips=""(?<Remark>.*?)""";

    public const string CardNoKey = "kahao";
    public const string CvvKey = "cvv";
    public const string TimeKey = "time";
    public const string CurrencyKey = "bizhong";
    public const string StatusKey = "zt";
    public const string AmountKey = "amt";
    public const string UsedAmountKey = "usedamt";

    public const string CardActiveStatus = "已激活";
    public const string TransactionAuthSuccessStatus = "已授权";
}
