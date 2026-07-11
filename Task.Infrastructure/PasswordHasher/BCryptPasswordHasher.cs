using Task.Application.Interface.PasswordHasher;
namespace Task.Infrastructure.PasswordHasher;

public class BCryptPasswordHasher: IPasswordHasher
{
    public string Hash(string password)
      => BCrypt.Net.BCrypt.HashPassword(password);

    public bool Verify(string password, string hashedPassword)
    => BCrypt.Net.BCrypt.Verify(password, hashedPassword);
}