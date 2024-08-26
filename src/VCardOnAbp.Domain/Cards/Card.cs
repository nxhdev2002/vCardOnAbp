using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace VCardOnAbp.Cards
{
    public class Card : FullAuditedAggregateRoot<Guid>
    {
        [MaxLength(20)]
        public string CardNo { get; private set; }
        public decimal Balance { get; private set; }
        public Guid SupplierId { get; private set; }
        [MaxLength(50)]
        public string SupplierIdentity { get; private set; }
        public CardStatus CardStatus { get; private set; }

        private Card() { }
        public Card(string cardNo, Guid supplierId, string supplierIdentity, CardStatus cardStatus, decimal balance)
        {
            CardNo = cardNo;
            SupplierId = supplierId;
            SupplierIdentity = supplierIdentity;
            CardStatus = cardStatus;
            Balance = balance;
        }

        public Card ChangeStatus(CardStatus cardStatus)
        {
            CardStatus = cardStatus;
            return this;
        }

        public Card SetBalance(decimal balance)
        {
            if (Balance + balance < 0)
            {
                throw new BusinessException();
            }
            Balance = balance; 
            return this;
        }
    }
}
