using System.Collections.Generic;
using System;
using VCardOnAbp.Cards.Dto;

namespace VCardOnAbp.Management.Cards.Dto;
public class GetCardManagementInput : GetCardInput
{
    public List<Guid>? OwnerIds { get; set; }
}
