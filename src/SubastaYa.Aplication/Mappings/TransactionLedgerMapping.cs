using AutoMapper;
using SubastaYa.Aplication.Dtos.TransactionLedger;
using SubastaYa.Domain.Entities;

namespace SubastaYa.Aplication.Mappings;

public class TransactionLedgerMapping : Profile
{
    public TransactionLedgerMapping()
    {
        CreateMap<TransactionLedger, TransactionLedgerResponseDto>()
            .ForMember(dest => dest.TypeDescription, opt =>
            opt.MapFrom(src => GetTypeDescription((TransactionType)src.Type)))
            
            .ForMember(dest => dest.AuctionTitle, opt =>
            opt.MapFrom(src => src.Auction != null ? src.Auction.Title : null));
    }

    private string GetTypeDescription(TransactionType type) => type switch
    {
        TransactionType.DEPOSIT => "Deposito",
        TransactionType.RETENTION => "Retencion",
        TransactionType.RELEASE => "Liberacion",
        TransactionType.PAYMENT => "Pago",
        TransactionType.COLLECTION => "Cobro",
        _ => "Desconocido"
    };
}
