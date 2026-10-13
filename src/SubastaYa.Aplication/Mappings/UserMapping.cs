using AutoMapper;
using SubastaYa.Aplication.Dtos.User;
using SubastaYa.Aplication.UseCases.Users.Commands.RegisterCommand;
using SubastaYa.Aplication.UseCases.Users.Commands.UpdateCommand;
using SubastaYa.Domain.Entities;

namespace SubastaYa.Aplication.Mappings;

public class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap<User, UserResponseDto>()
            .ForMember(dest => dest.WalletId, opt =>
            opt.MapFrom(src => src.Wallet != null ? (int?)src.Wallet.Id : null));

        CreateMap<RegisterUserCommand, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            .ForMember(dest => dest.Wallet, opt => opt.Ignore())

            .ForMember(dest => dest.Auctions, opt => opt.Ignore())
            .ForMember(dest => dest.Bids, opt => opt.Ignore())
            .ForMember(dest => dest.AuditLogs, opt => opt.Ignore())

            .ForMember(dest => dest.State, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedAt, opt => opt.Ignore());

        CreateMap<UpdateUserCommand, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            .ForMember(dest => dest.Wallet, opt => opt.Ignore())
            .ForMember(dest => dest.Auctions, opt => opt.Ignore())
            .ForMember(dest => dest.Bids, opt => opt.Ignore())
            .ForMember(dest => dest.AuditLogs, opt => opt.Ignore())
            .ForMember(dest => dest.State, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedAt, opt => opt.Ignore());

    }
}
