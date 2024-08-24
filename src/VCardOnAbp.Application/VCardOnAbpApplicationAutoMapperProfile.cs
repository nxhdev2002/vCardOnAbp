using AutoMapper;
using VCardOnAbp.Cards;
using VCardOnAbp.Cards.Dto;

namespace VCardOnAbp;

public class VCardOnAbpApplicationAutoMapperProfile : Profile
{
    public VCardOnAbpApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Card, CardDto>().ReverseMap();
    }
}
