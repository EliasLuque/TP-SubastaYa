using AutoMapper;
using SubastaYa.Aplication.Dtos.Bid;
using SubastaYa.Domain.Entities;

namespace SubastaYa.Aplication.Mappings;

public class BidMapping : Profile
{
    public BidMapping()
    {
        CreateMap<Bid, BidResponseDto>()
            .ForMember(dest => dest.AuctionTitle, opt =>
            opt.MapFrom(src => src.Auction.Title))
            
            .ForMember(dest => dest.UserEmail, opt=>
            opt.MapFrom(src => src.User.Email));

        CreateMap<BidCreateDto, Bid>()
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.BidDate, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Auction, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore());
    }
}
