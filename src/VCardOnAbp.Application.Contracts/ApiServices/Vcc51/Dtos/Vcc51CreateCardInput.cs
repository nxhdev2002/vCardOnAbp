using System;

namespace VCardOnAbp.ApiServices.Vcc51.Dtos;
public record Vcc51CreateCardInput(string bin, decimal Amount, string? Name, decimal totalFee, Guid cardId);
