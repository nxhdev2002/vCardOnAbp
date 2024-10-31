namespace VCardOnAbp.Currencies.Dto;
public class CreateCurrencyDto
{
    public required string Name { get; set; }
    public required string Code { get; set; }
    public required char Symbol { get; set; }
    public decimal? UsdRate { get; set; }
}
