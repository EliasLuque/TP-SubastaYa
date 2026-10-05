using AutoMapper;
using SubastaYa.Aplication.Dtos.Bid;
using SubastaYa.Aplication.UseCases.Bids.Commands.CreateCommand;
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

        CreateMap<CreateBidCommand, Bid>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.BidDate, opt => opt.Ignore())

            .ForMember(dest => dest.State, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())

            .ForMember(dest => dest.Auction, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore());
    }
}
