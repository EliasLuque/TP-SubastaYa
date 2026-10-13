using SubastaYa.Domain.Entities;

namespace SubastaYa.Aplication.Interface.Authentication;

public interface IJwtTokenGenerator 
{
    string GenerateToken(User user);
}
