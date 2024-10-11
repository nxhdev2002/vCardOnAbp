using System;
using VCardOnAbp.Currencies;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace VCardOnAbp.Users
{
    public class UserCurrency : FullAuditedAggregateRoot<Guid>
    {
        public Guid UserId { get; set; }
        public decimal Balance { get; set; }
        public Guid CurrencyId { get; set; }

        private UserCurrency()
        {
        }

        public UserCurrency(Guid id, Guid userId, decimal balance, Guid currency) : base(id)
        {
            if (balance < 0) throw new BusinessException();
            UserId = userId;
            Balance = balance;
            CurrencyId = currency;
        }
    }
}
