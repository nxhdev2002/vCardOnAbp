using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VCardOnAbp.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace VCardOnAbp.Cards
{
    public class CardRepository(IDbContextProvider<VCardOnAbpDbContext> dbContextProvider) : EfCoreRepository<VCardOnAbpDbContext, Card, Guid>(dbContextProvider), ICardRepository
    {
        public async Task<Card?> GetCard(Guid id, Guid userId, CancellationToken token = default)
        {
            return await (await GetQueryableAsync())
                .FirstOrDefaultAsync(x => x.Id == id && x.CreatorId == userId, token);
        }
    }
}
