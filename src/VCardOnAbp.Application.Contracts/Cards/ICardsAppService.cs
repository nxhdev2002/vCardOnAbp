using System;
using System.Threading.Tasks;
using VCardOnAbp.Cards.Dto;
using VCardOnAbp.Models;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace VCardOnAbp.Cards;

public interface ICardsAppService : IApplicationService
{
    Task<PagedResultDto<CardDto>> GetListAsync(GetCardInput input);
    Task<CardDto> GetAsync(Guid id);
    Task<PagedResultDto<CardTransactionDto>> GetTransactionAsync(Guid id, GetCardTransactionInput input);
    Task<ResponseModel> CreateAsync(CreateCardInput input);
    Task<ResponseModel> FundAsync(Guid id, FundCardInput input);
    Task DeleteAsync(Guid id);
    Task<CardSecretDto> GetSecretAsync(Guid id);
}
