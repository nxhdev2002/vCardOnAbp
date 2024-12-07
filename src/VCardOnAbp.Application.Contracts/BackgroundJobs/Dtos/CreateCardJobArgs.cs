﻿using System;
using VCardOnAbp.Cards;

namespace VCardOnAbp.BackgroundJobs.Dtos;

public class CreateCardJobArgs
{
    public Guid CardId { get; set; }
    public string? CardName { get; set; }
    public Supplier Supplier { get; set; }
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
    public Guid BinId { get; set; }
}
