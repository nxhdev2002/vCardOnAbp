using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using VCardOnAbp.ApiServices.Vcc51.Dtos;
using VCardOnAbp.Cards.Dto;

namespace VCardOnAbp.ApiServices.Vcc51;
public class Vcc51AppService : VCardOnAbpAppService, IVcc51AppService
{
    public async Task<List<Vcc51CardTransactionDto>> GetTransaction(string cardNo)
    {
        var response = await GetAsync($"{Vcc51Const.SERVICE_URL}?kano={cardNo}");
        return Vcc51RequestParser.ParseHtmlContentToTransactions(response);
    }
    public async Task<Vcc51Card> GetCardInfo(string cardNo)
    {
        var response = await GetAsync($"{Vcc51Const.SERVICE_URL}?kano={cardNo}");
        return Vcc51RequestParser.ParseHtmlContentToCardInfo(response);
    }

    private Dictionary<string, string> GetZhisanhuiHeaders()
    {
        var cookie = "YOUR COOKIE";
        return new Dictionary<string, string>()
        {
            { "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/108.0.0.0 Safari/537.36 Edg/108.0.1462.76" },
            { "Cookie", "ASP.NET_SessionId=" + cookie }
        };
    }

    private async Task<string> GetAsync(string url)
    {
        var headers = GetZhisanhuiHeaders();
        Activity.Current = null;
        using (var client = new HttpClient())
        {
            client.Timeout = TimeSpan.FromSeconds(30);
            foreach (var header in headers)
            {
                client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }

            var response = await client.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
