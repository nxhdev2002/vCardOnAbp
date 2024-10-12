using Volo.Abp.Application.Dtos;

namespace VCardOnAbp.Bins.Dtos;

public class GetBinDtoInput : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
}
