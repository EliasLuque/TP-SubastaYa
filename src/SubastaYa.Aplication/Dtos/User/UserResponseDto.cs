using System.Security.Principal;

namespace SubastaYa.Aplication.Dtos.User;

public class UserResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTime RegistrationDate { get; set; }

    public int? WalletId { get; set; }
}
