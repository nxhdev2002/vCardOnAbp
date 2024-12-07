using System;
using System.Threading.Tasks;
using VCardOnAbp.Cards.Dto;
using VCardOnAbp.Management.Cards.Dto;
using VCardOnAbp.Models;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace VCardOnAbp.Management.Cards;
public interface ICardsManagementAppService : IApplicationService
{
    Task<PagedResultDto<CardManagementOutputDto>> GetListAsync(GetCardManagementInput input);
    Task<ResponseModel> AddCardAsync(AddCardInput input);
    Task<ResponseModel> CardDeletionApproval(Guid id, CardDeletionApprovalInput input);
}
