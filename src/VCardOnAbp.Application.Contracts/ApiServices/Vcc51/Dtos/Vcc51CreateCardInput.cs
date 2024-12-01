using System;

namespace VCardOnAbp.ApiServices.Vcc51.Dtos;
public record Vcc51CreateCardInput(string bin, decimal Amount, string? Name, decimal totalFee, Guid cardId);
public record Vcc51FundCardInput(Guid cardId, decimal Amount);
public record Vcc51CardFundingResponse(string Status, string Msg);