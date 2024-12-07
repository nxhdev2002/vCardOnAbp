using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VCardOnAbp.Cards;
using VCardOnAbp.Cards.Dto;
using VCardOnAbp.Management.Cards.Dto;
using VCardOnAbp.Models;
using VCardOnAbp.Permissions;
using VCardOnAbp.Security;
using VCardOnAbp.Transactions;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace VCardOnAbp.Management.Cards;

[Authorize(VCardOnAbpPermissions.Manager)]
public class CardsManagementAppService(
  ICardRepository cardRepository,
  IRepository<IdentityUser> userRepository,
  IRepository<UserTransaction> transRepository,
  ICardsAppService cardsAppService
) : VCardOnAbpAppService, ICardsManagementAppService
{
    private readonly ICardRepository _cardRepository = cardRepository;
    private readonly IRepository<IdentityUser> _userRepository = userRepository;
    private readonly IRepository<UserTransaction> _transRepository = transRepository;
    private readonly ICardsAppService _cardsAppService = cardsAppService;

    public async Task<PagedResultDto<CardManagementOutputDto>> GetListAsync(GetCardManagementInput input)
    {
        input.SanitizeInput();

        var query = (await _cardRepository.GetQueryableAsync())
            .WhereIf(!string.IsNullOrEmpty(input.Filter), x =>
                EF.Functions.Like(x.CardNo, $"%{input.Filter}%") ||
                EF.Functions.Like(x.CardName, $"%{input.Filter}%") ||
                EF.Functions.Like(x.Note, $"%{input.Filter}%")
            )
            .WhereIf(input.OwnerIds != null && input.OwnerIds.Count > 0, x => input.OwnerIds!.Contains(x.OwnerId))
            .WhereIf(input.Suppliers != null && input.Suppliers.Count > 0, x => input.Suppliers!.Contains(x.Supplier))
            .WhereIf(input.BinIds != null && input.BinIds.Count > 0, x => input.BinIds!.Contains(x.BinId))
            .WhereIf(input.Statuses != null && input.Statuses.Count > 0, x => input.Statuses!.Contains(x.CardStatus))
            .WhereIf(input.BalanceFrom.HasValue, x => x.Balance >= input.BalanceFrom)
            .WhereIf(input.BalanceTo.HasValue, x => x.Balance <= input.BalanceTo);

        var data = await query.ToListAsync();
        var result = ObjectMapper.Map<List<Card>, List<CardManagementOutputDto>>(data);

        // Get owner names
        var ownerIds = result.Select(x => x.OwnerId).Distinct().ToList();
        var ownerNames = (await _userRepository.GetQueryableAsync())
            .Where(x => ownerIds.Contains(x.Id))
            .Select(x => new { x.Id, x.Name })
            .ToDictionary(x => x.Id, x => x.Name);

        result.ForEach(x => {
            x.OwnerName = ownerNames[x.OwnerId];
            _cardsAppService.BuildCardRowActions(x);
        });

        return new PagedResultDto<CardManagementOutputDto>(
            await query.CountAsync(),
            result
        );

    }

    public async Task<ResponseModel> AddCardAsync(AddCardInput input)
    {
        input.SanitizeInput();

        var card = new Card(
            GuidGenerator.Create(),
            input.CardNo,
            input.BinId,
            input.Supplier,
            input.SupplierIdentity,
            input.Status,
            input.Balance,
            input.CardName,
            input.UserId
        );

        await _cardRepository.InsertAsync(card);

        return ResponseModel.SuccessResponse(L["SuccessToast", L["CardPermission:Add"]]);
    }

    public async Task<ResponseModel> CardDeletionApproval(Guid id, CardDeletionApprovalInput input)
    {
        var card = await (await _cardRepository.GetQueryableAsync())
            .Where(x => x.Id == id)
            .Where(x => x.CardStatus == CardStatus.PendingDelete)
            .FirstOrDefaultAsync() ?? throw new UserFriendlyException(L["CardNotFound"]);

        card.SetLastView(DateTime.UtcNow);

        if (input.IsApproved)
        {
            var user = await _userRepository.GetAsync(x => x.Id == card.OwnerId);

            card.ChangeStatus(CardStatus.Active);
            card.ChangeOwner(CurrentUser.Id!.Value);

            user.ExtraProperties[UserConsts.Balance] = (decimal)(user.ExtraProperties[UserConsts.Balance] ?? 0) + input.RefundAmount;

            await _transRepository.InsertAsync(new UserTransaction(
                GuidGenerator.Create(),
                CurrentUser.Id!.Value,
                card.OwnerId,
                L["CardDeletionApprove"],
                UserTransactionType.RefundAmount,
                input.RefundAmount.GetValueOrDefault(0),
                RelatedTransactionType.Card
            ));

            return ResponseModel.SuccessResponse(L["SuccessToast", L["CardDeletionApprove"]]);
        } else
        {
            card.ChangeStatus(CardStatus.Active);
            return ResponseModel.SuccessResponse(L["SuccessToast", L["CardDeletionReject"]]);
        }
    }
}
