namespace VCardOnAbp.ApiServices.Vmcardio.Dtos;

public record GetVmcardioCardInput(string? bin, string? card_id, string? uid);
public class GetVmCardTransactionInput
{
    public string? start { get; set; }
    public string? end { get; set; }
    public string? type { get; set; }
    public string? card_id { get; set; }
    public string? page { get; set; }
    public string? page_size { get; set; }
    public string? bin { get; set; }
    public string? status { get; set; }
    public string? transaction_type { get; set; }
    public string? card_number { get; set; }
    public string? uid { get; set; }
    public string? start_time { get; set; }
    public string? end_time { get; set; }
}
