using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using VCardOnAbp.Cards;
using VCardOnAbp.Masters;
using VCardOnAbp.Statistics.Dtos;
using Volo.Abp.Domain.Repositories;
using System.Linq;
using VCardOnAbp.ApiServices.Vcc51;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace VCardOnAbp.Statistics;
public class StatisticAppService(
    IRepository<CardTransaction, Guid> cardTransactionRepository,
    IRepository<Bin, Guid> binRepository,
    ICardRepository cardRepository
) : VCardOnAbpAppService, IStatisticAppService
{
    private readonly IRepository<CardTransaction, Guid> _cardTransactionRepository = cardTransactionRepository;
    private readonly IRepository<Bin, Guid> _binRepository = binRepository;
    private readonly ICardRepository _cardRepository = cardRepository;

    public async Task<StatisticDto> GetAsync()
    {
        var failTransactionRate = await (from cardTransaction in (await _cardTransactionRepository.GetQueryableAsync())
                                  join card in (await _cardRepository.GetQueryableAsync())
                                        .Where(x => x.OwnerId == CurrentUser.Id!.Value)
                                    on cardTransaction.CardId equals card.Id
                                  join bin in (await _binRepository.GetQueryableAsync()).AsNoTracking()
                                    on card.BinId equals bin.Id
                                  group cardTransaction by new { bin.Name, card.Supplier } into g
                                  select new FailedTransactionRate
                                  (
                                      CurrentUser.Id!.Value,
                                      g.Key.Name,
                                      g.Key.Supplier,
                                      g.LongCount(x => Vcc51Const.FailTransactionStatuses.Contains(x.Status!)),
                                      g.LongCount(),
                                      (double)g.Count(x => Vcc51Const.FailTransactionStatuses.Contains(x.Status!)) / g.Count() * 100
                                  )).ToListAsync();

        var totalTransaction = await (from cardTransaction in (await _cardTransactionRepository.GetQueryableAsync())
                                      join card in (await _cardRepository.GetQueryableAsync())
                                            .Where(x => x.OwnerId == CurrentUser.Id!.Value)
                                        on cardTransaction.CardId equals card.Id
                                      select cardTransaction).CountAsync();


        return new StatisticDto(
            CurrentUser.Id!.Value,
            await _cardRepository.CountAsync(x => x.OwnerId == CurrentUser.Id!.Value),
            totalTransaction,
            failTransactionRate
        );
    }

    public async Task<PagedResultDto<StatisticDto>> GetAllStatisticsAsync(PagedResultRequestDto input)
    {
        var query = from cardTransaction in (await _cardTransactionRepository.GetQueryableAsync()).AsNoTracking()
                    join card in (await _cardRepository.GetQueryableAsync()).AsNoTracking()
                      on cardTransaction.CardId equals card.Id
                    join bin in (await _binRepository.GetQueryableAsync()).AsNoTracking()
                      on card.BinId equals bin.Id
                    group cardTransaction by new { bin.Name, card.Supplier, card.OwnerId } into g
                    select new FailedTransactionRate
                    (
                        g.Key.OwnerId,
                        g.Key.Name,
                        g.Key.Supplier,
                        g.LongCount(x => Vcc51Const.FailTransactionStatuses.Contains(x.Status!)),
                        g.LongCount(),
                        (double)g.Count(x => Vcc51Const.FailTransactionStatuses.Contains(x.Status!)) / g.Count() * 100
                    );

        var failTransactionRate = await query.PageBy(input).ToListAsync();

        var totalTransaction = await (from cardTransaction in (await _cardTransactionRepository.GetQueryableAsync()).AsNoTracking()
                                      join card in (await _cardRepository.GetQueryableAsync()).AsNoTracking()
                                        on cardTransaction.CardId equals card.Id
                                      group cardTransaction by card.OwnerId into g
                                      select new
                                      {
                                          UserId = g.Key,
                                          TotalTransaction = g.Count()
                                      }).ToListAsync();
        
        var listOwner = failTransactionRate.Select(x => x.Owner).Distinct();
        // count total card by owner
        var totalCard = await (from card in (await _cardRepository.GetQueryableAsync()).AsNoTracking()
                               where listOwner.Contains(card.OwnerId)
                               group card by card.OwnerId into g
                               select new
                               {
                                   UserId = g.Key,
                                   TotalCard = g.Count()
                               }).ToListAsync();

        // return result
        return new PagedResultDto<StatisticDto>(
            await query.CountAsync(),
            failTransactionRate.Select(x => new StatisticDto(
                x.Owner,
                totalCard.First(y => y.UserId == x.Owner).TotalCard,
                totalTransaction.First(y => y.UserId == x.Owner).TotalTransaction,
                failTransactionRate
            )).ToList()
        );
    }
}
