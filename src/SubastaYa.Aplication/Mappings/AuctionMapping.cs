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
    }

    private string GetStatusDescription(AuctionStatus status) => status switch
    {
        AuctionStatus.ACTIVE => "Activa",
        AuctionStatus.SCHEDULED => "Programada",
        AuctionStatus.FINISHED => "Finalizada",
        AuctionStatus.UNSOLD => "Desierta",
        _ => "Desconocido0"
    };
}
