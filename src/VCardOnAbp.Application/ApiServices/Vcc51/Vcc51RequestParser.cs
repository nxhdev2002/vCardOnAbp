using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using VCardOnAbp.ApiServices.Vcc51.Dtos;

namespace VCardOnAbp.ApiServices.Vcc51;
public class Vcc51RequestParser
{
    private const RegexOptions options = RegexOptions.Multiline;

    public static List<Vcc51CardTransactionDto> ParseHtmlContentToTransactions(string htmlContent)
    {
        List<Vcc51CardTransactionDto> result = new();
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
        Vcc51Card result = new();
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

    public static Vcc51PostDataCreateCardDto ParseHtmlContentToPostDataCreateCard(string htmlContent)
    {
        RegexOptions options = RegexOptions.Multiline;
        Vcc51PostDataCreateCardDto result = new();
        // regex to parse html content
        foreach (Match match in Regex.Matches(htmlContent, Vcc51Const.REGEX_GET_CREATE_CARDS_PAYLOAD_PATTERN, options))
        {
            if (match.Success)
            {
                string key = match.Groups["key"].Value;
                string value = match.Groups["value"].Value;
                switch (key)
                {
                    case "__EVENTARGUMENT":
                        result.__EVENTARGUMENT = "";
                        break;
                    case "__LASTFOCUS":
                        result.__LASTFOCUS = "";
                        break;
                    case "__VIEWSTATE":
                        result.__VIEWSTATE = value;
                        break;
                    case "__VIEWSTATEGENERATOR":
                        result.__VIEWSTATEGENERATOR = value;
                        break;
                    case "__EVENTVALIDATION":
                        result.__EVENTVALIDATION = value;
                        break;
                    case "lblkainum":
                        result.lblkainum = value;
                        break;
                    case "iskanum":
                        result.iskanum = value;
                        break;
                    case "lblkaifei":
                        result.lblkaifei = value;
                        break;
                    case "iskafei":
                        result.iskafei = value;
                        break;
                    case "isbizhong":
                        result.isbizhong = value;
                        break;
                    case "lblczfy":
                        result.lblczfy = value;
                        break;
                    case "lblczzd":
                        result.lblczzd = value;
                        break;
                    case "isczzd":
                        result.isczzd = value;
                        break;
                    case "isyue":
                        result.isyue = value;
                        break;
                    case "isczfy":
                        result.isczfy = value;
                        break;
                }
            }
        }
        return result;

    }

    public static FormUrlEncodedContent GetFormUrlEncodedContentToChangeBinCreateCardPayload(Vcc51PostDataCreateCardDto dtoInput)
    {
        return new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("__EVENTTARGET", dtoInput.__EVENTTARGET),
            new KeyValuePair<string, string>("__EVENTARGUMENT", ""),
            new KeyValuePair<string, string>("__LASTFOCUS", ""),
            new KeyValuePair<string, string>("__VIEWSTATE", dtoInput.__VIEWSTATE),
            new KeyValuePair<string, string>("__VIEWSTATEGENERATOR", dtoInput.__VIEWSTATEGENERATOR),
            new KeyValuePair<string, string>("__EVENTVALIDATION", dtoInput.__EVENTVALIDATION),
            new KeyValuePair<string, string>("ddlqd", dtoInput.ddlqd),
            new KeyValuePair<string, string>("txtkamoney", "0"),
            new KeyValuePair<string, string>("txtkanum", "1"),
            new KeyValuePair<string, string>("ddlcu", ""),
            new KeyValuePair<string, string>("ddlyg", "158"),
            new KeyValuePair<string, string>("txtbz", ""),
            new KeyValuePair<string, string>("lblkainum", dtoInput.lblkainum),
            new KeyValuePair<string, string>("iskanum", dtoInput.iskanum),
            new KeyValuePair<string, string>("lblkaifei", dtoInput.lblkaifei),
            new KeyValuePair<string, string>("iskafei", dtoInput.iskafei),
            new KeyValuePair<string, string>("isbizhong", dtoInput.isbizhong),
            new KeyValuePair<string, string>("lblczfy", dtoInput.lblczfy),
            new KeyValuePair<string, string>("lblczzd", dtoInput.lblczzd),
            new KeyValuePair<string, string>("isczfy", dtoInput.isczfy),
            new KeyValuePair<string, string>("isczzd", dtoInput.isczzd),
            new KeyValuePair<string, string>("issky", dtoInput.issky),
            new KeyValuePair<string, string>("lblallfei", dtoInput.lblallfei),
            new KeyValuePair<string, string>("isallfei", dtoInput.isallfei),
            new KeyValuePair<string, string>("isyue", dtoInput.isallfei),
        });
    }

    public static FormUrlEncodedContent GetFormUrlEncodedContentToCreateCardPayload(Vcc51PostDataCreateCardDto dtoInput)
    {
        return new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("__EVENTTARGET", dtoInput.__EVENTTARGET),
            new KeyValuePair<string, string>("__EVENTARGUMENT", ""),
            new KeyValuePair<string, string>("__LASTFOCUS", ""),
            new KeyValuePair<string, string>("__VIEWSTATE", dtoInput.__VIEWSTATE),
            new KeyValuePair<string, string>("__VIEWSTATEGENERATOR", dtoInput.__VIEWSTATEGENERATOR),
            new KeyValuePair<string, string>("__EVENTVALIDATION", dtoInput.__EVENTVALIDATION),
            new KeyValuePair<string, string>("ddlqd", dtoInput.ddlqd),
            new KeyValuePair<string, string>("xxList", dtoInput.xxList),
            new KeyValuePair<string, string>("txtkamoney", dtoInput.txtkamoney),
            new KeyValuePair<string, string>("txtkanum", dtoInput.txtkanum),
            new KeyValuePair<string, string>("ddlcu", dtoInput.ddlcu),
            new KeyValuePair<string, string>("ddlyg", "158"),
            new KeyValuePair<string, string>("txtbz", dtoInput.txtbz),
            new KeyValuePair<string, string>("lblkainum", dtoInput.lblkainum),
            new KeyValuePair<string, string>("iskanum", dtoInput.iskanum),
            new KeyValuePair<string, string>("lblkaifei", dtoInput.lblkaifei),
            new KeyValuePair<string, string>("iskafei", dtoInput.iskafei),
            new KeyValuePair<string, string>("isbizhong", dtoInput.isbizhong),
            new KeyValuePair<string, string>("lblczfy", dtoInput.lblczfy),
            new KeyValuePair<string, string>("lblczzd", dtoInput.lblczzd),
            new KeyValuePair<string, string>("isczfy", dtoInput.isczfy),
            new KeyValuePair<string, string>("isczzd", dtoInput.isczzd),
            new KeyValuePair<string, string>("issky", dtoInput.issky),
            new KeyValuePair<string, string>("lblallfei", dtoInput.lblallfei),
            new KeyValuePair<string, string>("isallfei", dtoInput.isallfei),
            new KeyValuePair<string, string>("isyue", dtoInput.isallfei),
            new KeyValuePair<string, string>("btnSave", "开卡"),
        });
    }

    public static FormUrlEncodedContent GetFormUrlEncodedContentToGetCardByPage(Vcc51PostDataGetCardDto postData)
    {
        return new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("__EVENTTARGET", postData.__EVENTTARGET),
                    new KeyValuePair<string, string>("__EVENTARGUMENT", postData.__EVENTARGUMENT),
                    new KeyValuePair<string, string>("__VIEWSTATE", postData.__VIEWSTATE),
                    new KeyValuePair<string, string>("__VIEWSTATEGENERATOR", postData.__VIEWSTATEGENERATOR),
                    new KeyValuePair<string, string>("__EVENTVALIDATION", postData.__EVENTVALIDATION),
                    new KeyValuePair<string, string>("__LASTFOCUS", postData.__LASTFOCUS),
                    new KeyValuePair<string, string>("start", postData.start),
                    new KeyValuePair<string, string>("txtName", postData.txtName),
                    new KeyValuePair<string, string>("ddlcp", postData.ddlcp),
                    new KeyValuePair<string, string>("ddlzt", postData.ddlzt),
                    new KeyValuePair<string, string>("ddlsh", postData.ddlsh.ToString()),
                    new KeyValuePair<string, string>("ddlyg", postData.ddlyg),
                    new KeyValuePair<string, string>("AspNetPager1_input", postData.AspNetPager1_input.ToString()),
                    new KeyValuePair<string, string>("ddlPageSize", postData.ddlPageSize.ToString()),
                });
    }

    public static int? GetZhisanhuiHtmlToTotalPage(string htmlContent, int PageSize)
    {
        Match match = Regex.Match(htmlContent, Vcc51Const.REGEX_GET_TOTAL_CARD_PATTERN, options);
        if (match.Success)
        {
            decimal totalPage = (decimal.Parse(match.Groups["TotalCard"].Value) / PageSize); ;
            return (int?)Math.Ceiling(totalPage);
        }
        return null;
    }

    public static Vcc51PostDataGetCardDto ParseHtmlContentToPostDataGetPage(string htmlContent, int page, int pageSize)
    {
        RegexOptions options = RegexOptions.Multiline;
        Vcc51PostDataGetCardDto result = new()
        {
            start = "",
            txtName = "",
            ddlcp = "",
            ddlzt = "",
            ddlsh = 1404,
            ddlyg = "",
            AspNetPager1_input = 1,
            ddlPageSize = pageSize,
            __EVENTTARGET = page > 1 ? "AspNetPager1" : "ddlPageSize",
            __EVENTARGUMENT = page > 1 ? page.ToString() : ""
        };
        // regex to parse html content
        foreach (Match match in Regex.Matches(htmlContent, Vcc51Const.REGEX_GET_POST_DATA_PATTERN, options))
        {
            if (match.Success)
            {
                string key = match.Groups["key"].Value;
                string value = match.Groups["value"].Value;
                switch (key)
                {
                    case "__LASTFOCUS":
                        result.__LASTFOCUS = value;
                        break;
                    //case "__EVENTTARGET":
                    //    result.__EVENTTARGET = value;
                    //    break;
                    //case "__EVENTARGUMENT":
                    //    result.__EVENTARGUMENT = value;
                    //    break;
                    case "__VIEWSTATE":
                        result.__VIEWSTATE = value;
                        break;
                    case "__VIEWSTATEGENERATOR":
                        result.__VIEWSTATEGENERATOR = value;
                        break;
                    case "__EVENTVALIDATION":
                        result.__EVENTVALIDATION = value;
                        break;
                }
            }
        }
        return result;

    }

    public static List<Vcc51Card> ParseHtmlContentToCreditCard(string htmlContent)
    {
        List<Vcc51Card> result = new();
        // regex to parse html content
        foreach (Match match in Regex.Matches(htmlContent, Vcc51Const.REGEX_GET_CREDIT_CARDS_PATTERN, options))
        {
            result.Add(new Vcc51Card
            {
                CreationTime = match.Groups["CreationTime"].Value,
                CardNo = match.Groups["CardNo"].Value,
                Cvv = match.Groups["Cvv"].Value,
                Exp = match.Groups["EXP"].Value,
                Currency = match.Groups["Currency"].Value,
                Status = match.Groups["Status"].Value,
                TotalAmount = match.Groups["TotalAmount"].Value,
                RemainAmount = match.Groups["RemainAmount"].Value,
                CardName = match.Groups["CardName"].Value,
                Remark = match.Groups["Remark"].Value
            });
        }
        return result;
    }

}
