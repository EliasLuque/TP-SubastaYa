using AutoMapper;
using SubastaYa.Aplication.Dtos.Auction;
using SubastaYa.Aplication.Interface.Services;
using SubastaYa.Aplication.UseCases.Auctions.Commands.CreateCommand;
using SubastaYa.Aplication.UseCases.Auctions.Commands.UpdateCommand;
using SubastaYa.Domain.Entities;
using System.Data.Common;

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
        CreateMap<Auction, AuctionResponseDto>()
            .ForMember(x => x.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
        CreateMap<CreateAuctionCommand, Auction>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (int)AuctionStatus.SCHEDULED))
            .ForMember(dest => dest.Version, opt => opt.MapFrom(src => 1))
            .ReverseMap();
            
        CreateMap<UpdateAuctionCommand, Auction>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version))
            .ReverseMap();
    }
}
