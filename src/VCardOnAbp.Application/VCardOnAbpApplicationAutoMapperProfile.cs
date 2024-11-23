using AutoMapper;
using VCardOnAbp.ApiServices.Vmcardio.Dtos;
using VCardOnAbp.Bins.Dtos;
using VCardOnAbp.Cards;
using VCardOnAbp.Cards.Dto;
using VCardOnAbp.Currencies;
using VCardOnAbp.Currencies.Dto;
using VCardOnAbp.Masters;
using VCardOnAbp.Payments;
using VCardOnAbp.Payments.Dtos;

namespace VCardOnAbp;

public class VCardOnAbpApplicationAutoMapperProfile : Profile
{
    public VCardOnAbpApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Card, CardDto>().ReverseMap();
        CreateMap<Card, AddCardInput>().ReverseMap();
        CreateMap<Bin, BinDto>().ReverseMap();
        CreateMap<Currency, CurrencyDto>().ReverseMap();
        CreateMap<Currency, CreateCurrencyDto>().ReverseMap();
        CreateMap<Currency, UpdateCurrencyDto>().ReverseMap();
        CreateMap<PaymentMethod, PaymentMethodDto>().ReverseMap();

        // Vmcardio
        CreateMap<Card, VmCardDto>()
            .ForMember(x => x.card_no, opt => opt.MapFrom(x => x.CardNo));
        CreateMap<VmCardioTransactionDto, CardTransaction>().ReverseMap();
        CreateMap<CardTransaction, CardTransactionDto>().ReverseMap();
        CreateMap<DepositTransaction, CreateDepositTransactionDto>().ReverseMap();
        CreateMap<DepositTransaction, DepositTransactionDto>().ReverseMap();

    }
}
