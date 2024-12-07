using System;
using VCardOnAbp.Cards.Dto;

namespace VCardOnAbp.Management.Cards.Dto;
public class CardManagementOutputDto : CardDto
{
    public Guid OwnerId { get; set; }
    public required string OwnerName { get; set; }
}
