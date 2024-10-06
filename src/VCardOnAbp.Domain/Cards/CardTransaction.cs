using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace VCardOnAbp.Cards
{
    public class CardTransaction : CreationAuditedEntity<Guid>
    {
        public Guid CardId { get; private set; }
        public decimal AuthAmount { get; private set; }
        public string? Currency { get; private set; }
        public string? Description { get; private set; }
        public string? MerchantName { get; private  set; }
        public decimal? SettleAmount { get; private set; }
        public string? Status { get; private set; }
        public string? Type { get; private set; }

        private CardTransaction()
        {
        }

        public CardTransaction(
            Guid cardId,
            decimal authAmount,
            string currency,
            string description,
            string merchantName,
            decimal settleAmount,
            string status
        )
        {
            CardId = cardId;
            AuthAmount = authAmount;
            Currency = currency;
            Description = description;
            MerchantName = merchantName;
            SettleAmount = settleAmount;
            Status = status;
        }
    }
}
