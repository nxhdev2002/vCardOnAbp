using System.Collections.Generic;
using System.Text.RegularExpressions;
using VCardOnAbp.ApiServices.Vcc51.Dtos;

namespace VCardOnAbp.ApiServices.Vcc51;
public class Vcc51RequestParser
{
    private const RegexOptions options = RegexOptions.Multiline;

    public static List<Vcc51CardTransactionDto> ParseHtmlContentToTransactions(string htmlContent)
    {
        var result = new List<Vcc51CardTransactionDto>();
        // regex to parse html content
        foreach (Match match in Regex.Matches(htmlContent, Vcc51Const.REGEX_GET_TRANSACTION_PATTERN, options))
        {
            result.Add(new Vcc51CardTransactionDto
            {
                TransactionTime = match.Groups["Time"].Value,
                SerialNumber = match.Groups["TransNo"].Value,
                Merchant = match.Groups["Merchant"].Value,
                Type = match.Groups["Status"].Value,
                Amount = match.Groups["Amount"].Value,
                AuthorizationAmt = match.Groups["AuthorizationAmount"].Value,
                RefundAmt = match.Groups["RefundAmount"].Value,
                Description = match.Groups["Description"].Value
            });
        }
        return result;
    }

    public static Vcc51Card ParseHtmlContentToCardInfo(string htmlContent)
    {
        var result = new Vcc51Card();
        // regex to parse html content
        foreach (Match match in Regex.Matches(htmlContent, Vcc51Const.REGEX_GET_CARD_INFO_PATTERN, options))
        {
            switch (match.Groups[1].Value)
            {
                case Vcc51Const.CardNoKey:
                    result.CardNo = match.Groups[2].Value;
                    break;
                case Vcc51Const.CvvKey:
                    result.Cvv = match.Groups[2].Value;
                    break;
                case Vcc51Const.TimeKey:
                    result.Exp = match.Groups[2].Value;
                    break;
                case Vcc51Const.AmountKey:
                    result.Amount = match.Groups[2].Value;
                    break;
                case Vcc51Const.StatusKey:
                    result.Status = match.Groups[2].Value;
                    break;
                default:
                    break;
            }
        }
        return result;
    }
}
