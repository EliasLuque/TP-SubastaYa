using AutoMapper;
using SubastaYa.Aplication.Dtos.AuditLog;
using SubastaYa.Domain.Entities;

namespace SubastaYa.Aplication.Mappings;

public class AuditLogMapping : Profile
{
    public AuditLogMapping()
    {
        CreateMap<AuditLog, AuditLogResponseDto>()
            .ForMember(dest => dest.UserEmail, opt =>
            opt.MapFrom(src => src.User != null ? src.User.Email : "Sistema"));
    }
}
