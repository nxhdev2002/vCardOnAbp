using System;
using System.Threading.Tasks;
using VCardOnAbp.Cards.Dto;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace VCardOnAbp.Cards
{
    public interface ICardsAppService : IApplicationService
    {
        Task<PagedResultDto<CardDto>> GetListAsync(GetCardInput input);
        Task<CardDto> GetAsync(Guid id);
        Task<PagedResultDto<CardTransactionDto>> GetTransactionAsync(GetCardTransactionInput input);
        Task CreateAsync(CreateCardInput input);
        Task FundAsync(FundCardInput input);
    }
}
