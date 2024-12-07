using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace VCardOnAbp.Cards.Dto;

public class GetCardInput : PagedResultRequestDto
{
    public string? Filter { get; set; }
    public List<Guid>? OwnerIds { get; set; } 
    public List<Supplier>? Suppliers { get; set; } 
    public List<Guid>? BinIds { get; set; } 
    public List<CardStatus>? Statuses { get; set; } 
    public decimal? BalanceFrom { get; set; } 
    public decimal? BalanceTo { get; set; } 
}
