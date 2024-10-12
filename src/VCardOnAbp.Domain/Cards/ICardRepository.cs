using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace VCardOnAbp.Cards;

public interface ICardRepository : IRepository<Card, Guid>
{
    public Task<Card?> GetCard(Guid id, Guid userId, CancellationToken token = default);
    public Task<List<Card>> GetActiveCardAsync(Supplier? supplier = null, CancellationToken token = default);
}
