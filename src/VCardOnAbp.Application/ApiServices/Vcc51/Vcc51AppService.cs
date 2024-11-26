using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using VCardOnAbp.ApiServices.Vcc51.Dtos;
using VCardOnAbp.Cards;
using Volo.Abp;

namespace VCardOnAbp.ApiServices.Vcc51;

[RemoteService(false)]
public class Vcc51AppService(
    ICardRepository cardRepository,
    IConfiguration configuration
) : VCardOnAbpAppService, IVcc51AppService
{
    private readonly ICardRepository _cardRepository = cardRepository;
    private readonly IConfiguration _configuration = configuration;
    private readonly string WebUrl = configuration["ThirdParty:Vcc51:Url"] ?? string.Empty;

    public async Task<List<Vcc51CardTransactionDto>> GetTransaction(string cardNo)
    {
        string response = await GetAsync($"{WebUrl}{Vcc51Const.SERVICE_URL}?kano={cardNo}");
        return Vcc51RequestParser.ParseHtmlContentToTransactions(response);
    }

    public async Task<Vcc51Card> GetCardInfo(string cardNo)
    {
        string response = await GetAsync($"{WebUrl}{Vcc51Const.SERVICE_URL}?kano={cardNo}");
        return Vcc51RequestParser.ParseHtmlContentToCardInfo(response);
    }

    public async Task<bool> CreateCard(Vcc51CreateCardInput input)
    {
        Card? cardDb = await (await _cardRepository.GetQueryableAsync()).FirstOrDefaultAsync(x => x.Id == input.cardId);
        if (cardDb == null) return false;

        decimal Amount = Math.Round(input.Amount);

        // step 1: Get to get input fields
        string response = await GetAsync($"{WebUrl}{Vcc51Const.CREATEEDIT_URL}");
        Vcc51PostDataCreateCardDto dtoInput = Vcc51RequestParser.ParseHtmlContentToPostDataCreateCard(response);
        dtoInput.__EVENTTARGET = "ddlqd";
        dtoInput.ddlqd = input.bin;

        // step 2: Post to get bin info
        FormUrlEncodedContent content = Vcc51RequestParser.GetFormUrlEncodedContentToChangeBinCreateCardPayload(dtoInput);
        string createResponse = await PostAsync($"{WebUrl}{Vcc51Const.CREATEEDIT_URL}", content);


        // step 3: Post to create card
        dtoInput = Vcc51RequestParser.ParseHtmlContentToPostDataCreateCard(createResponse);
        dtoInput.ddlqd = input.bin;
        dtoInput.xxList = "USD";
        dtoInput.txtkamoney = Amount.ToString();
        dtoInput.ddljine = "USD";
        dtoInput.txtkanum = "1";
        dtoInput.ddlcu = "";
        dtoInput.ddlyg = "1404";
        dtoInput.txtbz = CurrentUser.Id.ToString() + "-" + DateTime.Now.ToFileTime() + "-" + input.totalFee.ToString("#,#.00#");
        dtoInput.btnSave = "%BF%AA%BF%A8";
        dtoInput.issky = "";

        decimal fee = (Amount * Convert.ToDecimal(dtoInput.isczfy.Replace('.', ',')) / 100);

        dtoInput.lblallfei = (Amount + fee).ToString().Replace(',', '.') + dtoInput.xxList;
        dtoInput.isallfei = (Amount + fee).ToString().Replace(',', '.');

        FormUrlEncodedContent payload = Vcc51RequestParser.GetFormUrlEncodedContentToCreateCardPayload(dtoInput);
        string responseStr = await PostAsync($"{WebUrl}{Vcc51Const.CREATEEDIT_URL}", payload);


        cardDb.SetIdentifyKey(dtoInput.txtbz);

        return true;
    }

    public async Task<List<Vcc51Card>> GetCards(int pageSize)
    {
        string response = await GetAsync($"{WebUrl}{Vcc51Const.CLIENT_URL}");
        int? totalPage = Vcc51RequestParser.GetZhisanhuiHtmlToTotalPage(response, pageSize);
        List<Vcc51Card> data = new();

        // TODO: Get by page
        //for (int i = 1; i <= totalPage; i++)
        //{
        //    var cards = await GetCardsByPage(response, i, pageSize);
        //    data.AddRange(cards);
        //}

        //return data;

        return Vcc51RequestParser.ParseHtmlContentToCreditCard(response);

    }

    #region Private Methods
    private async Task<List<Vcc51Card>> GetCardsByPage(string htmlContent, int pageNumber, int pageSize)
    {
        Vcc51PostDataGetCardDto postData = Vcc51RequestParser.ParseHtmlContentToPostDataGetPage(htmlContent, pageNumber, pageSize);
        FormUrlEncodedContent content = Vcc51RequestParser.GetFormUrlEncodedContentToGetCardByPage(postData);
        string response = await PostAsync($"{WebUrl}{Vcc51Const.CLIENT_URL}", content);

        return Vcc51RequestParser.ParseHtmlContentToCreditCard(response);
    }

    private Dictionary<string, string> GetZhisanhuiHeaders()
    {
        string cookie = _configuration["ThirdParty:Vcc51:Cookie"] ?? "";
        return new Dictionary<string, string>()
        {
            { "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/108.0.0.0 Safari/537.36 Edg/108.0.1462.76" },
            { "Cookie", "ASP.NET_SessionId=" + cookie }
        };
    }

    private async Task<string> GetAsync(string url)
    {
        Dictionary<string, string> headers = GetZhisanhuiHeaders();
        Activity.Current = null;
        using HttpClient client = new();
        client.Timeout = TimeSpan.FromSeconds(30);
        foreach (KeyValuePair<string, string> header in headers)
        {
            client.DefaultRequestHeaders.Add(header.Key, header.Value);
        }

        HttpResponseMessage response = await client.GetAsync(url);
        return await response.Content.ReadAsStringAsync();
    }
    private async Task<string> PostAsync(string url, FormUrlEncodedContent payload)
    {
        Dictionary<string, string> headers = GetZhisanhuiHeaders();
        Activity.Current = null;
        using HttpClient client = new();
        client.Timeout = TimeSpan.FromSeconds(30);
        foreach (KeyValuePair<string, string> header in headers)
        {
            client.DefaultRequestHeaders.Add(header.Key, header.Value);
        }

        HttpResponseMessage response = await client.PostAsync(url, payload);
        return await response.Content.ReadAsStringAsync();
    }


    #endregion
}
