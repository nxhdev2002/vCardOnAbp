using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
