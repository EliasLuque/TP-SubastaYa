using AutoMapper;
using SubastaYa.Aplication.Dtos.Wallet;
using SubastaYa.Domain.Entities;

namespace SubastaYa.Aplication.Mappings;

public class WalletMapping : Profile
{
    public WalletMapping()
    {
        CreateMap<Wallet, WalletResponseDto>();
    }
}
