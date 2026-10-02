using AutoMapper;
using SubastaYa.Aplication.Dtos.Auction;
using SubastaYa.Domain.Entities;

namespace SubastaYa.Aplication.Mappings;

public class AuctionMapping : Profile
{
    public AuctionMapping()
    {
        CreateMap<Auction, AuctionResponseDto>()
            .ForMember(dest => dest.StatusDescription, opt =>
            opt.MapFrom(src => GetStatusDescription((AuctionStatus)src.Status)))
            
            .ForMember(dest => dest.CategoryName, opt =>
            opt.MapFrom(src => src.Category.Name))
            
            .ForMember(dest => dest.TotalBids, opt =>
            opt.MapFrom(src => src.Bids != null ? src.Bids.Count : 0));

        CreateMap<AuctionCreateDto, Auction>()
            .ForMember(dest => dest.CurrentPrice, opt =>
            opt.MapFrom(src => src.BasePrice))
            
            .ForMember(dest => dest.SellerId, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore())

            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Version, opt => opt.Ignore())
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.Bids, opt => opt.Ignore())
            .ForMember(dest => dest.TransactionLedger, opt => opt.Ignore());
    }

    private string GetStatusDescription(AuctionStatus status) => status switch
    {
        AuctionStatus.ACTIVE => "Activa",
        AuctionStatus.SCHEDULED => "Programada",
        AuctionStatus.FINISHED => "Finalizada",
        AuctionStatus.UNSOLD => "Desierta",
        _ => "Desconocido"
    };
}
