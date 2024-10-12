namespace VCardOnAbp.ApiServices.Vmcardio.Dtos;

public class GetCardsFilterInput
{
    public string? card_number { get; set; }
    public string? card_name_new { get; set; }
    public string? card_no { get; set; }
    public string? alias { get; set; }
    public string? status { get; set; }
    public string? bin { get; set; }
    public string? page { get; set; }
    public string? page_size { get; set; }
    public string? type { get; set; }
    public string? uid { get; set; }
}
