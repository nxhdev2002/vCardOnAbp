using Volo.Abp.Application.Dtos;

namespace VCardOnAbp.Accounts.Dtos;
public class GetUserTransactionInput : PagedResultRequestDto
{
    public string? Filter { get; set; }
}
