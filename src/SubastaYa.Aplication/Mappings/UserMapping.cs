using AutoMapper;
using SubastaYa.Aplication.Dtos.User;
using SubastaYa.Domain.Entities;

namespace SubastaYa.Aplication.Mappings;

public class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap<User, UserResponseDto>()
            .ForMember(dest => dest.WalletId, opt =>
            opt.MapFrom(src => src.Wallet != null ? (int?)src.Wallet.Id : null));

        CreateMap<UserCreateDto, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            .ForMember(dest => dest.RegistrationDate, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Wallet, opt => opt.Ignore())
            .ForMember(dest => dest.Auctions, opt => opt.Ignore())
            .ForMember(dest => dest.AuditLogs, opt => opt.Ignore())
            .ForMember(dest => dest.Bids, opt => opt.Ignore());
    }
}
