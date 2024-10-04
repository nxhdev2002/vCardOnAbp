using System;
using Volo.Abp.Application.Dtos;

namespace VCardOnAbp.Cards.Dto
{
    public class ActionInput : EntityDto<Guid>
    {
        public CardAction Action { get; set; }
        public decimal? Value { get; set; }
        public string DataHash { get; set; }
    }
}
