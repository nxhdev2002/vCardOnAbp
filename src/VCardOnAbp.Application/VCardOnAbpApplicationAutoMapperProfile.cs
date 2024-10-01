using AutoMapper;
using VCardOnAbp.ApiServices.Vmcardio.Dtos;
using VCardOnAbp.Bins.Dtos;
using VCardOnAbp.Cards;
using VCardOnAbp.Cards.Dto;
using VCardOnAbp.Masters;

namespace VCardOnAbp;

public class VCardOnAbpApplicationAutoMapperProfile : Profile
{
    public VCardOnAbpApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Card, CardDto>().ReverseMap();
        CreateMap<Bin, BinDto>().ReverseMap();

        // Vmcardio
        CreateMap<Card, VmCardDto>()
            .ForMember(x => x.card_no, opt => opt.MapFrom(x => x.CardNo));
    }
}
