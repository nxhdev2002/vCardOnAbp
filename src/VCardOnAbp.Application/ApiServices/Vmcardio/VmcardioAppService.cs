using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using VCardOnAbp.ApiServices.Vmcardio.Dtos;
using VCardOnAbp.Commons;

namespace VCardOnAbp.ApiServices.Vmcardio;

public class VmcardioAppService : VCardOnAbpAppService, IVmcardioAppService
{
    public async Task<object> GetCards(GetCardsFilterInput input)
    {
        Dictionary<string, string> query = input.ToDict();
        object cards = await SendVmcardioRequestAsync<object>(HttpMethod.Get, VmcardioApiConst.GetCards, queryParams: query);
        return cards;
    }

    public Task CreateCardAsync()
    {
        return Task.CompletedTask;
    }

    public Task DeleteCardAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<VmCardDto> GetCard(GetCardInput input)
    {
        Dictionary<string, string> query = input.ToDict();
        ResponseModel<GetCardOutput> card = await SendVmcardioRequestAsync<ResponseModel<GetCardOutput>>(HttpMethod.Get, VmcardioApiConst.GetCard, queryParams: query);
        return card.data.virtual_card;
    }

    public async Task<List<VmCardioTransactionDto>> GetCardTransactions(GetVmCardTransactionInput input)
    {
        Dictionary<string, string> query = input.ToDict();
        ResponseModel<VmCardioTransactionResponse> transactions = await SendVmcardioRequestAsync<ResponseModel<VmCardioTransactionResponse>>(HttpMethod.Get, VmcardioApiConst.GetCardTransactions, queryParams: query);
        return transactions.data.list;
    }

    public async Task FundCardAsync(FundCardDto input)
    {
        Dictionary<string, string> query = input.ToDict();
        ResponseModel<object> data = await SendVmcardioRequestAsync<ResponseModel<object>>(HttpMethod.Post, VmcardioApiConst.FundCard, queryParams: query);
        Logger.LogInformation("FundCardAsync: {0}", data);
    }

    private async Task<T> SendVmcardioRequestAsync<T>(HttpMethod method, string url, Dictionary<string, string> headers = null, Dictionary<string, string> queryParams = null, object body = null)
    {
        string token = await GetVmcardioTokenAsync();
        if (headers != null)
        {
            headers.Add("token", token);
        }
        else
        {
            headers = new Dictionary<string, string>
            {
                { "token", token },
                { "Accept", "application/json" },
                { "user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/128.0.0.0 Safari/537.36" }
            };
        }
        return await HttpHelper.SendRequestAsync<T>(method, url, headers, queryParams, body);
    }

    private async Task<string> GetVmcardioTokenAsync()
    {
        return "oA00l05ypjel6zHjihI2oiIisfAOaeeL=Jidj3WCT4N514OWiFMXwOVJiYi=28hcxTj4C86iQN1UiKRi=83cb2.WiceXQE53RENi1715yDCNODI1MNLUJxfAOQOk2IQ1n3JLQVbmMy=DIaINJizi979219QMIyJWJX";
    }


}
