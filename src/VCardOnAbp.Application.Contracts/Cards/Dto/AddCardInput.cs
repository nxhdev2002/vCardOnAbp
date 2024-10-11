using System;

namespace VCardOnAbp.Cards.Dto
{
    public class AddCardInput
    {
        public Guid UserId { get; set; }
        public required string CardNo { get; set; }
        public required string SupplierIdentity { get; set; }
        public CardStatus Status { get; set; }
        public decimal Balance { get; set; } = 0;
        public Supplier Supplier { get; set; }
    }
}
