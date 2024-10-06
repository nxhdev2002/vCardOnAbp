using Volo.Abp.Application.Dtos;

namespace VCardOnAbp.Cards.Dto
{
    public class GetCardInput : PagedResultRequestDto
    {
        public string? Filter { get; set; }
    }
}
