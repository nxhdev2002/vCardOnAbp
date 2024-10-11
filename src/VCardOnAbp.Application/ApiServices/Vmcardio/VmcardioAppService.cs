using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using VCardOnAbp.ApiServices.Vmcardio.Dtos;
using VCardOnAbp.Commons;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace VCardOnAbp.ApiServices.Vmcardio
{
    public class VmcardioAppService : VCardOnAbpAppService, IVmcardioAppService
    {
        public async Task<object> GetCards(GetCardsFilterInput input)
        {
            var query = input.ToDict();
            var cards = await SendVmcardioRequestAsync<object>(HttpMethod.Get, VmcardioApiConst.GetCards, queryParams: query);
            return cards;
        }

        public Task CreateCardAsync()
        {
            var ok = 1;
            return Task.CompletedTask;
        }

        public Task DeleteCardAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<VmCardDto> GetCard(GetCardInput input)
        {
            var query = input.ToDict();
            var card = await SendVmcardioRequestAsync<ResponseModel<GetCardOutput>>(HttpMethod.Get, VmcardioApiConst.GetCard, queryParams: query);
            return card.data.virtual_card;
        }

        public async Task<List<VmCardioTransactionDto>> GetCardTransactions(GetVmCardTransactionInput input)
        {
            var query = input.ToDict();
            var transactions = await SendVmcardioRequestAsync<ResponseModel<VmCardioTransactionResponse>>(HttpMethod.Get, VmcardioApiConst.GetCardTransactions, queryParams: query);
            return transactions.data.list;
        }

        public async Task FundCardAsync(FundCardDto input)
        {
            var query = input.ToDict();
            var data = await SendVmcardioRequestAsync<ResponseModel<object>>(HttpMethod.Post, VmcardioApiConst.FundCard, queryParams: query);
            Logger.LogInformation("FundCardAsync: {0}", data);
        }

        private async Task<T> SendVmcardioRequestAsync<T>(HttpMethod method, string url, Dictionary<string, string> headers = null, Dictionary<string, string> queryParams = null, object body = null)
        {
            var token = await GetVmcardioTokenAsync();
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
}
