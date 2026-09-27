using AutoMapper;
using SubastaYa.Aplication.Dtos.Auction;
using SubastaYa.Domain.Entities;

namespace SubastaYa.Aplication.Mappings;

public class AuctionMapping : Profile
{
    public AuctionMapping()
    {
        CreateMap<Auction, AuctionResponseDto>()
            .ForMember(x => x.StatusDescription, opt => opt.MapFrom(src =>
                src.Status == (int)AuctionStatus.ACTIVE ? "Active" :
                src.Status == (int)AuctionStatus.SCHEDULED ? "Scheduled" :
                src.Status == (int)AuctionStatus.FINISHED ? "Finished" :
                src.Status == (int)AuctionStatus.UNSOLD ? "Unsold" :
                "Unknown"))
            .ReverseMap();
    }
}
