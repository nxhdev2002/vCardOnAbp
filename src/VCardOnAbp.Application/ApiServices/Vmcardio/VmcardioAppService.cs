using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using VCardOnAbp.ApiServices.Vmcardio.Dtos;
using VCardOnAbp.Commons;
using Volo.Abp;

namespace VCardOnAbp.ApiServices.Vmcardio;

[RemoteService(false)]
public class VmcardioAppService : VCardOnAbpAppService, IVmcardioAppService
{
    public async Task<object> GetCards(GetCardsFilterInput input)
    {
        Dictionary<string, string> query = input.ToDict();
        object cards = await SendVmcardioRequestAsync<object>(HttpMethod.Get, VmcardioApiConst.GetCards, queryParams: query);
        return cards;
    }

    public async Task<object> CreateCardAsync(VmcardioCreateCardDto input)
    {
        return Task.CompletedTask;
        Dictionary<string, string> payload = input.ToDict();
        return await SendVmcardioRequestAsync<object>(HttpMethod.Post, VmcardioApiConst.CreateCard, body: payload);
    }

    public Task DeleteCardAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<VmCardDto> GetCard(GetVmcardioCardInput input)
    {
        Dictionary<string, string> query = input.ToDict();
        VmcardioResponseModel<GetCardOutput> card = await SendVmcardioRequestAsync<VmcardioResponseModel<GetCardOutput>>(HttpMethod.Get, VmcardioApiConst.GetCard, queryParams: query);
        return card.data.virtual_card;
    }

    public async Task<List<VmCardioTransactionDto>> GetCardTransactions(GetVmCardTransactionInput input)
    {
        Dictionary<string, string> query = input.ToDict();
        VmcardioResponseModel<VmCardioTransactionResponse> transactions = await SendVmcardioRequestAsync<VmcardioResponseModel<VmCardioTransactionResponse>>(HttpMethod.Get, VmcardioApiConst.GetCardTransactions, queryParams: query);
        return transactions.data.list;
    }

    public async Task<VmcardioResponseModel<object>> FundCardAsync(VmcardioFundCardDto input)
    {
        Dictionary<string, string> query = input.ToDict();
        VmcardioResponseModel<object> data = await SendVmcardioRequestAsync<VmcardioResponseModel<object>>(HttpMethod.Post, VmcardioApiConst.FundCard, formData: query);
        Logger.LogInformation("FundCardAsync: {0}", data);

        return data;
    }

    private async Task<T> SendVmcardioRequestAsync<T>(HttpMethod method, string url, Dictionary<string, string> headers = null, Dictionary<string, string> queryParams = null, Dictionary<string, string> formData = null, object body = null)
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
                { "user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/128.0.0.0 Safari/537.36" }
            };
        }
        return await HttpHelper.SendRequestAsync<T>(method, url, headers, queryParams, formData, body);
    }

    private async Task<string> GetVmcardioTokenAsync()
    {
        return "Ya5TWWh=c1D=Xw6I0EbsCi067NyfMe12eQWXo4K.oOjTRiDOVI6IHjxIE0eif3wiFiij5J3OXzApNlz321UQQIjRbIn25W=L093kC8yJ0aOOiMO7c18yiJk2NMN1iJ7NiQUAJd5iiy2NJV3M9LiJcTLheQCilN=m1A";
    }


}
