using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using VCardOnAbp.ApiServices.Vmcardio.Dtos;
using VCardOnAbp.Commons;

namespace VCardOnAbp.ApiServices.Vmcardio
{
    public class VmcardioAppService : VCardOnAbpAppService, IVmcardioAppService
    {
        public async Task<object> GetCardsAsync(GetCardsFilterInput input)
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

        private async Task<T> SendVmcardioRequestAsync<T>(HttpMethod method, string url, Dictionary<string, string> headers = null, Dictionary<string, string> queryParams = null, object body = null)
        {
            var token = await GetVmcardioTokenAsync();
            if (headers != null)
            {
                headers.Add("token", token);
            } else {
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
            return "R=OswWioi1mO46fhiKiJyIdT21RM3IJI5.iOJz2iC9IME9oj0QAWWjiDn2230yiy=F3JyD7QWA8L6LlNbC7pgIXCeae5eh8NxeU11OMi0iNXQf3ij3JNHNEaVLVcJJj2lO2N=5UXQ7c1QOyi170NYkiAITjk7z=Iib";
        }


    }
}
