using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace VCardOnAbp.Cards
{
    public class CardManager : DomainService
    {
        public Card CreateCard(string CardNo, Guid SupplierId, string SupplierIdentity, CardStatus cardStatus = CardStatus.Active, decimal Balance = 0)
        {
            // TODO: Return business exception
            if (Balance < 0) return null;

            return new Card(CardNo, SupplierId, SupplierIdentity, cardStatus, Balance);
        }
    }
}
